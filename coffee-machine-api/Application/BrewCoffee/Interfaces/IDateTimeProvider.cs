﻿using System;
namespace coffee_machine_api.Application.BrewCoffee.Interfaces
{
	public interface IDateTimeProvider
	{
		DateTime GetNow();
	}
}

