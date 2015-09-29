using System.Collections.Generic;
using OpenQA.Selenium;

namespace BankLibrary.DataConstructs
{
    public class PageState
    {
        public IReadOnlyCollection<Cookie> cookies { get; set; }
        public string Url { get; set; }
        public int Step { get; set; }
    }
}