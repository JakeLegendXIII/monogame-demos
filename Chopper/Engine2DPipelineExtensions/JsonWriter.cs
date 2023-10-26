using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = Engine2DPipelineExtensions.JsonProcessorResult;

namespace Engine2DPipelineExtensions
{
    [ContentTypeWriter]
    public class JsonTypeWriter : ContentTypeWriter<TInput>
    {
        private string _runtimeIdentifier;

        protected override void Write(ContentWriter output, TInput value)
        {
            _runtimeIdentifier = value.RuntimeIdentifier;
            output.Write(value.Json);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            
            return _runtimeIdentifier;
        }
    }
}
