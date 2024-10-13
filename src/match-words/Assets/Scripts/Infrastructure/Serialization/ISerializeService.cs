namespace Infrastructure.Serialization
{
    public interface ISerializeService
    {
        public bool Serialize<T>(T obj, out string data);
        public bool Deserialize<T>(string json, out T data);
    }
}
