using Microsoft.Xna.Framework.Content;
using System.Text.Json;

namespace Engine2DPipelineExtensions
{
    public class JsonContentTypeReader<AnimationData> : ContentTypeReader<AnimationData>
    {
        protected override AnimationData Read(ContentReader input, AnimationData existingInstance)
        {
            string json = input.ReadString();

            AnimationData result = JsonSerializer.Deserialize<AnimationData>(json);

            return result;
        }
    }
}
