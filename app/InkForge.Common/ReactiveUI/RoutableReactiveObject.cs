using ReactiveUI;

namespace InkForge.Common.ReactiveUI;

public abstract class RoutableReactiveObject(IScreen screen) : ReactiveObject, IRoutableViewModel
{
	public abstract string? UrlPathSegment { get; }

	public IScreen HostScreen => screen;
}
