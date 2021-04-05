using Api;
using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject
{
    public class Tests
    {
        private DependencyResolverHelpercs _serviceProvider;

        public Tests()
        {
            var webHost = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .Build();
            _serviceProvider = new DependencyResolverHelpercs(webHost);
        }

        [SetUp]
        public void Setup()
        { }

        [Test]
        public void ServiceInstanceTest()
        {
            var serviceCountry = _serviceProvider.GetService<ICountryService>();
            Assert.IsNotNull(serviceCountry);
        }

        [Test]
        public async Task ShouldInsertCountriesTest()
        {
            var serviceCountry = _serviceProvider.GetService<ICountryService>();

            var country = new Country("Pais Teste Insert", "Capital Teste Insert", "123", "123", "456", "Linguagem Teste Insert", "Domain Insert", 1);
            await serviceCountry.SaveCountry(country);

            country = new Country("Uruguai", "Capital do Uruguai", "7895", "457", "888", "Guarani", "Domain Uruguai", 2);
            await serviceCountry.SaveCountry(country);

            var list = await serviceCountry.GetByCountryName("Pais Teste");

            Assert.Contains("Uruguai", list.ToList());
        }

        [Test]
        public async Task ShouldReturnCoutryByNameTest()
        {
            var serviceCountry = _serviceProvider.GetService<ICountryService>();

            var country = new Country("Teste", "Capital Teste", "123", "123", "456", "Linguagem Teste", "Domain", 1);
            await serviceCountry.SaveCountry(country);

            var list = await serviceCountry.GetByCountryName("Pais Teste");

            Assert.Contains("Pais Teste", list.ToList());
        }

        [Test]
        public async Task ShouldReturnCoutryByCapitalNameTest()
        {
            var serviceCountry = _serviceProvider.GetService<ICountryService>();
            var list = await serviceCountry.GetByCapitalName("Uruguai");

            Assert.Contains("Uruguai", list.ToList());
        }

        [Test]
        public async Task ShouldReturnCountryById()
        {
            var serviceCountry = _serviceProvider.GetService<ICountryService>();
            var country = await serviceCountry.GetCountryById(1);
          
            Assert.IsNotNull(country);
        }

        [Test]
        public async Task ShouldUpdateCountry()
        {
            var country = new Country("Brasil", "Brasilia", "2000000", "123.999", "456", "Português", "Domain Update", 1);

            var serviceCountry = _serviceProvider.GetService<ICountryService>();
            await serviceCountry.UpdateCountry(country);

            var data = await serviceCountry.GetCountryById(country.Id);

            Assert.AreEqual(data, country);
        }

        [Test]
        public async Task ShouldDeleteCountry()
        {
            var serviceCountry = _serviceProvider.GetService<ICountryService>();
            await serviceCountry.DeleteCountry(1);

            var data = await serviceCountry.GetCountryById(1);

            Assert.IsNull(data);
        }
    }
}