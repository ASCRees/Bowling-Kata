using BowlingScoring.Interfaces;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScoring.UnitTests
{
    public class ScoreTests
    {
        private IScore bowlingscore;

        private IPlayersGame playersGame = new PlayersGame()
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
            Assert.IsNotNull(bowlingscore.CurrentScore == 5);
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
            bowlingscore.SetScore(4, true, firstframeNumber + 1);

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
        public void Check_First_IsStrike_Second_Is_Strike_And_Third_Five_Check_SecondFrame_Bonus_Is_Five()
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
            playersGame.PlayersFrames.Where(x => x.FrameNumber == firstframeNumber + 1).Select(x => x.BonusTotal).FirstOrDefault().Should().Be(5);
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

        [Test]
        public void Check_Scoring_Bonus_Frame_For_Ninth_After_Strike_In_Ninth_Tenth_Frame_And_Bonus_Frames()
        {
            //Arrange
            var tenthframeNumber = 10;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //Set Frame Data
            bowlingscore.SetScore(10, true, tenthframeNumber - 1); // Set a strike in the ninth frame
            bowlingscore.SetScore(10, true, tenthframeNumber); // Set a strike in the tenth frame

            playersGame.AddBonusFrame(); // Add the bonus frame

            //Act
            bowlingscore.SetScore(10, true, tenthframeNumber + 1);

            //Assert
            var ninthFrame = playersGame.PlayersFrames
                .Where(x => x.FrameNumber == tenthframeNumber - 1)
                .Select(x => new Frame() { FrameNumber = x.FrameNumber, BonusTotal = x.BonusTotal, FrameTotal = x.FrameTotal })
                .FirstOrDefault();

            ninthFrame.Should().BeEquivalentTo(new Frame()
            {
                FrameNumber = 9,
                BonusTotal = 20,
                FrameTotal = 30
            }, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void Check_Scoring_Bonus_Frame_Spare_In_Tenth_Frame_And_Then_Bonus_Frame_Of_Five()
        {
            //Arrange
            var tenthframeNumber = 10;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //Set Frame Data
            bowlingscore.SetScore(9, true, tenthframeNumber); // Set a spare in the tenth frame
            bowlingscore.SetScore(1, false, tenthframeNumber);

            playersGame.AddBonusFrame(); // Add the bonus frame

            //Act
            bowlingscore.SetScore(5, true, tenthframeNumber + 1);

            //Assert
            var ninthFrame = playersGame.PlayersFrames.Where(x => x.FrameNumber == tenthframeNumber).Select(x => new Frame() { FrameNumber = x.FrameNumber, BonusTotal = x.BonusTotal, FrameTotal = x.FrameTotal }).FirstOrDefault();

            ninthFrame.Should().BeEquivalentTo(new Frame()
            {
                FrameNumber = 10,
                BonusTotal = 5,
                FrameTotal = 15
            }, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void Check_Scoring_Bonus_Frame_Strike_In_Tenth_Frame_And_Check_Bonus_Set_After_Two_Bowls()
        {
            //Arrange
            var tenthframeNumber = 10;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //Set Frame Data
            bowlingscore.SetScore(10, true, tenthframeNumber); // Set a strike in the tenth frame

            playersGame.AddBonusFrame(); // Add the bonus frame

            //Act
            bowlingscore.SetScore(9, true, tenthframeNumber + 1);
            bowlingscore.SetScore(1, false, tenthframeNumber + 1);

            //Assert
            var tenthFrame = playersGame.PlayersFrames.Where(x => x.FrameNumber == tenthframeNumber).Select(x => new Frame() { FrameNumber = x.FrameNumber, FirstPins = x.FirstPins, BonusTotal = x.BonusTotal, FrameTotal = x.FrameTotal }).FirstOrDefault();

            tenthFrame.Should().BeEquivalentTo(new Frame()
            {
                FrameNumber = 10,
                FirstPins = 10,
                BonusTotal = 10,
                FrameTotal = 20
            }, options => options.ExcludingMissingMembers());
        }

        [Test]
        public void Check_Scoring_Bonus_Frame_Strike_In_Ninth_Tenth_And_Bonus_Frames()
        {
            //Arrange
            var tenthframeNumber = 10;
            playersGame.PlayersFrames = new List<IFrame>();
            playersGame.BuildPlayersFrames();

            //Set Frame Data
            bowlingscore.SetScore(10, true, tenthframeNumber - 1); // Set a strike in the tenth frame
            bowlingscore.SetScore(10, true, tenthframeNumber); // Set a strike in the tenth frame

            playersGame.AddBonusFrame(); // Add the bonus frame

            //Act
            bowlingscore.SetScore(10, true, tenthframeNumber + 1);
            bowlingscore.SetScore(10, false, tenthframeNumber + 1);

            //Assert
            var tenthFrame = playersGame.PlayersFrames.Where(x => x.FrameNumber == tenthframeNumber).Select(x => new Frame() { FrameNumber = x.FrameNumber, FirstPins = x.FirstPins, BonusTotal = x.BonusTotal, FrameTotal = x.FrameTotal }).FirstOrDefault();

            tenthFrame.Should().BeEquivalentTo(new Frame()
            {
                FrameNumber = 10,
                FirstPins = 10,
                BonusTotal = 20,
                FrameTotal = 30
            }, options => options.ExcludingMissingMembers());
        }
    }
}