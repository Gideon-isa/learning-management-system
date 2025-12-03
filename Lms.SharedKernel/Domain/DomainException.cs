namespace Lms.SharedKernel.Domain
{
    public abstract class DomainException : Exception
    {
        protected DomainException() { }
        public DomainException(string message) : base(message) { }
        protected DomainException(string message, Exception innerException) : base(message, innerException) { }

        public static T Create<T>(string message) where T : DomainException
        {
            var exception = (T)Activator.CreateInstance(typeof(T), message)!;
            return exception;
        }

        public static T Create<T>(string message, Exception innerException) where T : DomainException
        {
            var exception = (T)Activator.CreateInstance(typeof(T), message, innerException)!;
            return exception;
        }
    }
}
