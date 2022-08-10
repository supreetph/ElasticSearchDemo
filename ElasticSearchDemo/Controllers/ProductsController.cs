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
        public async Task<Products> Get(string name)
        {
            var results = await _client.SearchAsync<Products>(s => s
       .Query(q => q
           .Match(t => t
               .Field(f => f.id)
               .Query(name)
           )
       )
      );

            return results.Documents.FirstOrDefault();
        }

        // POST api/<ProductsController>
        [HttpPost]
        public Products Post([FromBody] Products value)
        {
            _client.IndexDocument<Products>(value);
            return value;
        }

        
    }
}
