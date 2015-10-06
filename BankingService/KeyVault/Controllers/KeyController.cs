using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KeyVault.Models;
using KeyVault.Models.Key;

namespace KeyVault.Controllers
{
    [RoutePrefix("key")]
    public class KeyController : ApiController
    {
        [Route("")]
        [HttpPost]
        public KeyValue GetByKey([FromBody] KeyRequest request)
        {
            return new KeyValue()
            {
                Key = request.Key,
                Value = ""
            };
        }

        [Route("")]
        [HttpPut]
        public KeyValue SetValue([FromBody] KeyRequest request)
        {
            return new KeyValue()
            {
                Key = request.Key,
                Value = request.Value
            };
        }
    }
}
