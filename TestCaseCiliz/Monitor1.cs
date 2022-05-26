using System;

namespace TestCaseCiliz
{
    public class Monitor1<TValue> : IMonitor<TValue>
    {
        public TValue Value => _closure.Invoke();

        private Func<TValue> _closure;
        
        public Monitor1(Func<TValue> closure)
        {
            _closure = closure;
        }
    }
}