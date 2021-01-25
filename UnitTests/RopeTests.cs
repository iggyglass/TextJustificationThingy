using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using RopeDataStructure;

namespace UnitTests
{
    public class RopeTests
    {

        public void TestSplit(string input, int split) // TODO: add data to tests
        {
            Rope a = new Rope(input);
            Rope b = a.Split(split);

            string expectedA = input.Substring(0, split);
            string expectedB = input.Substring(split);

            Assert.Equal(expectedA, a.Report());
            Assert.Equal(expectedB, b.Report());
        }

        public void TestIndexing(string text, int index)
        {
            Rope rope = new Rope(text);

            Assert.Equal(text[index], rope[index]);
        }

        public void TestConcat(string a, string b)
        {
            Rope ropeA = new Rope(a);
            Rope ropeB = new Rope(b);

            ropeA.Concat(ropeB);

            Assert.Equal(a + b, ropeA.Report());
        }

        public void TestDelete(string text, int start, int end)
        {
            Rope rope = new Rope(text);

            rope.Delete(start, end);

            string temp = text.Substring(start);
            string other = temp.Substring(end - start);

            string expected = text + other;

            Assert.Equal(expected, rope.Report());
        }
    }
}
