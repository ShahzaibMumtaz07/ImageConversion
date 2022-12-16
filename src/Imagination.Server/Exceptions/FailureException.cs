using System;
namespace Imagination.Server.Exceptions
{
    public class FailureException : Exception
    {
        public FailureException(string msg) : base(msg) {}
    }
}