using Dock.Model.Controls;

using InkForge.Desktop.Dock;
using InkForge.Desktop.Managers;
using InkForge.Desktop.ViewModels.Documents;

namespace InkForge.Desktop.ViewModels;

public class DocumentsViewModel
{
	private readonly WorkspaceFactory _workspaceFactory;

	public IRootDock Layout { get; }

	public DocumentsViewModel(WorkspaceFactory workspaceFactory, WorkspaceManager workspaceManager)
	{
		_workspaceFactory = workspaceFactory;

		Layout = workspaceFactory.CreateLayout();
		var documents = workspaceFactory.GetDockable<IDocumentDock>("Documents")!;
		workspaceFactory.AddDockable(documents, new WelcomePageDocumentViewModel(workspaceManager));
	}
}
