using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json.Linq;

namespace CosmosHttpClient.Repo
{
    public class CosmosRepo : ICosmosRepo
    {
        CosmosClient client;
        Container clientcontainer;
        public CosmosRepo()
        {
            this.client = new CosmosClient("AccountEndpoint=https://videoaccount.documents.azure.com:443/;AccountKey=tHg8H72Ds6awzbh23iepTlf68n3OObKUVQzzjOn6H4impO0DlYSWPLX1m8tXBFYA6Dt96XvZvzFii9oSMkkbdw==;");
            this.clientcontainer =  this.client.GetContainer("testdb","datacontainer");
        }
        public JsonResult GetAllData()
        {
            var resultList = new List<object>();
            var queryiterator = this.clientcontainer.GetItemQueryIterator<object>(new QueryDefinition("select * from T"));
            while (queryiterator.HasMoreResults)
            {
                FeedResponse<object> result = queryiterator.ReadNextAsync().Result;
                foreach (var item in result)
                {
                    resultList.Add(item);
                }
            }
            return new JsonResult(resultList);
        }

        public JsonResult GetDataByType(string type)
        {
            var resultList = new List<object>();
            var queryiterator = this.clientcontainer.GetItemQueryIterator<object>(new QueryDefinition($"select * from T where T.type = '{type}'"));
            while (queryiterator.HasMoreResults)
            {
                FeedResponse<object> result = queryiterator.ReadNextAsync().Result;
                foreach (var item in result)
                {
                    resultList.Add(item);
                }
            }
            return new JsonResult(resultList);
        }

        public bool AddData(JObject data)
        {
            data.Add("id", new JValue(Guid.NewGuid()));
            var returnedObject = this.clientcontainer.CreateItemAsync<JObject>(data,new PartitionKey(data.Property("type").Value.ToString())).Result;
            bool result = returnedObject != null;
            return result;
        }
    }
}