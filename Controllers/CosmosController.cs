using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosHttpClient.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace CosmosHttpClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CosmosController : ControllerBase
    {

        [HttpGet()]
        public IActionResult Get()
        {
            var repo = new CosmosRepo();
            return repo.GetAllData();
        }

        [HttpGet("{type}")]
        public IActionResult GetByType(string type)
        {
            var repo = new CosmosRepo();
            return repo.GetDataByType(type);
        }

        [HttpPost]
        public async Task<bool> AddItem()
        {
            string content = "";
            using (var reader = new System.IO.StreamReader(
            Request.Body, System.Text.Encoding.UTF8, true, 4096, true))
            {
                content = await reader.ReadToEndAsync();
            }
            var data = JObject.Parse(content);
            var repo = new CosmosRepo();
            return repo.AddData(data);
        }

    }
}
