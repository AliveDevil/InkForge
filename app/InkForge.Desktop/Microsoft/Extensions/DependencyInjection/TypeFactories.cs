namespace Microsoft.Extensions.DependencyInjection
{
	public static class TypeFactory
	{
		public static T Create<T>(IServiceProvider serviceProvider)
		{
			return TypeFactory<EmptyArguments, T>.Create(serviceProvider, default);
		}
	}

	public static class TypeFactory<TArguments, T>
		where TArguments : IFactoryArguments<TArguments>
	{
		private static ObjectFactory<T>? s_objectFactory;

		public static T Create(IServiceProvider serviceProvider, in TArguments factory)
		{
			s_objectFactory ??= ActivatorUtilities.CreateFactory<T>(TArguments.Types);
			return s_objectFactory(serviceProvider, (object[])factory);
		}
	}

	public readonly struct EmptyArguments : IFactoryArguments<EmptyArguments>
	{
		public static Type[] Types => [];

		public static implicit operator object[](in EmptyArguments _) => [];
	}

	public interface IFactoryArguments<T>
		where T : IFactoryArguments<T>
	{
		abstract static Type[] Types { get; }

		abstract static implicit operator object[](in T self);
	}
}
