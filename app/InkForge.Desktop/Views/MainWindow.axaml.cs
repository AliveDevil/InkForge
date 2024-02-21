using Avalonia.Input;
using Avalonia.ReactiveUI;

using InkForge.Desktop.ViewModels;

namespace InkForge.Desktop.Views;

public partial class MainWindow : ReactiveWindow<AppViewModel>
{
	public MainWindow()
	{
		InitializeComponent();
	}
}
