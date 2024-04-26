using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Brasserie.Exceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
	{
		public async ValueTask<bool> TryHandleAsync(
			HttpContext httpContext,
			Exception exception,
			CancellationToken cancellationToken)
		{
			
			var errorResponse = new ProblemDetails
			{
				Detail = exception.Message
			};
			switch (exception)
			{
                case BrewerNotFoundException:
                case BeerNotFoundException:
				case StockNotFoundException:
					errorResponse.Status = (int)HttpStatusCode.NotFound;
					errorResponse.Title = exception.GetType().Name;
					break;
                case BadParameterException:
                    errorResponse.Status = (int)HttpStatusCode.BadRequest;
					errorResponse.Title = exception.GetType().Name;
					break;
				default:
					errorResponse.Status = (int)HttpStatusCode.InternalServerError;
					errorResponse.Title = "Internal Server Error";
					break;
			}
			httpContext.Response.StatusCode = errorResponse.Status.Value;

			await httpContext
				.Response
				.WriteAsJsonAsync(errorResponse, cancellationToken);
			return true;
		}
	}

}