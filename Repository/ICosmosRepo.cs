using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Linq;

namespace CosmosHttpClient.Repo {
    public interface ICosmosRepo { 
        JsonResult GetAllData();
        JsonResult GetDataByType(string type);
        bool AddData(JObject data);
    }
}