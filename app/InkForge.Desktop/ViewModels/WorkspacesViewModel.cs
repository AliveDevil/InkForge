using InkForge.Desktop.Managers;
using InkForge.Desktop.ViewModels.Workspaces;

using ReactiveUI;

namespace InkForge.Desktop.ViewModels;

public class WorkspacesViewModel : ReactiveObject
{
	private readonly WorkspaceManager _workspaceManager;
	private WorkspaceViewModel? _workspace;

	public WorkspaceViewModel? Workspace
	{
		get => _workspace;
		private set => this.RaiseAndSetIfChanged(ref _workspace, value);
	}

	public WorkspacesViewModel(WorkspaceManager workspaceManager)
	{
		_workspaceManager = workspaceManager;
		workspaceManager.WhenAnyValue(v => v.Workspace, v => v is null ? null : new WorkspaceViewModel(v)).BindTo(this, v => v.Workspace);
	}
}
