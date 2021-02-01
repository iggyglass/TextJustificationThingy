using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RopeDataStructure;

namespace UnitTests
{
    public class RopeTests
    {
        [Theory]
        [InlineData("Hello world", 5)]
        public void TestSplit(string input, int split)
        {
            Rope a = new Rope(input);
            Rope b = a.Split(split);

            string expectedA = input.Substring(0, split);
            string expectedB = input.Substring(split);

            Assert.Equal(expectedA, a.Report());
            Assert.Equal(expectedB, b.Report());
        }

        [Theory]
        [InlineData("Hello world", 0)]
        [InlineData("Hello world", 10)]
        [InlineData("Hello World", 5)]
        public void TestIndexing(string text, int index)
        {
            Rope rope = new Rope(text);

            Assert.Equal(text[index], rope[index]);
        }

        [Theory]
        [InlineData("Hello world", "world hello")]
        public void TestConcat(string a, string b)
        {
            Rope ropeA = new Rope(a);
            Rope ropeB = new Rope(b);

            ropeA.Concat(ropeB);

            Assert.Equal(a + b, ropeA.Report());
        }

        [Theory]
        [InlineData("Hello World", 2, 5)]
        [InlineData("Hello world", 0, 3)]
        public void TestDelete(string text, int start, int end)
        {
            Rope rope = new Rope(text);

            rope.Delete(start, end);

            string beginning = text.Substring(0, start);
            string temp = text.Substring(start);
            string other = temp.Substring(end - start);

            string expected = beginning + other;

            Assert.Equal(expected, rope.Report());
        }
    }
}
