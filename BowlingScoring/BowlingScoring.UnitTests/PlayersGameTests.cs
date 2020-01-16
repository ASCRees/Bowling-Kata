using NUnit.Framework;
using System;
using BowlingScoring;
using BowlingScoring.Interfaces;

namespace BowlingScoring.UnitTests
{
    public class PlayersGameTests
    {
        IPlayersGame playersGame;

        [SetUp]
        public void Setup()
        {
            //Arrange
            playersGame = new PlayersGame();
        }

        [Test]
        public void Check_If_PlayersGame_Exists()
        {
            //Act
            Type pGame = playersGame.GetType();
            //Assert
            Assert.IsNotNull(playersGame);
        }
    }
}
