using InkForge.Desktop.Models;

using Microsoft.Extensions.DependencyInjection;

namespace InkForge.Desktop.ViewModels.Workspaces
{
	public class WorkspaceViewModel
	{
		private readonly NoteStore _noteStore;
		// private readonly ObservableAsPropertyHelper<string> _workspaceNameProperty;

		// public string WorkspaceName => _workspaceNameProperty.Value;

		// public ReactiveCommand<Unit, Unit> AddDocument { get; }

		public WorkspaceViewModel(NoteStore noteStore)
		{
			_noteStore = noteStore;
		}

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

	public interface IWorkspaceViewModelFactory
	{
		WorkspaceViewModel Create(Workspace workspace);
	}

	namespace Internal
	{
		internal class WorkspaceViewModelFactory(IServiceProvider services) : IWorkspaceViewModelFactory
		{
			private static ObjectFactory<WorkspaceViewModel>? s_workspaceViewModelFactory;

			public WorkspaceViewModel Create(Workspace workspace)
			{
				s_workspaceViewModelFactory ??= ActivatorUtilities.CreateFactory<WorkspaceViewModel>([typeof(Workspace)]);
				return s_workspaceViewModelFactory(services, [workspace]);
			}

			WorkspaceViewModel IWorkspaceViewModelFactory.Create(Workspace workspace) => Create(workspace);
		}
	}
}
