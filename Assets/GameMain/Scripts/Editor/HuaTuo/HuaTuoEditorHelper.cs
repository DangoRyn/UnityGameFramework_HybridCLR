using HuaTuo.Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEngine;
using FileMode = System.IO.FileMode;

namespace HuaTuo
{
    /// <summary>
    /// 这里仅仅是一个流程展示
    /// 简单说明如果你想将huatuo的dll做成自动化的简单实现
    /// </summary>
    public class HuaTuoEditorHelper
    {

        private static void CreateDirIfNotExists(string dirName)
        {
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
        }

        public static string ToReleateAssetPath(string s)
        {
            return s.Substring(s.IndexOf("Assets/"));
        }

        private static void CompileDll(string buildDir, BuildTarget target)
        {
            var group = BuildPipeline.GetBuildTargetGroup(target);

            ScriptCompilationSettings scriptCompilationSettings = new ScriptCompilationSettings();
            scriptCompilationSettings.group = group;
            scriptCompilationSettings.target = target;
            scriptCompilationSettings.options = ScriptCompilationOptions.DevelopmentBuild;
            CreateDirIfNotExists(buildDir);
            ScriptCompilationResult scriptCompilationResult = PlayerBuildInterface.CompilePlayerScripts(scriptCompilationSettings, buildDir);
            foreach (var ass in scriptCompilationResult.assemblies)
            {
                Debug.LogFormat("compile assemblies:{0}", ass);
            }
            
            var hotfixDlls = new List<string>()
            {
                "Game.Hotfix.dll",
                // "HotFix.dll",
                // "HotFix2.dll",
            };

            foreach(var dll in hotfixDlls)
            {
                string dllPath = $"{GetDllBuildOutputDirByTarget(target)}/{dll}";
                string dllBytesPath = $"{GetAssetBundleTempDirByTarget(target)}/{dll}.bytes";
                File.Copy(dllPath, dllBytesPath, true);
            }
            
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
        }

        public static string DllBuildOutputDir => Path.GetFullPath($"{Application.dataPath}/../Temp/HuaTuo/build");

        public static string GetDllBuildOutputDirByTarget(BuildTarget target)
        {
            return $"{DllBuildOutputDir}/{target}";
        }

        [MenuItem("HuaTuo/CompileDll/ActiveBuildTarget")]
        public static void CompileDllActiveBuildTarget()
        {
            var target = EditorUserBuildSettings.activeBuildTarget;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/Win64")]
        public static void CompileDllWin64()
        {
            var target = BuildTarget.StandaloneWindows64;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/Linux64")]
        public static void CompileDllLinux()
        {
            var target = BuildTarget.StandaloneLinux64;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/OSX")]
        public static void CompileDllOSX()
        {
            var target = BuildTarget.StandaloneOSX;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/Android")]
        public static void CompileDllAndroid()
        {
            var target = BuildTarget.Android;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }

        [MenuItem("HuaTuo/CompileDll/IOS")]
        public static void CompileDllIOS()
        {
            //var target = EditorUserBuildSettings.activeBuildTarget;
            var target = BuildTarget.iOS;
            CompileDll(GetDllBuildOutputDirByTarget(target), target);
        }


        public static string AssetBundleOutputDir => Application.dataPath + "/../Build/HuaTuo/Output";

        public static string GetAssetBundleOutputDirByTarget(BuildTarget target)
        {
            return $"{AssetBundleOutputDir}/{target}";
        }

        public static string GetAssetBundleTempDirByTarget(BuildTarget target)
        {
            return $"{Application.dataPath}/GameMain/Hotfix";
        }

        [MenuItem("HuaTuo/Generate/MethodBridge_X64")]
        public static void MethodBridge_X86()
        {
            //var target = EditorUserBuildSettings.activeBuildTarget;
            string outputFile = $"{Application.dataPath}/../Library/Huatuo/MethodBridge_x64.cpp";
            var g = new MethodBridgeGenerator(new MethodBridgeGeneratorOptions()
            {
                CallConvention = CallConventionType.X64,
                Assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList(),
                OutputFile = outputFile,
            });

            g.PrepareMethods();
            g.Generate();
            Debug.LogFormat("== output:{0} ==", outputFile);
        }

        [MenuItem("HuaTuo/Generate/MethodBridge_Arm64")]
        public static void MethodBridge_Arm64()
        {
            string outputFile = $"{Application.dataPath}/../Library/Huatuo/MethodBridge_arm64.cpp";
            var g = new MethodBridgeGenerator(new MethodBridgeGeneratorOptions()
            {
                CallConvention = CallConventionType.Arm64,
                Assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList(),
                OutputFile = outputFile,
            });

            g.PrepareMethods();
            g.Generate();
            Debug.LogFormat("== output:{0} ==", outputFile);
        }
    }
}