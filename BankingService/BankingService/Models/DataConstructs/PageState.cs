using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium;

namespace BankingService.Models.DataConstructs
{
    public class PageState
    {
        public IReadOnlyCollection<Cookie> cookies { get; set; }
        public string Url { get; set; }
        public int Step { get; set; }
    }
}