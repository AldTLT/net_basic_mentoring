using Microsoft.VisualStudio.TestTools.UnitTesting;
using IntParse;

namespace IntParse_Tests
{
    [TestClass]
    public class IntParse_Test
    {
        [TestMethod]
        [DataRow("0", 0)]
        [DataRow ("1", 1)]
        [DataRow("65", 65)]
        [DataRow("093", 93)]
        [DataRow("+2561", 2561)]
        [DataRow("95784", 95784)]
        [DataRow("+100000", 100000)]
        [DataRow("8933934", 8933934)]
        [DataRow("94030034", 94030034)]
        [DataRow("+345839045", 345839045)]
        [DataRow("2147483647", int.MaxValue)]
        public void Int32_Positive_Test(string source, int expected)
        {
            var result = source.TryParseInt32(out int parsedResult);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, parsedResult);
        }

        [TestMethod]
        [DataRow("-1", -1)]
        [DataRow("-65", -65)]
        [DataRow("-093", -93)]
        [DataRow("-2561", -2561)]
        [DataRow("-95784", -95784)]
        [DataRow("-100000", -100000)]
        [DataRow("-8933934", -8933934)]
        [DataRow("-94030034", -94030034)]
        [DataRow("-345839045", -345839045)]
        [DataRow("-2147483648", int.MinValue)]
        public void Int32_Negative_Test(string source, int expected)
        {
            var result = source.TryParseInt32(out int parsedResult);

            Assert.IsTrue(result);
            Assert.AreEqual(expected, parsedResult);
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        [DataRow("_1")]
        [DataRow("ddf")]
        [DataRow("ва")]
        [DataRow("a2561")]
        [DataRow("2s")]
        [DataRow("67#")]
        [DataRow("9.0")]
        [DataRow("+-45")]
        [DataRow("=39045")]
        [DataRow("-2147483649")]
        [DataRow("2147483648")]
        [DataRow("214748364801")]
        public void Int32_Failed_Test(string source)
        {
            var result = source.TryParseInt32(out int parsedResult);

            Assert.IsFalse(result);
            Assert.AreEqual(0, parsedResult);
        }
    }
}
