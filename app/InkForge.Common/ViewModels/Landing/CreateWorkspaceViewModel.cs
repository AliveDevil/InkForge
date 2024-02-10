using InkForge.Common.ReactiveUI;

using ReactiveUI;

namespace InkForge.Common.ViewModels.Landing;

public class CreateWorkspaceViewModel : LandingViewModelBase
{
	public override string? UrlPathSegment => null;

	public CreateWorkspaceViewModel(LandingViewModel landing) : base(landing)
	{
	}
}
