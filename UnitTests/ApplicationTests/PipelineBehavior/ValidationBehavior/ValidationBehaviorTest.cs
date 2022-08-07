using System;
using coffee_machine_api.Application.BrewCoffee.Interfaces;
using coffee_machine_api.Application.BrewCoffee.Queries.BrewCoffee;
using coffee_machine_api.Application.Exceptions;
using coffee_machine_api.Application.PipelineBehaviors;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;

namespace UnitTests.ApplicationTests.PipelineBehavior.ValidationBehavior
{
	public class ValidationBehaviorTest
	{
        [Fact]
		public async void Hanlder_WhenNoValidationFailure_ShouldNotThrowException()
        {
            var brewCoffeeCounterService = new Mock<IBrewCoffeeCounterService>().Object;
            var datetimeProvider = new Mock<IDateTimeProvider>().Object;
            var mockValidator = new Mock<IValidator<IRequest<Unit>>>();
            mockValidator.Setup(s => s.ValidateAsync(It.IsAny<IValidationContext>(), CancellationToken.None))
                .Returns(Task.FromResult(new ValidationResult()));

            var validators = new List<IValidator<IRequest<Unit>>>() { mockValidator.Object };

            var validationBehaviour = new ValidationBehaviour<IRequest<Unit>, Unit>(validators);

            var mockDelegate = new Mock<RequestHandlerDelegate<Unit>>();

            try
            {
                await validationBehaviour.Handle(null!, CancellationToken.None, mockDelegate.Object);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async void Hanlder_WhenStatusCodeBasedValidationFailure_ShouldThrowStatusCodeBasedValidationException()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Test failure", "Test message") {CustomState = new StatusCodeBasedValidationException(503)}
            };

            var brewCoffeeCounterService = new Mock<IBrewCoffeeCounterService>().Object;
            var datetimeProvider = new Mock<IDateTimeProvider>().Object;
            var mockValidator = new Mock<IValidator<IRequest<Unit>>>();
            mockValidator.Setup(s => s.ValidateAsync(It.IsAny<IValidationContext>(), CancellationToken.None))
                .Returns(Task.FromResult(new ValidationResult(validationFailures)));

            var validators = new List<IValidator<IRequest<Unit>>>() { mockValidator.Object };

            var validationBehaviour = new ValidationBehaviour<IRequest<Unit>, Unit>(validators);

            var mockDelegate = new Mock<RequestHandlerDelegate<Unit>>();

            try
            {
                await validationBehaviour.Handle(null!, CancellationToken.None, mockDelegate.Object);
                Assert.True(false);
            }
            catch(StatusCodeBasedValidationException ex)
            {
                Assert.True(ex.GetStatusCode() == 503);
                Assert.True(true);
            }
        }

        [Fact]
        public async void Hanlder_WhenOtherFailure_ShouldThrowValidationException()
        {
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Test failed property", "Test message")
            };

            var brewCoffeeCounterService = new Mock<IBrewCoffeeCounterService>().Object;
            var datetimeProvider = new Mock<IDateTimeProvider>().Object;
            var mockValidator = new Mock<IValidator<IRequest<Unit>>>();
            mockValidator.Setup(s => s.ValidateAsync(It.IsAny<IValidationContext>(), CancellationToken.None))
                .Returns(Task.FromResult(new ValidationResult(validationFailures)));

            var validators = new List<IValidator<IRequest<Unit>>>() { mockValidator.Object };

            var validationBehaviour = new ValidationBehaviour<IRequest<Unit>, Unit>(validators);

            var mockDelegate = new Mock<RequestHandlerDelegate<Unit>>();

            try
            {
                await validationBehaviour.Handle(null!, CancellationToken.None, mockDelegate.Object);
                Assert.True(false);
            }
            catch (ValidationException ex)
            {
                Assert.True(ex.Errors.First().PropertyName == "Test failed property");
                Assert.True(ex.Errors.First().ErrorMessage == "Test message");
                Assert.True(true);
            }
        }
    }
}

