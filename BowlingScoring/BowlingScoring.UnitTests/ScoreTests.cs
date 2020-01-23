using NUnit.Framework;
using System;
using System.Linq;
using BowlingScoring;
using BowlingScoring.Interfaces;
using FluentAssertions;
using System.Collections.Generic;

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

        [Test]
        public void Check_IsSpare_Set()
        {
            //Act
            var frameNumber = 1;
            var score1 = 9;
            var score2 = 1;
            var firstBowl = true;

            bowlingscore.SetScore(score1, firstBowl, frameNumber);
            bowlingscore.SetScore(score2, false, frameNumber);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == frameNumber).Select(x => x.IsSpare).FirstOrDefault().Should().Be(true);
        }

        [Test]
        public void Check_IsStrike_Set()
        {
            //Act
            var frameNumber = 1;
            var score1 = 10;
            var firstBowl = true;

            bowlingscore.SetScore(score1, firstBowl, frameNumber);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == frameNumber).Select(x => x.IsStrike).FirstOrDefault().Should().Be(true);
        }

        [Test]
        public void Check_IsComplete_Set()
        {
            //Act
            var frameNumber = 1;
            var score1 = 10;
            var firstBowl = true;

            bowlingscore.SetScore(score1, firstBowl, frameNumber);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == frameNumber).Select(x => x.IsComplete).FirstOrDefault().Should().Be(true);
        }

        [Test]
        public void Check_First_IsSpare_Second_No_Strike_Check_BonusTotal()
        {
            //Arrange
            var firstframeNumber = 1;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //First Frame
            bowlingscore.SetScore(5, true, firstframeNumber);
            bowlingscore.SetScore(5, false, firstframeNumber);

            //Act
            //Second Frame
            bowlingscore.SetScore(4, true, firstframeNumber+1);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == firstframeNumber).Select(x => x.BonusTotal).FirstOrDefault().Should().Be(4);
        }

        [Test]
        public void Check_First_IsSpare_Second_No_Strike_Check_FrameTotal()
        {
            //Arrange
            var firstframeNumber = 1;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //First Frame
            bowlingscore.SetScore(5, true, firstframeNumber);
            bowlingscore.SetScore(5, false, firstframeNumber);

            //Act
            //Second Frame
            bowlingscore.SetScore(4, true, firstframeNumber + 1);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == firstframeNumber).Select(x => x.FrameTotal).FirstOrDefault().Should().Be(14);
        }

        [Test]
        public void Check_First_IsSpare_Second_Is_Strike_Check_FrameTotal()
        {
            //Arrange
            var firstframeNumber = 1;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //First Frame
            bowlingscore.SetScore(5, true, firstframeNumber);
            bowlingscore.SetScore(5, false, firstframeNumber);

            //Act
            //Second Frame
            bowlingscore.SetScore(10, true, firstframeNumber + 1);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == firstframeNumber).Select(x => x.FrameTotal).FirstOrDefault().Should().Be(20);
        }

        [Test]
        public void Check_First_IsStrike_Second_Is_Strike_Check_BonusTotal_Is_Ten()
        {
            //Arrange
            var firstframeNumber = 1;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //First Frame
            bowlingscore.SetScore(10, true, firstframeNumber);

            //Act
            //Second Frame
            bowlingscore.SetScore(10, true, firstframeNumber + 1);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == firstframeNumber).Select(x => x.BonusTotal).FirstOrDefault().Should().Be(10);
        }

        [Test]
        public void Check_First_IsStrike_Second_Is_Strike_Check_FrameTotal_Is_Zero()
        {
            //Arrange
            var firstframeNumber = 1;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //First Frame
            bowlingscore.SetScore(10, true, firstframeNumber);

            //Act
            //Second Frame
            bowlingscore.SetScore(10, true, firstframeNumber + 1);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == firstframeNumber).Select(x => x.FrameTotal).FirstOrDefault().Should().Be(0);
        }

        [Test]
        public void Check_First_IsStrike_Second_Is_Strike_And_Third_IsStrike_Check_FrameTotal_Is_Thirty()
        {
            //Arrange
            var firstframeNumber = 1;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //First Frame
            bowlingscore.SetScore(10, true, firstframeNumber);

            //Act
            //Second Frame
            bowlingscore.SetScore(10, true, firstframeNumber + 1);

            //Third Frame
            bowlingscore.SetScore(10, true, firstframeNumber + 2);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == firstframeNumber).Select(x => x.FrameTotal).FirstOrDefault().Should().Be(30);
        }

        [Test]
        public void Check_First_IsStrike_Second_Is_Strike_And_Third_Five_Check_FrameTotal_Is_TwentyFive()
        {
            //Arrange
            var firstframeNumber = 1;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //First Frame
            bowlingscore.SetScore(10, true, firstframeNumber);

            //Act
            //Second Frame
            bowlingscore.SetScore(10, true, firstframeNumber + 1);

            //Third Frame
            bowlingscore.SetScore(5, true, firstframeNumber + 2);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == firstframeNumber).Select(x => x.FrameTotal).FirstOrDefault().Should().Be(25);
        }

        [Test]
        public void Check_FrameTotal_Set_For_First_and_Second_Bowl_No_Spare()
        {
            //Arrange
            var firstframeNumber = 1;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //First Frame
            bowlingscore.SetScore(4, true, firstframeNumber);

            //Act
            bowlingscore.SetScore(5, false, firstframeNumber);

            //Assert
            playersGame.PlayersFrames.Where(x => x.FrameNumber == firstframeNumber).Select(x => x.FrameTotal).FirstOrDefault().Should().Be(9);
        }

    }
}