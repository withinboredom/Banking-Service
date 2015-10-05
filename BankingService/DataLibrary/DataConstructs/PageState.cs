using System.Collections.Generic;
using OpenQA.Selenium;

namespace DataLibrary.DataConstructs
{
    public class PageState
    {
        public IReadOnlyCollection<Cookie> Cookies { get; set; }
        public string Url { get; set; }
        public int Step { get; set; }
    }
}