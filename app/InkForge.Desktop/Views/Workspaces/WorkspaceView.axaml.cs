using Avalonia.ReactiveUI;

using InkForge.Desktop.ViewModels.Workspaces;

namespace InkForge.Desktop.Views.Workspaces;

public partial class WorkspaceView : ReactiveUserControl<WorkspaceViewModel>
{
	public WorkspaceView()
	{
		InitializeComponent();
	}
}
