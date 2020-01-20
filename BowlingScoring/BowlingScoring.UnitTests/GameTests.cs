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
            Assert.Throws<ArgumentNullException>(() => bowlingGame.InitializeGameForPlayer(""), "Players name cannot be blank");
        }

        [Test]
        public void Check_Game_Set_Player_With_Name()
        {
            //Arrange
            bowlingGame = new Game();
            //Act
            bowlingGame.InitializeGameForPlayer("John");
            //Assert
            bowlingGame.PlayersGames.Should().HaveCount(1, "This should have only one entry");
        }

        [Test]
        public void Check_Game_Set_Player_With_Name_Check_Frames()
        {
            //Arrange
            bowlingGame = new Game();
            //Act
            bowlingGame.InitializeGameForPlayer("John");
            //Assert
            bowlingGame.PlayersGames[0].PlayersFrames.Should().HaveCount(10, "This should ten entries");
        }

        [Test]
        public void Check_Game_Set_Mulitple_Players()
        {
            //Arrange
            bowlingGame = new Game();
            //Act
            bowlingGame.InitializeGameForPlayer("John");
            bowlingGame.InitializeGameForPlayer("Dave");
            bowlingGame.InitializeGameForPlayer("Bob");
            bowlingGame.InitializeGameForPlayer("Steve");
            //Assert
            bowlingGame.PlayersGames.Should().HaveCount(4, "This should have only one entry");
        }

        [Test]
        public void Check_Game_Set_Mulitple_Players_Check_Name()
        {
            //Arrange
            bowlingGame = new Game();
            //Act
            bowlingGame.InitializeGameForPlayer("John");
            bowlingGame.InitializeGameForPlayer("Dave");
            bowlingGame.InitializeGameForPlayer("Bob");
            bowlingGame.InitializeGameForPlayer("Steve");
            //Assert
            bowlingGame.PlayersGames[0].Name.Should().Be("John", "This should be John");
        }

        [Test]
        public void Check_Game_To_See_If_Player_IsComplete()
        {
            //Arrange
            bowlingGame = new Game();
            bowlingGame.InitializeGameForPlayer("John");

            //Act
            bowlingGame.PlayersGames[0].PlayersFrames[9].FirstBowl = true;
            bowlingGame.PlayersGames[0].PlayersFrames[9].FirstPins = 4;
            bowlingGame.PlayersGames[0].PlayersFrames[9].FirstBowl = false;
            bowlingGame.PlayersGames[0].PlayersFrames[9].SecondPins = 3;
            //Assert
            bowlingGame.CheckPlayerIsComplete(bowlingGame.PlayersGames[0], 9);
        }
    }
}
