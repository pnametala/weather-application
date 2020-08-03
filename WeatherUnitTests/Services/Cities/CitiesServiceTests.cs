using System.Diagnostics;
using Moq;
using NUnit.Framework;
using WeatherApplication.OpenWeather.Models;
using WeatherApplication.OpenWeather.Services.Cities;

namespace WeatherUnitTests.Services.Cities
{
    public class CitiesServiceTests
    {
        private ICitiesService _service { get; set; }
        
        [OneTimeSetUp]
        public void Setup()
        {
            _service = new CitiesService();
        }

        [Test(Description = "When calling search history with no previous searches, should obtain an empty list")]
        public void SearchHistory_WithNoPreviousQueries_ShouldBeEmpty()
        {
            Assert.That(_service.SearchHistory(), Is.Empty);
        }
        
        [Test(Description = "When calling FindById for an existing city, expects to add it in search history")]
        public void SearchHistory_WithPreviousQueries_ShouldMatch()
        {
            var cityId = 6101546;
            var city = _service.FindById(cityId);
            
            Assert.That(_service.SearchHistory().TrueForAll(c => c.Id == city.Id), Is.True);
        }
        
        [Test(Description = "When calling FindById for an existing city, expects to add it in search history")]
        public void SearchHistory_WithSameQueryMultpleTimes_OnlyOneMatch()
        {
            var cityId = 6101546;
            var city = _service.FindById(cityId);
            _service.FindById(cityId);
            _service.FindById(cityId);
            _service.FindById(cityId);
            
            Assert.That(_service.SearchHistory().TrueForAll(c => c.Id == city.Id), Is.True);
            Assert.That(_service.SearchHistory().Count, Is.EqualTo(1));
        }
        
        [Test(Description = "Query with valid city name should return matches")]
        public void Query_WithValidCity_ShouldReturnMatches()
        {
            var cities = _service.Search("Perth");
            
            Assert.That(cities.Count, Is.Not.Zero);
        }
        
        [Test(Description = "Query with invalid city should return no matches")]
        public void Query_WithInvalidCity_ShouldReturnNoMatches()
        {
            var cities = _service.Search("aaaaaaaaaaaaaaaaaaaaa");
            
            Assert.That(cities.Count, Is.Zero);
        }
    }
}