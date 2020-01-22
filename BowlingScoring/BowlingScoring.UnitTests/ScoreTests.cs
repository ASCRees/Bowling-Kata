using NUnit.Framework;
using System;
using System.Linq;
using BowlingScoring;
using BowlingScoring.Interfaces;
using FluentAssertions;

namespace BowlingScoring.UnitTests
{
    public class ScoreTests
    {
        IScore bowlingscore;
        IPlayersGame playersGame = new PlayersGame()
        {
            Name = "John"
        };

        [SetUp]
        public void Setup()
        {


            playersGame.BuildPlayersFrames();
            //Arrange
            bowlingscore = new Score(playersGame);
        }

        [Test]
        public void Check_If_Score_Exists()
        {     
            //Act
            Type scoreType = bowlingscore.GetType();
            //Assert
            Assert.IsNotNull(scoreType);
        }

        [Test]
        public void Check_Current_Score()
        {
            //Act
            bowlingscore.CurrentScore = 5;
            //Assert
            Assert.IsNotNull(bowlingscore.CurrentScore==5);
        }

        [Test]
        public void Check_FirstPins_Score_Gets_Set()
        {
            //Act
            var frameNumber = 1;
            var score = 5;
            var firstBowl = true;

            bowlingscore.SetScore(score, firstBowl, frameNumber);
            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == frameNumber).Select(x => x.FirstPins).FirstOrDefault().Should().Be(score);
        }

        [Test]
        public void Check_SecondPins_Score_Gets_Set()
        {
            //Act
            var frameNumber = 1;
            var score1 = 5;
            var score2 = 4;
            var firstBowl = true;

            bowlingscore.SetScore(score1, firstBowl, frameNumber);
            bowlingscore.SetScore(score2, false, frameNumber);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == frameNumber).Select(x => x.SecondPins).FirstOrDefault().Should().Be(score2);
        }

    }
}