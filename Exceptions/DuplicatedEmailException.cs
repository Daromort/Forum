namespace Forum_Management_System.Exceptions
{
    public class DuplicatedEmailException : ApplicationException
    {
        private string message;

        public DuplicatedEmailException(string message)
        {
            this.message = message;
        }
        public override string Message { get => this.message; }
    }
}
