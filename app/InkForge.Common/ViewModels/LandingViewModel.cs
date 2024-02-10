using System.Reactive.Linq;

using InkForge.Common.ViewModels.Landing;

using Microsoft.Extensions.DependencyInjection;

using ReactiveUI;

namespace InkForge.Common.ViewModels;

public class LandingViewModel : ReactiveObject, IScreen
{
	private readonly LandingViewModelFactory _factory;

	public RoutingState Router { get; } = new();

	public LandingViewModel(LandingViewModelFactory factory)
	{
		_factory = factory;

		Router.CurrentViewModel.Where(x => x is null)
			.SelectMany(Observable.Return(factory.Create<OpenRecentViewModel>(this)))
			.InvokeCommand<IRoutableViewModel>(Router.NavigateAndReset);
	}

	public void Navigate<T>() where T : LandingViewModelBase
	{
	}
}
