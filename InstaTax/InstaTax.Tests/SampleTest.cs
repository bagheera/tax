using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaTax.Core;
using NUnit.Framework;

namespace InstaTax.Tests
{
    [TestFixture]
    public class SampleTest
    {
        [Test]
        public void SampleTestMethod()
        {
            SampleClass test = new SampleClass();
            Assert.AreEqual("TESTING", test.SampleMethod());
        }
    }
}
