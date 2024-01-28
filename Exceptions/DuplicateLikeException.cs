namespace Forum_Management_System.Exceptions
{
    public class DuplicateLikeException : ApplicationException
    {
        private string message;
        public DuplicateLikeException() : base()
        {

        }
        public DuplicateLikeException(string message)
        {
            this.message = message;
        }
        public override string Message { get => this.message; }
    }
}
