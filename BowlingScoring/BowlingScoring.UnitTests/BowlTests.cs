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


        [SetUp]
        public void Setup()
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
    }
}
