using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace Serialization
{
    public class Serialization : ISerialization
    {
        public async Task Serialize<T>(T objectToSave, string filename)
        {
            await Task.Run(() =>
            {
                using (XmlWriter xmlReader = XmlWriter.Create(filename))
                {

                    DataContractSerializer ser = new DataContractSerializer(typeof(T), null,
                        0x7FFF /*maxItemsInObjectGraph*/,
                        false /*ignoreExtensionDataObject*/,
                        true /*preserveObjectReferences : this is where the magic happens */,
                        null /*dataContractSurrogate*/);
                    ser.WriteObject(xmlReader, objectToSave);
                }
            });
        }

        public async Task<T> Deserialize<T>(string filename)
        {
            return await Task.Run(() =>
            {
                using (XmlReader xmlReader = XmlReader.Create(filename))
                {
                    DataContractSerializer serializer =
                        new DataContractSerializer(typeof(T));
                    T theObject = (T)serializer.ReadObject(xmlReader);
                    return theObject;
                }
            });
        }
    }
}