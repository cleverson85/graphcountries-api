using Domain.Interfaces.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Domain.Util.Endpoints;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/coutries/[action]")]
    public class GraphCountries : Controller
    {
        private readonly ICountryService _countryService;

        public GraphCountries(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _countryService.GetAll());
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
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] Country entity)
        {
            try
            {
                await _countryService.SaveCountry(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route(Route.DELETE)]
        public async Task<IActionResult> Delete([FromBody] Country entity)
        {
            return Json(await _countryService.GetByCountryName(countryName));
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
