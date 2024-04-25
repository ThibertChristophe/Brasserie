namespace Brasserie.Exceptions
{
	public class WholesalerFoundException : Exception
	{
			public WholesalerFoundException() { }

			public WholesalerFoundException(string message) : base(message) { }

			public WholesalerFoundException(string message, Exception innerException)
				: base(message, innerException) { }	
	}
}
