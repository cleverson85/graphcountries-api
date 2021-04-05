using AutoMapper;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static Domain.Util.Endpoints;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/countries/[action]")]
    public class GraphCountriesController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly AppSettings _appSettings;

        public GraphCountriesController(ICountryService countryService, AppSettings appSettings)
        {
            _countryService = countryService;
            _appSettings = appSettings;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _countryService.GetCountries());
        }

        [HttpGet]
        [Route(Route.GET_PAGE)]
        public async Task<IActionResult> GetWithPages(int pageIndex, int pageSize)
        {
            return Ok(await _countryService.GetWithPages(pageIndex, pageSize));
        }

        [HttpGet]
        [Route(Route.GET_BY_ID)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _countryService.GetCountryById(id));
        }

        [HttpGet]
        [Route(Route.GET_BY_CAPITAL_NAME)]
        public async Task<IActionResult> GetByCapitalName(string capitalName)
        {
            return Ok(await _countryService.GetByCapitalName(capitalName));
        }

        [HttpGet]
        [Route(Route.GET_BY_COUNTRY_NAME)]
        public async Task<IActionResult> GetByCountryName(string countryName)
        {
            return Ok(await _countryService.GetByCountryName(countryName));
        }

        [HttpPost]
        [Route(Route.POST)]
        public async Task<IActionResult> Save([FromBody] Country entity)
        {
            await _countryService.SaveCountry(entity);
            return Ok(await GetAll());
        }

        [HttpPut]
        [Route(Route.PUT)]
        public async Task<IActionResult> Update([FromBody] Country entity)
        {
            await _countryService.UpdateCountry(entity);
            return Ok();
        }

        [HttpDelete]
        [Route(Route.DELETE)]
        public async Task<IActionResult> Delete(int id)
        {
            await _countryService.DeleteCountry(id);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetCount()
        {
            int count = await _countryService.GetCount();
            return Ok(count);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUrlRepository()
        {
            return await Task.FromResult(Json(_appSettings.GitRespository));
        }
    }
}
