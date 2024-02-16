namespace Microsoft.Extensions.DependencyInjection
{
	public static class TypeFactory<TFactory, T>
		where TFactory : struct, IObjectParameters<TFactory>
	{
		private static ObjectFactory<T>? s_objectFactory;

		public static T Create(in TFactory factory, IServiceProvider serviceProvider)
		{
			s_objectFactory ??= ActivatorUtilities.CreateFactory<T>(TFactory.Types);
			return s_objectFactory(serviceProvider, (object[])factory);
		}
	}

	public interface IObjectParameters<T>
		where T : struct, IObjectParameters<T>
	{
		abstract static Type[] Types { get; }

		abstract static implicit operator object[](in T self);
	}
}
