using InkForge.Common.ReactiveUI;

namespace InkForge.Common.ViewModels.Landing;

public class OpenRecentViewModel : LandingViewModelBase
{
	public override string? UrlPathSegment => null;

	public OpenRecentViewModel(LandingViewModel landing) : base(landing)
	{
	}
}
