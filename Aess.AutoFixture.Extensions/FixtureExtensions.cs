using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using AutoFixture.Dsl;

namespace Aess.AutoFixture.Extensions
{
    public static class FixtureExtensions
    {
        public static IEnumerable<ICustomizationComposer<T>> BuildMany<T>(this Fixture fixture, int count = 3)
        {
            return Enumerable.Repeat(fixture.Build<T>(), count);
        }
    }
}