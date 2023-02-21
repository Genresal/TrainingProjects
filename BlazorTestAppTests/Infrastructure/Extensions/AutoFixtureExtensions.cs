using AutoFixture;
using AutoFixture.Xunit2;

namespace BlazorTestAppTests.Infrastructure.Extensions;

/*
public static class AutoDataExtensions
{
	
	public static AutoDataAttribute WithOmitOnRecursionBehavior(this AutoDataAttribute autoData)
	{
		autoData.Fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
		return autoData;
	}
}
*/
/*
public class AutoDomainDataAttribute : AutoDataAttribute
{
	public AutoDomainDataAttribute() : base(() =>
		{
			var fixture = new Fixture();
			fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
				.ForEach(b => fixture.Behaviors.Remove(b));
			fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

			return fixture;
		})
	{
	}
}*/


public class AutoDataWithOmitOnRecursionAttribute : AutoDataAttribute
{
	public AutoDataWithOmitOnRecursionAttribute()
		: base(() => new Fixture().Customize(new OmitOnRecursionCustomization()))
	{
	}
}

public class OmitOnRecursionCustomization : ICustomization
{
	public void Customize(IFixture fixture)
	{
		fixture.Behaviors.Add(new OmitOnRecursionBehavior());
	}
}