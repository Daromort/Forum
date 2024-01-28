namespace Forum_Management_System.Exceptions
{
    public class UnauthenticatedOperationException : ApplicationException
    {
        public UnauthenticatedOperationException(string message)
          : base(message)
        {
        }

    }
}
