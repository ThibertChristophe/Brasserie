namespace Brasserie.Exceptions
{
	public class BadParameterException : Exception
	{
			public BadParameterException() { }

			public BadParameterException(string message) : base(message) { }

			public BadParameterException(string message, Exception innerException)
				: base(message, innerException) { }	
	}
}
