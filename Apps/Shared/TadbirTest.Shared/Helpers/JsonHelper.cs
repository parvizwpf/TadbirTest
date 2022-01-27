using System.Text;
using System.Text.Json;

namespace TadbirTest.Shared.Helpers
{
    public static class JsonHelper
    {
        public static byte[] ToByteArray(this object model) => 
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(model));
    }
}
