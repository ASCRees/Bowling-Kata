using FluentAssertions;
using Moq;
using System.Linq;

namespace BowlingScoring.UnitTests
{
    using BowlingScoring;
    using BowlingScoring.Interfaces;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class GameTests
    {
        private IGame bowlingGame;

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

        [TestCase(10)]
        public void Verify_Bowl_Does_Not_Return_More_Than_Available_Pins(Int32 availablePins)
        {
            //Act
            //Assert
            bowlingGame.Bowl(availablePins).Should().BeLessOrEqualTo(availablePins);
        }

        [TestCase(10, TestName = "Verify_Bowl_Returns_Between_Zero_And. 10 Pins")]
        [TestCase(5, TestName = "Verify_Bowl_Returns_Between_Zero_And. 5 Pins")]
        [TestCase(0, TestName = "Verify_Bowl_Returns_Between_Zero_And. 0 Pins")]
        public void Verify_Bowl_Returns_Between_Zero_and_X_Pins(Int32 availablePins)
        {
            //Act
            //Assert
            bowlingGame.Bowl(availablePins).Should().BeInRange(0, availablePins, "because you can only knock down " + availablePins + " pins");
        }

        [Test]
        public void Verify_Perfect_Game()
        {
            //Arrange
            var bowlMock = new Mock<IBowl>();
            bowlMock.Setup(x => x.BowlBall(It.IsAny<Int32>())).Returns(10);

            IGame bowlingGame = new Game(bowlMock.Object);

            bowlingGame.InitializeGameForPlayer("John");

            //Act
            bowlingGame.RunGame();
            //Assert
            bowlingGame.PlayersGames.Where(x => x.Name == "John").Select(c => c.PlayerTotal).FirstOrDefault().Should().Be(300);
        }

        [Test]
        public void Verify_Ninety_Game()
        {
            //Arrange
            Int32[] sampleBowls = new Int32[20] { 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0 };
            Int32 expectedPlayersScore = 90;

            var bowlMock = new Mock<IBowl>();

            CreateMockSequenceForBowls(sampleBowls, bowlMock);

            // Create a mock sequence to specify the bowls to perform
            IGame bowlingGame = new Game(bowlMock.Object);
            bowlingGame.InitializeGameForPlayer("John");

            //Act
            bowlingGame.RunGame();

            //Assert
            bowlingGame.PlayersGames.Where(x => x.Name == "John").Select(c => c.PlayerTotal).FirstOrDefault().Should().Be(expectedPlayersScore);
        }

        [Test]
        public void Verify_One_Ninety_Game()
        {
            //Arrange
            Int32[] sampleBowls = new Int32[21] { 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9 };
            Int32 expectedPlayersScore = 190;

            var bowlMock = new Mock<IBowl>();
            CreateMockSequenceForBowls(sampleBowls, bowlMock);

            IGame bowlingGame = new Game(bowlMock.Object);
            bowlingGame.InitializeGameForPlayer("John");

            //Act
            bowlingGame.RunGame();
            //Assert
            bowlingGame.PlayersGames.Where(x => x.Name == "John").Select(c => c.PlayerTotal).FirstOrDefault().Should().Be(expectedPlayersScore);
        }

        [Test]
        public void Verify_Two_Zero_Two_Game()
        {
            //Arrange
            Int32[] sampleBowls = new Int32[21] { 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 9, 1, 10, 10, 10 };
            Int32 expectedPlayersScore = 202;

            var bowlMock = new Mock<IBowl>();
            CreateMockSequenceForBowls(sampleBowls, bowlMock);

            IGame bowlingGame = new Game(bowlMock.Object);
            bowlingGame.InitializeGameForPlayer("John");

            //Act
            bowlingGame.RunGame();
            //Assert
            bowlingGame.PlayersGames.Where(x => x.Name == "John").Select(c => c.PlayerTotal).FirstOrDefault().Should().Be(expectedPlayersScore);
        }

        private static void CreateMockSequenceForBowls(int[] sampleBowls, Mock<IBowl> bowlMock)
        {
            // Create a mock sequence to specify the bowls to perform
            var sequence = new MockSequence();
            foreach (Int32 bowl in sampleBowls)
            {
                bowlMock.InSequence(sequence).Setup(x => x.BowlBall(It.IsAny<Int32>())).Returns(bowl);
            }
        }
    }
}