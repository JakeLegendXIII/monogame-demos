using Microsoft.Xna.Framework.Content.Pipeline;

namespace ChopperPipelineExtensions
{
    [ContentProcessor(DisplayName = "LevelProcessor")]
    public class LevelProcessor : ContentProcessor<string, Level>
    {
        public override Level Process(string input, ContentProcessorContext context)
        {
            return new Level(input);
        }
    }
}
