namespace TestCaseCiliz
{
    public class TestClass
    {
        public string StringValue;
        public int IntValue;

        public bool Equals(TestClass other)
        {
            return other.StringValue.Equals(StringValue) &&
                   other.IntValue == IntValue;
        }
    }
}