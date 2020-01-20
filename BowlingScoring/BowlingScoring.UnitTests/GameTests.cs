using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace BowlingScoring.UnitTests
{
    using NUnit.Framework;
    using System;
    using BowlingScoring;
    using BowlingScoring.Interfaces;

    [TestFixture]
    public class GameTests
    {
        IGame bowlingGame;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //Arrange
            bowlingGame = new Game();
        }

        [Test]
        public void Check_If_Game_Exists()
        {
            //Act
            Type gameType = bowlingGame.GetType();

            //Assert
            Assert.IsNotNull(gameType);
        }

        [Test]
        public void Check_Game_Blank_Player_Throws_Error()
        {
            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(()=> bowlingGame.InitializeGameForPlayer(""),"Players name cannot be blank");
        }

        [Test]
        public void Check_Game_Set_Player_With_Name()
        {
            //Act
            bowlingGame.InitializeGameForPlayer("John");
            //Assert
            bowlingGame.PlayersGames.Should().HaveCount(1, "This should have only one entry");
        }
    }
}
