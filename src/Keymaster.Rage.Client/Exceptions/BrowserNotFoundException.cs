using System;

namespace Keymaster.Rage.Client.Exceptions
{
    internal class BrowserNotFoundException : Exception
    {
        public BrowserNotFoundException()
        {
        }

        public BrowserNotFoundException(string message) : base(message)
        {
        }
    }
}
