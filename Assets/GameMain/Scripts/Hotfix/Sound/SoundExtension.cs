//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using GameFramework.DataTable;
using GameFramework.Sound;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public static class SoundExtension
    {
        private const float FadeVolumeDuration = 1f;
        private static int? s_MusicSerialId = null;

        public static int? PlayMusic(this SoundComponent soundComponent, int musicId, object userData = null)
        {
            soundComponent.StopMusic();

            if (GameEntry.LubanTable.TryGetTables(out Cfg.Tables tables))
            {
                var tbMusic = tables.TbMusic.Get(musicId);
                if (tbMusic == null)
                {
                    Log.Warning("Can not load music '{0}' from data table.", musicId.ToString());
                    return null;
                }
                
                PlaySoundParams playSoundParams = PlaySoundParams.Create();
                playSoundParams.Priority = 64;
                playSoundParams.Loop = true;
                playSoundParams.VolumeInSoundGroup = 1f;
                playSoundParams.FadeInSeconds = FadeVolumeDuration;
                playSoundParams.SpatialBlend = 0f;
                s_MusicSerialId = soundComponent.PlaySound(AssetUtility.GetMusicAsset(tbMusic.AssetName), "Music", Constant.AssetPriority.MusicAsset, playSoundParams, null, userData);
                return s_MusicSerialId;
            }
            
            return null;
        }

        public static void StopMusic(this SoundComponent soundComponent)
        {
            if (!s_MusicSerialId.HasValue)
            {
                return;
            }

            soundComponent.StopSound(s_MusicSerialId.Value, FadeVolumeDuration);
            s_MusicSerialId = null;
        }

        public static int? PlaySound(this SoundComponent soundComponent, int soundId, Entity bindingEntity = null, object userData = null)
        {
            if (GameEntry.LubanTable.TryGetTables(out Cfg.Tables tables))
            {
                var tbSound = tables.TbSound.Get(soundId);
                
                PlaySoundParams playSoundParams = PlaySoundParams.Create();
                playSoundParams.Priority = tbSound.Priority;
                playSoundParams.Loop = tbSound.Loop;
                playSoundParams.VolumeInSoundGroup = tbSound.Volume;
                playSoundParams.SpatialBlend = tbSound.SpatialBlend;
                return soundComponent.PlaySound(AssetUtility.GetSoundAsset(tbSound.AssetName), "Sound", Constant.AssetPriority.SoundAsset, playSoundParams, bindingEntity != null ? bindingEntity.Entity : null, userData);
            }

            return null;
        }

        public static int? PlayUISound(this SoundComponent soundComponent, int uiSoundId, object userData = null)
        {
            if (GameEntry.LubanTable.TryGetTables(out Cfg.Tables tables))
            {
                var uiSound = tables.TbUISound.Get(uiSoundId);
                
                if (uiSound == null)
                {
                    Log.Warning("Can not load UI sound '{0}' from data table.", uiSoundId.ToString());
                    return null;
                }

                PlaySoundParams playSoundParams = PlaySoundParams.Create();
                playSoundParams.Priority = uiSound.Priority;
                playSoundParams.Loop = false;
                playSoundParams.VolumeInSoundGroup = uiSound.Volume;
                playSoundParams.SpatialBlend = 0f;
                return soundComponent.PlaySound(AssetUtility.GetUISoundAsset(uiSound.AssetName), "UISound", Constant.AssetPriority.UISoundAsset, playSoundParams, userData);
            }

            return null;
        }

        public static bool IsMuted(this SoundComponent soundComponent, string soundGroupName)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return true;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return true;
            }

            return soundGroup.Mute;
        }

        public static void Mute(this SoundComponent soundComponent, string soundGroupName, bool mute)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return;
            }

            soundGroup.Mute = mute;

            GameEntry.Setting.SetBool(Utility.Text.Format(Constant.Setting.SoundGroupMuted, soundGroupName), mute);
            GameEntry.Setting.Save();
        }

        public static float GetVolume(this SoundComponent soundComponent, string soundGroupName)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return 0f;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return 0f;
            }

            return soundGroup.Volume;
        }

        public static void SetVolume(this SoundComponent soundComponent, string soundGroupName, float volume)
        {
            if (string.IsNullOrEmpty(soundGroupName))
            {
                Log.Warning("Sound group is invalid.");
                return;
            }

            ISoundGroup soundGroup = soundComponent.GetSoundGroup(soundGroupName);
            if (soundGroup == null)
            {
                Log.Warning("Sound group '{0}' is invalid.", soundGroupName);
                return;
            }

            soundGroup.Volume = volume;

            GameEntry.Setting.SetFloat(Utility.Text.Format(Constant.Setting.SoundGroupVolume, soundGroupName), volume);
            GameEntry.Setting.Save();
        }
    }
}
