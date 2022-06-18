//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using System.IO;
using UnityEngine;
using UnityGameFramework.Editor;
using UnityGameFramework.Editor.ResourceTools;

namespace Game.Editor
{
    public static class GameFrameworkConfigs
    {
        [BuildSettingsConfigPath]
        public static string BuildSettingsConfig = GameFramework.Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "GameMain/Configs/BuildSettings.xml"));

        [ResourceCollectionConfigPath]
        public static string ResourceCollectionConfig = GameFramework.Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "GameMain/Configs/ResourceCollection.xml"));

        [ResourceEditorConfigPath]
        public static string ResourceEditorConfig = GameFramework.Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "GameMain/Configs/ResourceEditor.xml"));

        [ResourceBuilderConfigPath]
        public static string ResourceBuilderConfig = GameFramework.Utility.Path.GetRegularPath(Path.Combine(Application.dataPath, "GameMain/Configs/ResourceBuilder.xml"));
    }
}
