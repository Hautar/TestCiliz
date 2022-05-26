namespace TestCaseCiliz
{
    public class Monitor2And3<TValue> : IMonitor<TValue>
    {
        public TValue Value { get; }

        public Monitor2And3(TValue tValue)
        {
            Value = tValue;
        }
    }
}