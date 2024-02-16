using Avalonia.ReactiveUI;

using InkForge.Desktop.ViewModels;

namespace InkForge.Desktop.Views;

public partial class WorkspaceView : ReactiveUserControl<WorkspaceViewModel>
{
	public WorkspaceView()
	{
		InitializeComponent();
	}
}
