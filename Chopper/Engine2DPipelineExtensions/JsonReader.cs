using Microsoft.Xna.Framework.Content;
// using System.Text.Json;
using Newtonsoft.Json;

namespace Engine2DPipelineExtensions
{
    public class JsonContentTypeReader<T> : ContentTypeReader<T>
    {
        protected override T Read(ContentReader input, T existingInstance)
        {
            string json = input.ReadString();

            T result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }
    }
}
