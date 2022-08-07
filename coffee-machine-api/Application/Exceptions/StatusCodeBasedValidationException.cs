using System;
using FluentValidation;
using FluentValidation.Results;

namespace coffee_machine_api.Application.Exceptions
{
	public class StatusCodeBasedValidationException: ValidationException
	{
		private int statusCode;

		public StatusCodeBasedValidationException(int statusCode): base("")
		{
			this.statusCode = statusCode;
		}

		public int GetStatusCode()
        {
			return this.statusCode;
        }
	}
}

