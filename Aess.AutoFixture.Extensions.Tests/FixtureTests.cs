using System.Diagnostics.CodeAnalysis;
using Aess.AutoFixture.Extensions;
using AutoFixture;
using FluentAssertions;
using JetBrains.Annotations;
using ParkSquare.Testing.Generators;
using Xunit;

namespace Aess.AutoFIxture.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class FixtureTests
    {
        private readonly Fixture _sut = new();

        [Fact]
        public void BuildMany_WhenCountNotSpecified_ShouldReturnEnumerableOfLength3()
        {
            var result = _sut.BuildMany<TestClass>();

            result.Should().HaveCount(3);
        }

        [Fact]
        public void BuildMany_WhenCountIsSpecified_ShouldReturnEnumerableOfSpecifiedLength()
        {
            var length = IntegerGenerator.AnyPositiveInteger();
            var result = _sut.BuildMany<TestClass>(length);

            result.Should().HaveCount(length);
        }
    }
}