using ElasticSearchDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Nest;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElasticSearchDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ElasticClient _client;

        public ProductsController(ElasticClient client)
        {
            _client = client;
        }

        // GET api/<ProductsController>/5
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
           var  results = await _client.SearchAsync<Products>(s => s
            .Query(q => q
                .Term(t => t
                    .Field(f => f.Name)
                    .Value(name)
                )
            )
        );

             return Ok(results.Documents.ToList());
        }

        // POST api/<ProductsController>
        [HttpPost]
        public async Task<IActionResult> Post( Products value)
        {
           await  _client.IndexDocumentAsync(value);
            var createIndexResponse = _client.Indices.Create("products",
            index => index.Map<Products>(x => x.AutoMap())
        );
            return Ok(value);
        }

        
    }
}
