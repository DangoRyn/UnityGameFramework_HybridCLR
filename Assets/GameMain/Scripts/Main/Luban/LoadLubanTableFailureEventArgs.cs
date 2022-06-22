using GameFramework;
using GameFramework.Event;

namespace Game
{
    public class LoadLubanTableFailureEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(LoadLubanTableFailureEventArgs).GetHashCode();
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
        
        public static LoadLubanTableFailureEventArgs Create(string loadFlagKey,object userData)
        {
            LoadLubanTableFailureEventArgs loadLubanTableFailureEventArgs = ReferencePool.Acquire<LoadLubanTableFailureEventArgs>();
            loadLubanTableFailureEventArgs.LoadFlagKey = loadFlagKey;
            loadLubanTableFailureEventArgs.UserData = userData;

            return loadLubanTableFailureEventArgs;
        }
        
        public override void Clear()
        {
            LoadFlagKey = string.Empty;
            UserData = null;
        }
    }
}

