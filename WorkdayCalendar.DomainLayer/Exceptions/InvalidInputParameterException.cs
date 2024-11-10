namespace WorkdayCalendar.DomainLayer.Exceptions
{
    public class InvalidInputParameterException : Exception
    {
        public InvalidInputParameterException() { }
        public InvalidInputParameterException(string message) : base(message) { }
        public InvalidInputParameterException(string message, Exception innerException) : base(message, innerException) { }
    }
}
