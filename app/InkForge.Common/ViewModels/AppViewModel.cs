using InkForge.Common.Controllers;

using ReactiveUI;

namespace InkForge.Common.ViewModels;

public class AppViewModel : ReactiveObject
{
	private object _view;

	public object View
	{
		get => _view;
		set => this.RaiseAndSetIfChanged(ref _view, value);
	}

	public AppViewModel(WorkspaceController workspace)
	{
		View = new LandingViewModel();
	}
}
