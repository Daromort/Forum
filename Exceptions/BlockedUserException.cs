namespace Forum_Management_System.Exceptions
{
    public class BlockedUserException : ApplicationException
    {
        private string message;

        public BlockedUserException(string message)
        {
            this.message = message;
        }
        public override string Message { get => this.message; }

    }
}
