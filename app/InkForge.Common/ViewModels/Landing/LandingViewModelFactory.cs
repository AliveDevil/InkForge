using Microsoft.Extensions.DependencyInjection;

namespace InkForge.Common.ViewModels.Landing;

public class LandingViewModelFactory(IServiceProvider serviceProvider)
{
	public T Create<T>(LandingViewModel landing) where T : LandingViewModelBase
	{
		LandingViewModelsObjectParameters objectParameters = new(landing);
		return TypeFactory<LandingViewModelsObjectParameters, T>.Create(objectParameters, serviceProvider);
	}

	readonly record struct LandingViewModelsObjectParameters(
		LandingViewModel Landing
	) : IObjectParameters<LandingViewModelsObjectParameters>
	{
		public static Type[] Types => [typeof(LandingViewModel)];

		public static implicit operator object[](in LandingViewModelsObjectParameters self) => [self.Landing];
	}
}
