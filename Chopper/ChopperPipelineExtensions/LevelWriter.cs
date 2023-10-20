using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace ChopperPipelineExtensions
{
    [ContentTypeWriter]
    public class LevelWriter : ContentTypeWriter<Level>
    {
        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "ChopperPipelineExtensions.LevelReader, ChopperPipelineExtensions";
        }

        protected override void Write(ContentWriter output, Level value)
        {
            output.Write(value.LevelStringEncoding);
        }
    }
}
