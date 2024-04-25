namespace Brasserie.Exceptions
{
	public class BeerNotFoundException : Exception
	{
			public BeerNotFoundException() { }

			public BeerNotFoundException(string message) : base(message) { }

			public BeerNotFoundException(string message, Exception innerException)
				: base(message, innerException) { }	
	}
}
