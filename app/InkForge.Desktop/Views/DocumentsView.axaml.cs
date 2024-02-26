using Avalonia;
using Avalonia.Controls;

using InkForge.Desktop.ViewModels;

using Microsoft.Extensions.DependencyInjection;

namespace InkForge.Desktop.Views;

public partial class DocumentsView : UserControl
{
	public DocumentsView()
	{
		InitializeComponent();
		DataContext = CreateViewModel();
	}

	private static DocumentsViewModel CreateViewModel()
	{
		return ActivatorUtilities.CreateInstance<DocumentsViewModel>(
			Application.Current!.GetValue(App.ServiceProviderProperty)
		);
	}
}
