using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI;

namespace InkForge.Desktop.Dock;

public class WorkspaceFactory : Factory
{
	public override IRootDock CreateLayout()
	{
		var documents = CreateDocumentDock();
		documents.Id = "Documents";
		documents.Title = "Documents";

		var root = CreateRootDock();

		root.VisibleDockables = [documents];
		root.ActiveDockable = documents;
		root.DefaultDockable = documents;

		DockableLocator = new Dictionary<string, Func<IDockable?>>
		{
			["Root"] = () => root,
			["Documents"] = () => documents,
		};

		return root;
	}
}
