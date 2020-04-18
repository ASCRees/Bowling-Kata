namespace BowlingScoring.UnitTests
{
    using BowlingScoring;
    using BowlingScoring.Interfaces;
    using FluentAssertions;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class BowlTests
    {
        private IBowl bowlball;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //Arrange
            bowlball = new Bowl();
        }

        [Test]
        public void Check_If_Bowl_Exists()
        {
            //Act
            Type bowlType = bowlball.GetType();
            //Assert
            Assert.IsNotNull(bowlType);
        }

        [Test]
        public void Check_Bowl_Values()
        {
            //Act
            Type bowlType = bowlball.GetType();
            //Assert
            bowlball.BowlBall(10).Should().BeInRange(0, 10, "because it cannot be outside the range 0-10");
        }

        [TestCase(10)]
        public void Verify_Bowl_Does_Not_Return_More_Than_Available_Pins(Int32 availablePins)
        {
            //Act
            //Assert
            bowlball.BowlBall(availablePins).Should().BeLessOrEqualTo(availablePins);
        }

        [TestCase(10, TestName = "Verify_Bowl_Returns_Between_Zero_And. 10 Pins")]
        [TestCase(5, TestName = "Verify_Bowl_Returns_Between_Zero_And. 5 Pins")]
        [TestCase(0, TestName = "Verify_Bowl_Returns_Between_Zero_And. 0 Pins")]
        public void Verify_Bowl_Returns_Between_Zero_and_X_Pins(Int32 availablePins)
        {
            //Act
            //Assert
            bowlball.BowlBall(availablePins).Should().BeInRange(0, availablePins, "because you can only knock down " + availablePins + " pins");
        }

    }
}
