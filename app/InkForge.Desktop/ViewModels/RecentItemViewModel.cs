using ReactiveUI;

namespace InkForge.Desktop.ViewModels;

public record class RecentItemViewModel(
	DateTimeOffset Created,
	string Name,
	DateTimeOffset LastUsed
) : ReactiveRecord;
