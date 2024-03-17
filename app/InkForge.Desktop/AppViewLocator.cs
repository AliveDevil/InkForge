using Avalonia.Controls;
using Avalonia.Controls.Templates;

using InkForge.Desktop.ViewModels;
using InkForge.Desktop.ViewModels.Documents;
using InkForge.Desktop.ViewModels.Workspaces;
using InkForge.Desktop.Views.Documents;
using InkForge.Desktop.Views.Workspaces;

using ReactiveUI;

namespace InkForge.Desktop;

public class AppViewLocator : IDataTemplate
{
	public Control? Build(object? param)
	{
#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
		return param switch
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
		{
			ViewModels.Tools.WorkspaceTool viewModel => _(new Views.Tools.WorkspaceTool(), viewModel),
			WelcomePageDocumentViewModel viewModel => _(new WelcomePageDocument(), viewModel),
			WorkspaceViewModel viewModel => _(new WorkspaceView(), viewModel),
		};

		static TView _<TView, TViewModel>(TView view, TViewModel viewModel)
			where TViewModel : class
			where TView : IViewFor<TViewModel>
		{
			view.ViewModel = viewModel;
			return view;
		}
	}

	public bool Match(object? data)
	{
		return data is
			RecentItemViewModel or
			ViewModels.Tools.WorkspaceTool or
			WelcomePageDocumentViewModel or
			WorkspaceViewModel;
	}
}
