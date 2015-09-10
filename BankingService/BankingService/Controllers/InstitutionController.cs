using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using BankingService.Models;

namespace BankingService.Controllers
{
    public class InstitutionController : ApiController
    {
        [method: HttpPost]
        public IFlow BeginLogin(string username, string password)
        {
            return new Flow() {FlowId = Guid.NewGuid(), Id = Guid.NewGuid()};
        }
    }
}
