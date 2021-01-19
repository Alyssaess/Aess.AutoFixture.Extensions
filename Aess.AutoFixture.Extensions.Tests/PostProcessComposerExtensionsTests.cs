using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoFixture;
using FluentAssertions;
using ParkSquare.Testing.Generators;
using Xunit;
using StringGenerator = ParkSquare.Testing.Generators.StringGenerator;

namespace Aess.AutoFixture.Extensions.Tests
{
    [ExcludeFromCodeCoverage]
    public class PostProcessComposerExtensionsTests
    {
        private readonly Fixture _sut = new Fixture();

        [Fact]
        public void With_WhenPropertyValueIsPassedIn_PropertyHasValue()
        {
            var count = IntegerGenerator.AnyIntegerInRange(2,100);
            var value = StringGenerator.AnyNonNullString();

            var result = _sut.BuildMany<TestClass>(count)
                .With(r => r.StringProperty, value);
            
            result.Select(r=>r.Create().StringProperty).All(r=>r.Equals(value)).Should().BeTrue();
        }
        [Fact]
        public void With_WhenPropertyFunctionIsPassedIn_FuncIsExecutedCorrectTimes()
        {
            var count = IntegerGenerator.AnyIntegerInRange(2,100);
            var executed = 0;

            string ValueFunction()
            {
                executed++;
                return StringGenerator.AnyNonNullString();
            }

            var result = _sut.BuildMany<TestClass>(count)
                .With(r => r.StringProperty, ValueFunction);

            result.ForEach(r => r.Create());
            executed.Should().Be(count);
        }
        [Fact]
        public void Without_PropertyIsNotSet()
        {
            var count = IntegerGenerator.AnyIntegerInRange(2,100);

            var result = _sut.BuildMany<TestClass>(count)
                .Without(r => r.StringProperty);

            result.Select(r=>r.Create().StringProperty)
                .All(r=>r==null).Should().BeTrue();
        }
        [Fact]
        public void Create_ShouldReturnCorrectLength()
        {
            var count = IntegerGenerator.AnyIntegerInRange(2,100);

            var result = _sut.BuildMany<TestClass>(count)
                .Create();

            result.Should().HaveCount(count); 
        }
        [Fact]
        public void WithAutoProperties_ShouldSetAutoProperties()
        {
            _sut.OmitAutoProperties = true;
            var count = IntegerGenerator.AnyIntegerInRange(2,100);

            var result = _sut.BuildMany<TestClass>(count)
                .WithAutoProperties();

            result.Select(r => r.Create().StringProperty)
                .Any(r => r == null).Should().BeFalse();
            
        }
        [Fact]
        public void WithAutoProperties_ShouldNotSetAutoProperties()
        {
            _sut.OmitAutoProperties = false;
            var count = IntegerGenerator.AnyIntegerInRange(2,100);

            var result = _sut.BuildMany<TestClass>(count)
                .OmitAutoProperties();

            result.Select(r => r.Create().StringProperty)
                .All(r => r == null).Should().BeTrue();

        }
    }
}