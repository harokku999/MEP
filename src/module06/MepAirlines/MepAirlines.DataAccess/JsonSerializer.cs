using Newtonsoft.Json;

namespace MepAirlines.DataAccess
{
    public interface IJsonSerializer
    {
        TModel Deserialize<TModel>(string json);
        string Serialize(object obj);
    }

    public sealed class JsonSerializer : IJsonSerializer
    {
        public TModel Deserialize<TModel>(string json) =>
            JsonConvert.DeserializeObject<TModel>(json);

        public string Serialize(object obj) =>
            JsonConvert.SerializeObject(obj);
    }
}
