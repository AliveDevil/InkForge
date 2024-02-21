using InkForge.Desktop.Models;

using ReactiveUI;

namespace InkForge.Desktop.ViewModels;

public class WorkspaceViewModel : ReactiveObject
{
	private readonly Workspace _workspace;
	private readonly ObservableAsPropertyHelper<string> _workspaceNameProperty;

	public string WorkspaceName => _workspaceNameProperty.Value;

	public WorkspaceViewModel(Workspace workspace)
	{
		_workspace = workspace;
		_workspaceNameProperty = this.WhenAnyValue(v => v._workspace.Name).ToProperty(this, nameof(WorkspaceName));
	}
}
