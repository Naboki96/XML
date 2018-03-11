using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace XML.BL.Serialization
{
    [InheritedExport(typeof(ISerialization))]
    public interface ISerialization
    {
        Task Serialize<T>(T objectToSave, string filename);
        Task<T> Deserialize<T>(string filename);
    }
}
