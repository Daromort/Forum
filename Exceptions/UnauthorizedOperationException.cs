﻿namespace Forum_Management_System.Exceptions
{
    public class UnauthorizedOperationException : ApplicationException
    {
        public UnauthorizedOperationException(string message)
         : base(message)
        {
        }
    }
}
