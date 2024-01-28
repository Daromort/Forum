namespace Forum_Management_System.Exceptions
{
    public class BlockedPostException : ApplicationException
    {
        private string message;

        public BlockedPostException(string message)
        {
            this.message = message;
        }
        public override string Message { get => this.message; }
    }
}
