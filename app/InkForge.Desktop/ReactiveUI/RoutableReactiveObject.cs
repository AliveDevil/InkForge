using ReactiveUI;

namespace InkForge.Desktop.ReactiveUI;

public abstract class RoutableReactiveObject(IScreen screen) : ReactiveObject, IRoutableViewModel
{
	public abstract string? UrlPathSegment { get; }

	public IScreen HostScreen => screen;
}
