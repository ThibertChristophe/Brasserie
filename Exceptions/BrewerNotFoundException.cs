namespace Brasserie.Exceptions
{
	public class BrewerNotFoundException : Exception
	{
			public BrewerNotFoundException() { }

			public BrewerNotFoundException(string message) : base(message) { }

			public BrewerNotFoundException(string message, Exception innerException)
				: base(message, innerException) { }	
	}
}
