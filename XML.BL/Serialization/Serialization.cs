using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace XML.BL.Serialization
{
    public class Serialization : ISerialization
    {
        public async Task Serialize<T>(T objectToSave, string filename)
        {
            await Task.Run(() =>
            {
                using (XmlWriter xmlReader = XmlWriter.Create(filename))
                {

                    DataContractSerializer ser = new DataContractSerializer(type: typeof(T), knownTypes: null,
                        maxItemsInObjectGraph: 0x7FFF,
                        ignoreExtensionDataObject: false,
                        preserveObjectReferences: true,
                        dataContractSurrogate: null);
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