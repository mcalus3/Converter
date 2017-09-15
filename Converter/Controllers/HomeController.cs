using Converter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Converter.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index(string plainText, string type)
        {
            var vm = new IndexViewModel();
            vm.PlainText = plainText;

            if (!string.IsNullOrEmpty(type))
            {
                SortedDocument doc = SortedDocumentFactory.Instance.CreateSortedDocument(type, plainText);
                vm.ConvertedDocument = doc.GetFormattedString();
            }

            return View(vm);
        }
        //
        // GET: Home/ApiDoc/
        public ActionResult ApiDoc()
        {
            return View();
        }
    }
}
