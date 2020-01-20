using NUnit.Framework;
using System;
using BowlingScoring;
using BowlingScoring.Interfaces;

namespace BowlingScoring.UnitTests
{
    public class ScoreTests
    {
        IScore bowlingscore;

        [SetUp]
        public void Setup()
        {
            //Arrange
            bowlingscore = new Score(new Frame()
            {
                FrameNumber = 1
            });
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
        public void Check_Current_Score_Gets_Set()
        {
            //Act
            bowlingscore.SetScore(5,true);
            //Assert
            Assert.IsNotNull(bowlingscore.CurrentScore == 5);
        }


    }
}