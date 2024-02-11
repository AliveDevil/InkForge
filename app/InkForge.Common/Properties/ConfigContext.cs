using System.Text.Json.Serialization;

namespace InkForge.Common.Properties;

[JsonSerializable(typeof(ApplicationSettings))]
[JsonSerializable(typeof(IDictionary<string, object>))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
public partial class ConfigContext : JsonSerializerContext;
