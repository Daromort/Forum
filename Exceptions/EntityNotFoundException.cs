namespace Forum_Management_System.Exceptions
{
    public class EntityNotFoundException : ApplicationException
    {
        private string message;

        public EntityNotFoundException(string message)
        {
            this.message = message;
        }
        public override string Message { get => this.message; }
    }
}
