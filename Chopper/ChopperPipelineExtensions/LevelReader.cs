using Microsoft.Xna.Framework.Content;

namespace ChopperPipelineExtensions
{
    public class LevelReader : ContentTypeReader<Level>
    {
        protected override Level Read(ContentReader input, Level existingInstance)
        {
            return new Level(input.ReadString());
        }
    }
}
