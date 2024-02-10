using ReactiveUI;

namespace InkForge.Common.ViewModels.Landing;

public record class RecentItemViewModel(
	DateTimeOffset Created,
	string Name,
	DateTimeOffset LastUsed
) : ReactiveRecord;
