using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApplication1
{
    public class JsonSerialisation<T> : ISerialiser<T>
        where T : class
    {

        public string ToString(T objectToSerialise)
        {
            if (objectToSerialise == null)
            {
                throw new ArgumentNullException("objectToSerialise");
            }

            string json = JsonConvert.SerializeObject(objectToSerialise);

            return json;
        }

        public byte[] ToByteArray(T objectToSerialise, bool isCompressed = false)
        {
            if (objectToSerialise == null)
            {
                throw new ArgumentNullException("objectToSerialise");
            }


            using (var memoryStream = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(memoryStream))
                {
                    using (JsonWriter jw = new JsonTextWriter(sw))
                    {

                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(jw, objectToSerialise);
                    }
                }

                return memoryStream.ToArray();
            }
        }

        public T FromString(string serialisedObject)
        {
            if (serialisedObject == null)
            {
                throw new ArgumentNullException("serialisedObject");
            }

            T toReturn = JsonConvert.DeserializeObject<T>(serialisedObject);

            return toReturn;
        }

        public T FromByteArray(byte[] serialisedObject, bool isCompressed = false)
        {
            if (serialisedObject == null)
            {
                throw new ArgumentNullException("serialisedObject");
            }

            T toReturn = null;

            using (var memoryStream = new MemoryStream(serialisedObject))
            {
                using (StreamReader sr = new StreamReader(memoryStream))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        // read the json from a stream
                        toReturn = serializer.Deserialize<T>(reader);
                    }
                }

            }

            return toReturn;
        }

        public object FromByteArray(byte[] serialisedObject, Type objectType, bool isCompressed = false)
        {
            if (serialisedObject == null)
            {
                throw new ArgumentNullException("serialisedObject");
            }

            object toReturn = null;

            using (var memoryStream = new MemoryStream(serialisedObject))
            {
                using (StreamReader sr = new StreamReader(memoryStream))
                {
                    using (JsonReader reader = new JsonTextReader(sr))
                    {
                        JsonSerializer serializer = new JsonSerializer();

                        // read the json from a stream
                        toReturn = serializer.Deserialize(reader, objectType);
                    }
                }

            }

            return toReturn;
        }
    }

    public interface ISerialiser<T>
    {
        string ToString(T objectToSerialise);
        byte[] ToByteArray(T objectToSerialise, bool isCompressed = false);
        T FromString(string serialisedObject);
        T FromByteArray(byte[] serialisedObject, bool isCompressed = false);

    }
}
