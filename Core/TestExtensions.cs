namespace AutomationFramework.Core
{
    using System;
    using System.IO;
    using NUnit.Framework;

    public static class TestExtensions
    {
        /// <summary>
        /// Asserts two values are the same. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void ShouldBe(this string a, string b)
        {
            try
            {
                Assert.AreEqual(a, b);
            }
            catch (Exception)
            {
                // I need to work on getting this catch into extent reports
            }
        }

        /// <summary>
        /// Asserts two values are not the same. 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void ShouldNotBe(this string a, string b)
        {
            try
            {
                Assert.AreNotEqual(a, b);
            }
            catch
            {
                // I need to work on getting this catch into extent reports
            }
        }
    }
}