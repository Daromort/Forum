namespace Forum_Management_System.Exceptions
{
    public class BlockedCommentException : ApplicationException
    {
        private string message;

        public BlockedCommentException(string message)
        {
            this.message = message;
        }
        public override string Message { get => this.message; }
    }
}
