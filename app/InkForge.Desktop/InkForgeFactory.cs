using Dock.Model.ReactiveUI;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;
using Dock.Avalonia.Controls;
using Microsoft.Extensions.DependencyInjection;
using Avalonia;
using InkForge.Desktop.ViewModels;

namespace InkForge.Desktop;

public class InkForgeFactory : Factory
{
	private readonly IDocumentDock _documentDock;
	private readonly IRootDock _rootDock;
	private readonly ViewModels.Tools.WorkspaceTool _workspaceTool;

	public InkForgeFactory()
	{
		_rootDock = new RootDock
		{
			IsCollapsable = false,
		};

		_documentDock = new InkForgeDocumentDock
		{
			Id = "Documents",
			Title = "Documents",
			CanCreateDocument = false,
			IsCollapsable = false,
			Proportion = double.NaN,
		};

		_workspaceTool = CreateWorkspaceTool();
	}

	public override IRootDock CreateLayout()
	{
		ProportionalDock workspaceLayout = new()
		{
			Proportion = 0.3,
			VisibleDockables = [_workspaceTool],
		};

		ProportionalDock windowLayoutContent = new()
		{
			Orientation = Orientation.Horizontal,
			IsCollapsable = false,
			VisibleDockables = [workspaceLayout, new ProportionalDockSplitter(), _documentDock]
		};

		RootDock windowLayout = new()
		{
			Title = "Default",
			IsCollapsable = false,
			VisibleDockables = [windowLayoutContent],
			ActiveDockable = windowLayoutContent,
		};

		_rootDock.VisibleDockables = [windowLayout];
		_rootDock.ActiveDockable = windowLayout;
		_rootDock.DefaultDockable = windowLayout;

		return _rootDock;
	}

	public override void InitLayout(IDockable layout)
	{
		DockableLocator = new Dictionary<string, Func<IDockable?>>
		{
			["Root"] = () => _rootDock,
			["Documents"] = () => _documentDock,
			["Workspace"] = () => _workspaceTool,
		};

		HostWindowLocator = new Dictionary<string, Func<IHostWindow?>>
		{
			[nameof(IDockWindow)] = () => new HostWindow()
		};

		base.InitLayout(layout);
	}

	private static ViewModels.Tools.WorkspaceTool CreateWorkspaceTool()
	{
		return ActivatorUtilities.CreateInstance<ViewModels.Tools.WorkspaceTool>(
			Application.Current!.GetValue(App.ServiceProviderProperty)
		);
	}
}
