using FluentAssertions;

namespace QuizyZunaUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //arrange
            var a = 0;

            //act
            a++;

            //assert
            a.Should().Be(1);
        }
    }
}