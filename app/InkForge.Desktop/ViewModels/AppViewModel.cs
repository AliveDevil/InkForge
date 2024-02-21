using InkForge.Desktop.Managers;
using InkForge.Desktop.Models;

using ReactiveUI;

namespace InkForge.Desktop.ViewModels;

public class AppViewModel : ReactiveObject
{
	private readonly LandingViewModel _landingViewModel;
	private readonly WorkspaceManager _workspace;
	private object _view;

	public object View
	{
		get => _view;
		set => this.RaiseAndSetIfChanged(ref _view, value);
	}

	public AppViewModel(WorkspaceManager workspace, LandingViewModel landingViewModel)
	{
		_workspace = workspace;
		_landingViewModel = landingViewModel;

		this.WhenAnyValue(v => v._workspace.Workspace).Subscribe(OnWorkspaceChanged);
	}

	private void OnWorkspaceChanged(Workspace workspace)
	{
		View = workspace switch
		{
			null => _landingViewModel,
			{ } => new WorkspaceViewModel(workspace) // scoped?
		};
	}
}
