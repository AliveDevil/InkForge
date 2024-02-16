using System.Text.Json.Serialization;

namespace InkForge.Desktop.Properties;

[JsonSerializable(typeof(ApplicationSettings))]
[JsonSerializable(typeof(IDictionary<string, object>))]
[JsonSourceGenerationOptions(GenerationMode = JsonSourceGenerationMode.Metadata)]
public partial class ConfigContext : JsonSerializerContext;
