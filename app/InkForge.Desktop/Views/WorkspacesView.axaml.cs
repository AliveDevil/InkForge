using Avalonia.Controls;

using InkForge.Desktop.ViewModels;

using Splat;

namespace InkForge.Desktop.Views;

public partial class WorkspacesView : UserControl
{
	public WorkspacesView()
	{
		InitializeComponent();
		DataContext = Locator.Current.GetService<WorkspacesViewModel>();
	}
}
