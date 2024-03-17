using Avalonia;

using Dock.Model.Core;

using InkForge.Desktop.Models;
using InkForge.Desktop.ViewModels.Documents;

using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

namespace InkForge.Desktop.Managers;

public class DocumentManager
{
	private readonly IDock _documents;
	private readonly InkForgeFactory _factory;
	private readonly WelcomePageDocumentViewModel _welcomePage;
	private readonly WorkspaceManager _workspaceManager;

	public DocumentManager(WorkspaceManager workspaceManager, InkForgeFactory factory)
	{
		_workspaceManager = workspaceManager;
		_factory = factory;
		_documents = factory.GetDockable<IDock>("Documents")!;
		_welcomePage = CreateWelcomePageDocumentViewModel();
		workspaceManager.WhenAnyValue(v => v.Workspace).Subscribe(OnWorkspaceChanged);
	}

	private void OnWorkspaceChanged(Workspace? workspace)
	{
		if (workspace is null)
		{
			_factory.AddDockable(_documents, _welcomePage);
		}
		else
		{
			_factory.RemoveDockable(_welcomePage, false);
		}
	}

	private static WelcomePageDocumentViewModel CreateWelcomePageDocumentViewModel()
	{
		return ActivatorUtilities.CreateInstance<WelcomePageDocumentViewModel>(
			Application.Current!.GetValue(App.ServiceProviderProperty)
		);
	}
}
