using Microsoft.Extensions.DependencyInjection;

namespace InkForge.Desktop.ReactiveUI;

public interface IViewModelFactory<T, TCreator>
{
	abstract static ObjectFactory<T> CreateObjectFactory();

	abstract static TCreator GetCreator(ObjectFactory<T> factory, IServiceProvider serviceProvider);
}

public class ViewModelFactory<T, TFactory, TCreator>
	where TFactory : IViewModelFactory<T, TCreator>
	where TCreator : Delegate
{
	private static ObjectFactory<T>? s_factory;

	public TCreator CreateFactory(IServiceProvider serviceProvider)
	{
		s_factory ??= TFactory.CreateObjectFactory();
		return TFactory.GetCreator(s_factory, serviceProvider);
	}
}
