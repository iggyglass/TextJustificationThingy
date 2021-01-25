namespace RopeDataStructure
{
    internal class RopeNode
    {
        public RopeNode Parent;
        public RopeNode Left;
        public RopeNode Right;

        public int Weight;
        public string Text;

        public RopeNode(string text)
        {
            Text = text;
        }
    }
}
