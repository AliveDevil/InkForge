using InkForge.Common.ReactiveUI;

namespace InkForge.Common.ViewModels.Landing;

public abstract class LandingViewModelBase(LandingViewModel landing) : RoutableReactiveObject(landing)
{
	public LandingViewModel Landing => landing;
}
