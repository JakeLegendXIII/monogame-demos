using Microsoft.Xna.Framework.Content.Pipeline;
using System.IO;

namespace ChopperPipelineExtensions
{
    [ContentImporter(".txt", DisplayName = "LevelImporter", DefaultProcessor = "LevelProcessor")]
    public class LevelImporter : ContentImporter<string>
    {
        public override string Import(string fileName, ContentImporterContext context)
        {
            return File.ReadAllText(fileName);
        }
    }
}
