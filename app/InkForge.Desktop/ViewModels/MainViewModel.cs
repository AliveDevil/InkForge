using Avalonia;

using Dock.Model.Core;

using InkForge.Desktop.Managers;

using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

namespace InkForge.Desktop.ViewModels;

public class MainViewModel : ReactiveObject
{
	private readonly DocumentManager _documentManager;
	public IDock Layout { get; }

	public MainViewModel(InkForgeFactory factory)
	{
		Layout = factory.CreateLayout();
		factory.InitLayout(Layout);

		_documentManager = CreateDocumentManager();
	}

	private static DocumentManager CreateDocumentManager()
	{
		return ActivatorUtilities.CreateInstance<DocumentManager>(
			Application.Current!.GetValue(App.ServiceProviderProperty)
		);
	}
}
