using System;
using Newtonsoft.Json;
using UnityEngine;

namespace Infrastructure.Serialization
{
    public class JsonSerializeService : ISerializeService
    {
        public bool Serialize<T>(T obj, out string data)
        {
            try
            {
                data = JsonConvert.SerializeObject(obj);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to serialize object: " + e.Message);

                data = null;
                return false;
            }
        }

        public bool Deserialize<T>(string json, out T data)
        {
            if (string.IsNullOrEmpty(json))
            {
                data = default;
                return false;
            }

            try
            {
                data = JsonConvert.DeserializeObject<T>(json);
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError("Failed to deserialize object: " + e.Message);

                data = default;
                return false;
            }
        }
    }
}
