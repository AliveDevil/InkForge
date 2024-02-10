using InkForge.Common.Controllers;
using InkForge.Common.ReactiveUI;

using ReactiveUI;

namespace InkForge.Common.ViewModels.Landing;

public class CreateWorkspaceViewModel : LandingViewModelBase
{
	public override string? UrlPathSegment => null;

	private string workspaceName;

	public string WorkspaceName
	{
		get => workspaceName;
		set => this.RaiseAndSetIfChanged(ref workspaceName, value);
	}

	public CreateWorkspaceViewModel(LandingViewModel landing, WorkspaceController workspace) : base(landing)
	{
	}
}
