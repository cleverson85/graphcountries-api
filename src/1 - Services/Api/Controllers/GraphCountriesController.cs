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
            return Json(await _countryService.GetAll());
        }

        [HttpGet]
        [Route(Route.GET_BY_ID)]
        public async Task<IActionResult> GetById(int id)
        {
            return Json(await _countryService.GetById(id));
        }

        [HttpGet]
        [Route(Route.GET_BY_CAPITAL_NAME)]
        public async Task<IActionResult> GetByCapitalName(string capitalName)
        {
            return Json(await _countryService.GetByCapitalName(capitalName));
        }

        [HttpGet]
        [Route(Route.GET_BY_COUNTRY_NAME)]
        public async Task<IActionResult> GetByCountryName(string countryName)
        {
            return Json(await _countryService.GetByCountryName(countryName));
        }

        [HttpPost]
        [Route(Route.POST)]
        public async Task<IActionResult> Save([FromBody] Country entity)
        {
            await _countryService.SaveCountry(entity);
            return Ok();
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
        public async Task<IActionResult> Delete([FromBody] Country entity)
        {
            await _countryService.DeleteCountry(entity);
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetUrlRepository()
        {
            return await Task.FromResult(Json(_appSettings.GitRespository));
        }


        //public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        //{
        //    var inputs = query.Variables;

        //    var schema = new Schema()
        //    {
        //        Query = new CountryQuery(_countryService)
        //    };

        //    var result = await new DocumentExecuter().ExecuteAsync(_ =>
        //    {
        //        _.Schema = schema;
        //        _.Query = query.Query;
        //        _.OperationName = query.OperationName;
        //        _.Inputs = inputs;
        //    }).ConfigureAwait(false);

        //    if (result.Errors?.Count > 0)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok(result);
        //}
    }
}
