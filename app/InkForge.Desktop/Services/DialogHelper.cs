using Avalonia.Platform.Storage;

namespace InkForge.Desktop.Services;

public static class StorageProviderExtensions
{
	public static IStorageProvider? GetStorageProvider(this object? context)
	{
		ArgumentNullException.ThrowIfNull(context);

		return TopLevels.GetTopLevelForContext(context)?.StorageProvider;
	}
}
