namespace BowlingScoring.UnitTests
{
    using System;
    using System.Linq;
    using BowlingScoring.Interfaces;
    using FluentAssertions;
    using NUnit.Framework;

    public class PlayersGameTests
    {
        private IPlayersGame playersGame;

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
            playersGame.BuildPlayersFrames(); ;

            //Assert
            Assert.AreEqual(playersGame.PlayersFrames.Count, 10);
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
        public void Verify_Players_Add_Bonus_Frame_Check_Contents()
        {
            //Arrange
            playersGame.BuildPlayersFrames();

            //Act
            playersGame.AddBonusFrame();

            //Assert
            var bonusFrame = playersGame.PlayersFrames.Where(x => x.FrameNumber == 11).Select(x => new Frame() { FrameNumber = x.FrameNumber, IsBonusFrame = x.IsBonusFrame }).FirstOrDefault();

            bonusFrame.Should().BeEquivalentTo(new Frame()
            {
                FrameNumber = 11,
                IsBonusFrame = true
            }, options => options.ExcludingMissingMembers());
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
                SecondPins = 4,
                FrameTotal = 9
            });

            //Assert

            Assert.AreEqual(playersGame.PlayersFrames.Count, 11);
        }

        [Test]
        public void Verify_Players_Total()
        {
            //Arrange
            playersGame.BuildPlayersFrames();
            IScore scoreGame = new Score(playersGame);

            //Act
            scoreGame.SetScore(10, true, 1);
            scoreGame.SetScore(10, true, 2);
            scoreGame.SetScore(10, true, 3);

            //Assert
            playersGame.PlayerTotal.Should().Be(30);
        }

        [Test]
        public void Verify_PlayersTotal_Is_Zero_When_NoFrames_Setup()
        {
            //Arrange
            //Act
            //Assert
            playersGame.PlayerTotal.Should().Be(0);
        }
    }
}