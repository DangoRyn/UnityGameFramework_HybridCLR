using System.Threading.Tasks;
using Bright.Serialization;
using SimpleJSON;

namespace Game
{
    public interface ILubanTables
    {
        LubanTableType GetTableType { get; }
        Task LoadAsync(System.Func<string, Task<ByteBuf>> loader);
        Task LoadAsync(System.Func<string, Task<JSONNode>> loader);
        void TranslateText(System.Func<string, string, string> translator);
    }
}


