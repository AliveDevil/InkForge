using InkForge.Desktop.Models;

namespace InkForge.Desktop.ViewModels.Workspaces;

public class WorkspaceViewModel(Workspace workspace)
{
	// private readonly Workspace _workspace;
	// private readonly ObservableAsPropertyHelper<string> _workspaceNameProperty;

	// public string WorkspaceName => _workspaceNameProperty.Value;

	// public ReactiveCommand<Unit, Unit> AddDocument { get; }

	// public WorkspacesViewModel(Workspace workspace)
	// {
	// 	_workspace = workspace;
	// 	_workspaceNameProperty = this.WhenAnyValue(v => v._workspace.Name).ToProperty(this, nameof(WorkspaceName));

	// 	AddDocument = ReactiveCommand.Create(OnAddDocument);
	// }

	// private void OnAddDocument()
	// {

	// }
}
