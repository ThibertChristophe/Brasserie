namespace Brasserie.Exceptions
{
	public class QuoteNotFoundException : Exception
	{
		public QuoteNotFoundException() { }

		public QuoteNotFoundException(string message) : base(message) { }

		public QuoteNotFoundException(string message, Exception innerException)
			: base(message, innerException) { }
	}
}
