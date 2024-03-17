using Dock.Model.ReactiveUI.Controls;

using InkForge.Desktop.Managers;
using InkForge.Desktop.ViewModels.Workspaces;

using ReactiveUI;

namespace InkForge.Desktop.ViewModels.Tools;

public class WorkspaceTool : Tool
{
	private WorkspaceViewModel? _workspace;

	public WorkspaceViewModel? Workspace
	{
		get => _workspace;
		private set => this.RaiseAndSetIfChanged(ref _workspace, value);
	}

	public WorkspaceTool(WorkspaceManager workspaceManager, IWorkspaceViewModelFactory workspaceViewModelFactory)
	{
		Title = "Workspace";
		CanClose = false;

		workspaceManager.WhenAnyValue(v => v.Workspace,
			v => v switch
			{
				{ } => workspaceViewModelFactory.Create(v),
				_ => null
			}).BindTo(this, v => v.Workspace);
	}
}
