using Avalonia.ReactiveUI;

using InkForge.Common.ViewModels;

namespace InkForge.Desktop.Views;

public partial class MainWindow : ReactiveWindow<AppViewModel>
{
	public MainWindow()
	{
		InitializeComponent();
	}
}
