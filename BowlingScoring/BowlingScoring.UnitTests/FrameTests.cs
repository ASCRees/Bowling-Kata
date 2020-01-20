namespace BowlingScoring.UnitTests
{
    using NUnit.Framework;
    using System;
    using BowlingScoring;
    using BowlingScoring.Interfaces;

    [TestFixture]
    public class FrameTests
    {
        IFrame bowlingFrame;

        [SetUp]
        public void Setup()
        {
            //Arrange
            bowlingFrame = new Frame();
        }

        [Test]
        public void Check_If_Frame_Exists()
        {
            //Act
            Type frameType = bowlingFrame.GetType();

            //Assert
            Assert.IsNotNull(frameType);
        }

        [Test]
        public void Check_Frame_First_Score_Value_Exists()
        {
            //Act
            bowlingFrame.FirstScore = 7;
            //Assert
            Assert.IsTrue(bowlingFrame.FirstScore == 7);
        }

        [Test]
        public void Check_Frame_First_Score_Errors_For_Values_Greater_Than_Ten()
        {
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(()=>bowlingFrame.FirstScore = 11);
        }

        [Test]
        public void Check_Frame_First_Score_Errors_For_Values_Less_Than_Zero()
        {
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => bowlingFrame.FirstScore = -1);
        }

        [Test]
        public void Check_Frame_First_Score_Set_Values_For_Valid_Valid()
        {
            //Act
            bowlingFrame.FirstScore = 9;

            //Assert
            Assert.AreEqual(bowlingFrame.FirstScore, 9);
        }

        [Test]
        public void Check_Frame_Is_First_Bowl()
        {
            //Act
            bowlingFrame.FirstBowl = true;
            //Assert
            Assert.IsTrue(bowlingFrame.FirstBowl);
        }

        [Test]
        public void Check_Frame_Is_Strike()
        {
            //Act
            bowlingFrame.FirstScore=10;
            //Assert
            Assert.IsTrue(bowlingFrame.IsStrike);
        }

        [Test]
        public void Check_Frame_Is_Not_Strike()
        {
            //Act
            bowlingFrame.FirstScore = 9;
            //Assert
            Assert.IsFalse(bowlingFrame.IsStrike);
        }

        [Test]
        public void Check_Frame_Is_Spare()
        {
            //Act
            bowlingFrame.FirstScore = 8;
            bowlingFrame.FirstBowl = false;
            bowlingFrame.SecondScore = 2;
            //Assert
            Assert.IsTrue(bowlingFrame.IsSpare);
        }

        [Test]
        public void Check_Frame_Is_Not_Spare()
        {
            //Act
            bowlingFrame.FirstScore = 8;
            bowlingFrame.FirstBowl = false;
            bowlingFrame.SecondScore = 1;
            //Assert
            Assert.IsFalse(bowlingFrame.IsSpare);
        }

        [Test]
        public void Check_Frame_Second_Score_Can_Be_Set()
        {
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => bowlingFrame.SecondScore = 11);
        }

        [Test]
        public void Check_Frame_Second_Score_Errors_For_Values_Greater_Than_Ten()
        {
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => bowlingFrame.SecondScore = 11, "Score can only be between 0 and 10");
        }

        [Test]
        public void Check_Frame_Second_Score_Errors_For_Values_Less_Than_Zero()
        {
            //Act
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => bowlingFrame.SecondScore = -1, "Score can only be between 0 and 10");
        }

        [Test]
        public void Check_Frame_Second_Score_Set_Values_For_Valid_Valid()
        {
            //Act
            bowlingFrame.SecondScore = 9;

            //Assert
            Assert.AreEqual(bowlingFrame.SecondScore, 9);
        }

        [Test]
        public void Check_Frame_SubTotal_Is_Sum_Of_First_And_SecondScore()
        {
            //Act
            bowlingFrame.FirstScore = 8;
            bowlingFrame.FirstBowl = false;
            bowlingFrame.SecondScore = 1;

            //Assert
            Assert.AreEqual(bowlingFrame.SubTotal, 9);
        }

        [Test]
        public void Check_Frame_Number_In_Range()
        {
            //Act

            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => bowlingFrame.FrameNumber = 0, "Frame Number can only be between 1 and 11");
        }

        [TestCase(0,null,ExpectedResult = false,TestName ="Frame Is Not Complete. First bowl Only - 0")]
        [TestCase(0, 0, ExpectedResult = true, TestName = "Frame Is Complete. Zero on both")]
        [TestCase(null, null, ExpectedResult = false, TestName = "Frame Is Not Complete. Frame has not run")]
        [TestCase(10, null, ExpectedResult = true, TestName = "Frame Is Complete. Strike")]
        [TestCase(9, 1, ExpectedResult = true, TestName = "Frame Is Complete. Spare")]
        [TestCase(5, 4, ExpectedResult = true, TestName = "Frame Is Complete. Nine")]
        [TestCase(3, null, ExpectedResult = false, TestName = "Frame Is Not Complete. First Bowl Only - 3")]
        public bool Check_Frame_IsComplete(Int32 firstScore,Int32? secondScore)
        {
            //Act
            bowlingFrame.FirstBowl = true;
            bowlingFrame.FirstScore = firstScore;
            if (secondScore != null)
            {
                bowlingFrame.FirstBowl = false;
                bowlingFrame.SecondScore = Convert.ToInt32(secondScore);
            }

            //Assert
            return bowlingFrame.IsComplete;
        }
    }
}
