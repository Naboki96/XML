using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    [InheritedExport(typeof(ISerialization))]
    public interface ISerialization
    {
        Task Serialize<T>(T objectToSave, string filename);
        Task<T> Deserialize<T>(string filename);
    }
}
