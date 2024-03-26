using System;

namespace tut2
{
    public class OverfillException : Exception
    {
        public OverfillException(string message) : base(message)
        {
        }
    }
}