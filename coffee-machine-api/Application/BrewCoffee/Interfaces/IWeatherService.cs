namespace coffee_machine_api.Application.BrewCoffee.Interfaces
{
    public interface IWeatherService
    {
        /// <summary>
        /// get the most current temperature
        /// </summary>
        /// <returns></returns>
        Task<double> GetTemperature();
    }
}