using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Mentoring.System.Extensions.Tests
{
    [TestClass]
    public class RomanNumeralsTests
    {
        [TestMethod]
        public void Returns_Correct_Value_For_One()
        {
            // Given
            var testValues = new Dictionary<int, string>
            {
                [1] = "I",
                [2] = "II",
                [3] = "III",
                [4] = "IV",
                [7] = "VII",
                [324] = "CCCXXIV",
                [1987] = "MCMLXXXVII",
                [3999] = "MMMCMXCIX",
            };

            foreach (var testValue in testValues)
            {
                var romanNumber = testValue.Key.ToRoman();
                Assert.AreEqual(testValue.Value, romanNumber);
            }
        }

        [TestMethod]
        public void Throws_Exception_For_Zero()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => 0.ToRoman());
        }

        [TestMethod]
        public void Throws_Exception_For_Negative_Numbers()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => (-450).ToRoman());
        }

        [TestMethod]
        public void Throws_Exception_If_Number_Larger_Than_3999()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => 4000.ToRoman());

        }
    }
}
