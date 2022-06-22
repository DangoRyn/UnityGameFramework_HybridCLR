using System;
using System.Threading.Tasks;
using Bright.Serialization;
using SimpleJSON;

namespace Game
{
    public class BaseLubanTables : ILubanTables
    {
        public virtual LubanTableType GetTableType { get; }

        public virtual Task LoadAsync(Func<string, Task<ByteBuf>> loader)
        {
            return null;
        }

        public virtual Task LoadAsync(Func<string, Task<JSONNode>> loader)
        {
            return null;
        }

        public virtual void TranslateText(Func<string, string, string> translator)
        {
            
        }
    }
}

