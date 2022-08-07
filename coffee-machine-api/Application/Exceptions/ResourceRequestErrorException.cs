using System;
namespace coffee_machine_api.Application.Exceptions
{
	public class ResourceRequestErrorException: Exception
	{
		public ResourceRequestErrorException(string message): base(message)
		{
		}
	}
}

