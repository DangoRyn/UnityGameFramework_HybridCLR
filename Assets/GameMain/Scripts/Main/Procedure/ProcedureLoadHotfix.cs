using System;
using System.Collections.Generic;
using System.Reflection;
using GameFramework.Fsm;
using GameFramework.Procedure;
using GameFramework.Resource;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game
{
    public class ProcedureLoadHotfix : ProcedureBase
    {
        /// <summary>
        /// 需要热更的dll
        /// </summary>
        private static readonly string s_HotfixAssemblyName = "Game.Hotfix";
        
        /// <summary>
        /// 用于补充元数据的dll
        /// </summary>
        private static readonly List<string> s_AotDllList = new List<string>
        {
            "mscorlib",
            "System",
            "System.Core",
            "GameFramework",
            "UnityGameFramework.Runtime",
            "LitJson",
            "protobuf-net"
        };

        private int m_LoadMetadataCount = 0;
        private Assembly m_Assembly;

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            
            m_LoadMetadataCount = 0;
#if UNITY_EDITOR
            if (GameEntry.Base.EditorResourceMode)
            {
                Assembly assembly = Assembly.Load(s_HotfixAssemblyName);
                if (assembly != null)
                {
                    m_Assembly = assembly;
                    StartAppDomain();
                }
            }
            else
            {
                LoadHotfixAssembly();
            }
#else
            LoadMetadataForAOTAssembly();
#endif
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            m_LoadMetadataCount = 0;
            base.OnLeave(procedureOwner, isShutdown);
        }
        
        private void StartAppDomain()
        {
            var AppDomain = m_Assembly.GetType("Game.Hotfix.AppDomain");
            var start = AppDomain.GetMethod("Start");
            start?.Invoke(null, null);
        }

        private void LoadHotfixAssembly()
        {
            string assetName = AssetUtility.GetHotfixAsset(s_HotfixAssemblyName);
            GameEntry.Resource.LoadAsset(assetName,
                new LoadAssetCallbacks(OnLoadHotfixAssetSuccess, OnLoadHotfixAssetFail));
        }

        private void OnLoadHotfixAssetSuccess(string assetName, object asset, float duration, object userData)
        {
            byte[] bytes = (asset as TextAsset).bytes;
            if (bytes.Length > 0)
            {
                m_Assembly = Assembly.Load(bytes);
                StartAppDomain();
            }
        }

        private void OnLoadHotfixAssetFail(string assetName, LoadResourceStatus status, string errorMessage,
            object userData)
        {
            Log.Error("Load hotfix asset failed!" + errorMessage);
        }


        private void LoadMetadataForAOTAssembly()
        {
            foreach (var aotDll in s_AotDllList)
            {
                LoadMetadataAssembly(aotDll);
            }
        }

        private void LoadMetadataAssembly(string dllName)
        {
            string assetName = AssetUtility.GetMetadataAssemblyAsset(dllName);
            GameEntry.Resource.LoadAsset(assetName,
                new LoadAssetCallbacks(OnLoadMetadataAssetSuccess, OnLoadLoadMetadataAssetFail));
        }

        /// <summary>
        /// 为aot assembly加载原始metadata， 这个代码放aot或者热更新都行。
        /// 一旦加载后，如果AOT泛型函数对应native实现不存在，则自动替换为解释模式执行
        /// </summary>
        private unsafe void OnLoadMetadataAssetSuccess(string assetName, object asset, float duration, object userData)
        {
            // 可以加载任意aot assembly的对应的dll。但要求dll必须与unity build过程中生成的裁剪后的dll一致，而不能直接使用
            // 原始dll。
            // 这些dll可以在目录 Temp\StagingArea\Il2Cpp\Managed 下找到。
            // 对于Win Standalone，也可以在 build目录的 {Project}/Managed目录下找到。
            // 对于Android及其他target, 导出工程中并没有这些dll，因此还是得去 Temp\StagingArea\Il2Cpp\Managed 获取。
            //
            // 这里以最常用的mscorlib.dll举例
            //
            // 加载打包时 unity在build目录下生成的 裁剪过的 mscorlib，注意，不能为原始mscorlib
            //
            //string mscorelib = @$"{Application.dataPath}/../Temp/StagingArea/Il2Cpp/Managed/mscorlib.dll";

            // 注意，补充元数据是给AOT dll补充元数据，而不是给热更新dll补充元数据。
            // 热更新dll不缺元数据，不需要补充，如果调用LoadMetadataForAOTAssembly会返回错误
            byte[] bytes = (asset as TextAsset).bytes;
            fixed (byte* ptr = bytes)
            {
                //加载assembly对应的dll，会自动为它hook。一旦aot泛型函数的native函数不存在，用解释器版本代码
                int err = HuatuoApi.LoadMetadataForAOTAssembly((IntPtr)ptr, bytes.Length);
                if (err == 0)
                {
                    m_LoadMetadataCount++;
                    if (m_LoadMetadataCount >= s_AotDllList.Count)
                    {
                        LoadHotfixAssembly();
                    }
                }
                else
                {
                    Log.Error("LoadMetadataForAOTAssembly. ret:" + err);
                }
            }
        }

        private void OnLoadLoadMetadataAssetFail(string assetName, LoadResourceStatus status, string errorMessage,
            object userData)
        {
            Log.Error("Load Metadata asset failed!" + errorMessage);
        }

        public override bool UseNativeDialog { get; }
    }
}