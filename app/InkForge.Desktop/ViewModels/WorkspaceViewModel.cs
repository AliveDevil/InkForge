using InkForge.Desktop.Models;

using ReactiveUI;

namespace InkForge.Desktop.ViewModels;

public class WorkspaceViewModel : ReactiveObject
{
	private readonly Workspace _workspace;

	public WorkspaceViewModel(Workspace workspace)
	{
		_workspace = workspace;
	}
}
