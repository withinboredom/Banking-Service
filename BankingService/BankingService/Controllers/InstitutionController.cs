using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls;
using BankingService.Models;
using BankingService.Models.Banks;

namespace BankingService.Controllers
{
    public class InstitutionController : ApiController
    {
        /// <summary>
        /// Begins a login flow to a bank
        /// </summary>
        /// <param name="username">The username to signin with</param>
        /// <param name="password">The password to signin with</param>
        /// <param name="institution">The institution to signin to</param>
        /// <returns>Flow state</returns>
        [method: HttpPost]
        public IFlow BeginLogin(string username, string password, Institutions institution)
        {
            return new Flow() {FlowId = Guid.NewGuid(), Id = Guid.NewGuid()};
        }
    }
}
