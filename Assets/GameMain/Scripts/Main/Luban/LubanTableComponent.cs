using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityGameFramework.Runtime;
using System.Threading.Tasks;
using Bright.Serialization;
using GameFramework;
using GameFramework.Resource;
using SimpleJSON;

namespace Game
{
    /// <summary>
    /// 全局配置组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/LubanTable")]
    public sealed class LubanTableComponent : GameFrameworkComponent
    {
        private ILubanTables m_LubanTables = null;

        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();
        private Dictionary<string, TaskCompletionSource<TextAsset>> m_LubanTableTcs =
            new Dictionary<string, TaskCompletionSource<TextAsset>>();

        public bool TryGetTables<T>(out T tables) where T : class,ILubanTables
        {
            tables = null;
            if (m_LubanTables != null && m_LubanTables is T)
            {
                tables = m_LubanTables as T;
                return true;
            }
            
            Log.Error("Please initialize luban tables first!");
            return false;
        }
        
        public T GetTables<T>() where T : class,ILubanTables
        {
            if (m_LubanTables != null && m_LubanTables is T)
            {
                return m_LubanTables as T;
            }
            
            Log.Error("Please initialize luban tables first!");
            return default;
        }

        public async void LoadLubanTable<T>(string loadFlagKey,object userData) where T : ILubanTables,new()
        {
            m_LoadedFlag.Clear();
            m_LubanTableTcs.Clear();
            
            var tables = new T();
            
            if (tables.GetTableType == LubanTableType.BYTES)
                await tables.LoadAsync( file => LoadLubanByteBufAsync(file));
            else
                await tables.LoadAsync( file => LoadLubanJsonAsync(file));

            if (IsLoadSuccssful())
            {
                m_LubanTables = tables;
                GameEntry.Event.Fire(this,LoadLubanTableSuccessEventArgs.Create(loadFlagKey,userData));
            }
            else
            {
                GameEntry.Event.Fire(this,LoadLubanTableSuccessEventArgs.Create(loadFlagKey,userData));
            }
        }

        private bool IsLoadSuccssful()
        {
            foreach (var flag in m_LoadedFlag)
            {
                if (!flag.Value)
                    return false;
            }

            return true;
        }

        private async Task<JSONNode> LoadLubanJsonAsync(string file)
        {
            return JSON.Parse((await LoadLubanTableAssetAsync(file,false)).text);
        }

        private async Task<ByteBuf> LoadLubanByteBufAsync(string file)
        {
            return new ByteBuf((await LoadLubanTableAssetAsync(file,true)).bytes);
        }
        
        private Task<TextAsset> LoadLubanTableAssetAsync(string assetName,bool fromBytes)
        {
            var tcs = new TaskCompletionSource<TextAsset>();
            string assetFullName = AssetUtility.GetLubanTableAsset(assetName,fromBytes);
            m_LubanTableTcs.Add(assetFullName,tcs);
            m_LoadedFlag.Add(assetFullName, false);
            
            GameEntry.Resource.LoadAsset(assetFullName,new LoadAssetCallbacks((string assetName, object asset, float duration, object userData) =>
            {
                m_LubanTableTcs.TryGetValue(assetName, out TaskCompletionSource<TextAsset> tcs);
                if (tcs != null)
                {
                    m_LoadedFlag[assetName] = true;
                    tcs.SetResult((TextAsset)asset);
                    m_LubanTableTcs.Remove(assetName);
                }
            }, (string assetName, LoadResourceStatus status, string errorMessage,
                object userData) =>
            {
                m_LubanTableTcs.TryGetValue(assetName, out TaskCompletionSource<TextAsset> tcs);
                if (tcs != null)
                {
                    tcs.SetCanceled();
                    m_LubanTableTcs.Remove(assetName);
                }
                
                Log.Error("Can not load luban table from '{0}' with error message '{1}'.", assetName, errorMessage);
            }));
            
            return tcs.Task;
        }
    }
}


