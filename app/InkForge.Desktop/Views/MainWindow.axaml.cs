using Avalonia;
using Avalonia.ReactiveUI;

using InkForge.Desktop.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace InkForge.Desktop.Views;

public partial class MainWindow : ReactiveWindow<MainViewModel>
{
	public MainWindow()
	{
		InitializeComponent();
		ViewModel = CreateViewModel();
	}

	private static MainViewModel CreateViewModel()
	{
		return ActivatorUtilities.CreateInstance<MainViewModel>(
			Application.Current!.GetValue(App.ServiceProviderProperty)
		);
	}
}
