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

        [Test]
        public void Verify_Players_Name_Is_Set()
        {
            //Act
            playersGame.Name = "John";

            //Assert
            Assert.AreEqual(playersGame.Name, "John");
        }

        [Test]
        public void Verify_Players_Set_Of_Frames_Built()
        {
            //Act
            playersGame.BuildPlayersFrames();;

            //Assert
            Assert.AreEqual(playersGame.PlayersFrames.Count,10);
        }

        [Test]
        public void Verify_Players_Add_Bonus_Frame()
        {
            //Arrange
            playersGame.BuildPlayersFrames();

            //Act
            playersGame.AddBonusFrame();

            //Assert
            Assert.AreEqual(playersGame.PlayersFrames.Count, 11);
        }

        [Test]
        public void Verify_Players_Total_Fifteen()
        {
            //Arrange
            playersGame.BuildPlayersFrames();

            //Act
            playersGame.PlayersFrames.Add(new Frame()
            {
                FirstPins = 5,
                SecondPins=4,
                FrameTotal=9
            });

            //

            //Assert
            Assert.AreEqual(playersGame.PlayersFrames.Count, 11);
        }

    }
}
