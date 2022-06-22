using System.IO;
using GameFramework;
using GameFramework.Event;

namespace Game
{
    public class LoadLubanTableSuccessEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoadLubanTableSuccessEventArgs).GetHashCode();
        public override int Id
        {
            get { return EventId; }
        }

        public object UserData
        {
            get;
            private set;
        }

        public string LoadFlagKey
        {
            get;
            private set;
        }
        
        public static LoadLubanTableSuccessEventArgs Create(string loadFlagKey,object userData)
        {
            LoadLubanTableSuccessEventArgs loadLubanTableSuccessEventArgs = ReferencePool.Acquire<LoadLubanTableSuccessEventArgs>();
            loadLubanTableSuccessEventArgs.LoadFlagKey = loadFlagKey;
            loadLubanTableSuccessEventArgs.UserData = userData;

            return loadLubanTableSuccessEventArgs;
        }

        public override void Clear()
        {
            UserData = null;
            LoadFlagKey = string.Empty;
        }
    }
}

