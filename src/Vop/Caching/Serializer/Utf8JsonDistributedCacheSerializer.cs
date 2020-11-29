using System.Text;

namespace Vop.Api.Caching
{
    public class Utf8JsonDistributedCacheSerializer : IDistributedCacheSerializer
    {
        public byte[] Serialize<T>(T obj)
        {
            return Encoding.UTF8.GetBytes(JsonHelper.Serialize(obj));
        }

        public T Deserialize<T>(byte[] bytes)
        {
            return JsonHelper.Deserialize<T>(Encoding.UTF8.GetString(bytes));
        }
    }
}