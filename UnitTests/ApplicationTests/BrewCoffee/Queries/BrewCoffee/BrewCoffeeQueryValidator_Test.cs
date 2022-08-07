using System;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Queries.BrewCoffee;
using coffee_machine_api.Application.Exceptions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace UnitTests.ApplicationTests.BrewCoffee.Queries.BrewCoffee
{
	public class BrewCoffeeQueryValidator_Test
	{
        [Fact]
		public void Validator_WhenDay1_Should418Status()
        {
			var mockBrewCoffeeCounterService = new Mock<IBrewCoffeeCounterService>();
			mockBrewCoffeeCounterService.Setup(s => s.IsFifthCoffee()).Returns(false);

            var mockDatetimeProvider = new Mock<IDateTimeProvider>();
            mockDatetimeProvider.Setup(s => s.GetNow()).Returns(new DateTime(2022, 8, 1));

            var validator = new BrewCoffeeQueryValidator(mockBrewCoffeeCounterService.Object, mockDatetimeProvider.Object);
            var validateResult = validator.Validate(new BrewCoffeeQuery());
            var validateErrors = validateResult.Errors;
            var customState = validateErrors.First().CustomState;

            Assert.True(validateErrors.Count == 1);
            Assert.True(customState is StatusCodeBasedValidationException);
            Assert.True(((StatusCodeBasedValidationException)customState).GetStatusCode() == StatusCodes.Status418ImATeapot);
            validateResult.IsValid.Should().BeFalse();
        }


        [Fact]
        public void Validator_When5thCall_Should503Status()
        {
            var mockBrewCoffeeCounterService = new Mock<IBrewCoffeeCounterService>();
            mockBrewCoffeeCounterService.Setup(s => s.IsFifthCoffee()).Returns(true);

            var mockDatetimeProvider = new Mock<IDateTimeProvider>();
            mockDatetimeProvider.Setup(s => s.GetNow()).Returns(DateTime.Now);

            var validator = new BrewCoffeeQueryValidator(mockBrewCoffeeCounterService.Object, mockDatetimeProvider.Object);
            var validateResult = validator.Validate(new BrewCoffeeQuery());
            var validateErrors = validateResult.Errors;
            var customState = validateErrors.First().CustomState;

            Assert.True(validateErrors.Count == 1);
            Assert.True(customState is StatusCodeBasedValidationException);
            Assert.True(((StatusCodeBasedValidationException)customState).GetStatusCode() == StatusCodes.Status503ServiceUnavailable);
            validateResult.IsValid.Should().BeFalse();
        }


        [Fact]
        public void Validator_When5thCallAndDay1_Should418Status()
        {
            var mockBrewCoffeeCounterService = new Mock<IBrewCoffeeCounterService>();
            mockBrewCoffeeCounterService.Setup(s => s.IsFifthCoffee()).Returns(true);

            var mockDatetimeProvider = new Mock<IDateTimeProvider>();
            mockDatetimeProvider.Setup(s => s.GetNow()).Returns(new DateTime(2022, 8, 1));

            var validator = new BrewCoffeeQueryValidator(mockBrewCoffeeCounterService.Object, mockDatetimeProvider.Object);
            var validateResult = validator.Validate(new BrewCoffeeQuery());
            var validateErrors = validateResult.Errors;
            var customState = validateErrors.First().CustomState;

            Assert.True(validateErrors.Count == 1);
            Assert.True(customState is StatusCodeBasedValidationException);
            Assert.True(((StatusCodeBasedValidationException)customState).GetStatusCode() == StatusCodes.Status418ImATeapot);
            validateResult.IsValid.Should().BeFalse();
        }


        [Fact]
        public void Validator_WhenNot5thCallOrDay1_Should200()
        {
            var mockBrewCoffeeCounterService = new Mock<IBrewCoffeeCounterService>();
            mockBrewCoffeeCounterService.Setup(s => s.IsFifthCoffee()).Returns(false);

            var mockDatetimeProvider = new Mock<IDateTimeProvider>();
            mockDatetimeProvider.Setup(s => s.GetNow()).Returns(DateTime.Now);

            var validator = new BrewCoffeeQueryValidator(mockBrewCoffeeCounterService.Object, mockDatetimeProvider.Object);
            var validateResult = validator.Validate(new BrewCoffeeQuery());
            var validateErrors = validateResult.Errors;

            Assert.True(validateErrors.Count == 0);
            validateResult.IsValid.Should().BeTrue();
        }
    }
}

