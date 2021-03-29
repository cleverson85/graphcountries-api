using Data.GraphQL.Queries;
using Domain.Interfaces.Services;
using Domain.Models;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GraphCountries : Controller
    {
        private readonly ICountryService _countryService;

        public GraphCountries(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save([FromBody] Country entity)
        {
            try
            {
                
                await _countryService.Save(new ContryData { JsonData = JsonConvert.SerializeObject(entity) });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
