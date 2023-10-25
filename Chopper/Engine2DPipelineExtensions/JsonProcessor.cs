using Microsoft.Xna.Framework.Content.Pipeline;
using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using TInput = System.String;
using TOutput = Engine2DPipelineExtensions.JsonContentProcessorResult;

namespace Engine2DPipelineExtensions
{
    [ContentProcessor(DisplayName = "Json Processor - JakeLegendXIII")]
    public class JsonProcessor : ContentProcessor<TInput, TOutput>
    {
        [DisplayName("Minify JSON")]
        public bool Minify { get; set; } = true;

        [DisplayName("Runtime Type")]
        public string RuntimeType { get; set; } = string.Empty;

        public override TOutput Process(string input, ContentProcessorContext context)
        {
            if (string.IsNullOrEmpty(RuntimeType))
            {
                throw new InvalidContentException("No Runtime Type was specified for this content.");
            }

            if (Minify)
            {
                input = MinifyJson(input);
            }

            TOutput result = new();
            result.Json = input;
            result.RuntimeIdentifier = RuntimeType;

            return result;
        }

        private string MinifyJson(string input)
        {
            JsonWriterOptions options = new JsonWriterOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                Indented = false
            };

            JsonDocument doc = JsonDocument.Parse(input);

            using (MemoryStream stream = new MemoryStream())
            {
                using (Utf8JsonWriter writer = new Utf8JsonWriter(stream, options))
                {
                    doc.WriteTo(writer);
                    writer.Flush();
                }

                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
