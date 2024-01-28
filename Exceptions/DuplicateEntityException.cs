namespace Forum_Management_System.Exceptions
{
    public class DuplicateEntityException : ApplicationException
    {
        private string message;

        public DuplicateEntityException(string message)
        {
            this.message = message;
        }
        public override string Message { get => this.message; }
    }
}
