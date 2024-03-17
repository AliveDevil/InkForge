using Dock.Model.Core;
using Dock.Model.ReactiveUI.Controls;

namespace InkForge.Desktop.ViewModels;

public class InkForgeDocumentDock : DocumentDock, IDock
{
	bool IDock.IsEmpty
	{
		get => false;
		set { }
	}
}
