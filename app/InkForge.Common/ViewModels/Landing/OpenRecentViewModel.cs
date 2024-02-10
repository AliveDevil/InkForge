using System.Collections.ObjectModel;

using InkForge.Common.ReactiveUI;

namespace InkForge.Common.ViewModels.Landing;

public class OpenRecentViewModel : LandingViewModelBase
{
	public override string? UrlPathSegment => null;
	private readonly ReadOnlyObservableCollection<RecentItemViewModel> recentItems;

	public ReadOnlyObservableCollection<RecentItemViewModel> RecentItems => recentItems;

	public OpenRecentViewModel(LandingViewModel landing) : base(landing)
	{
	}
}
