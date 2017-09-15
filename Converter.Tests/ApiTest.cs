using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Converter.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Hosting;

namespace Converter.Tests
{
    [TestClass]
    public class ApiTest
    {
        [TestMethod]
        public void ApiConvertCsvTest()
        {
            //setUp
            string inputString = @"Mary had a little lamb. Peter called for the wolf, and Aesop came. 
Cinderella likes shoes.
";
            string inputString2 = @"  Mary   had a little  lamb  . 


  Peter   called for the wolf   ,  and Aesop came .
 Cinderella  likes shoes."
;
            string outputString = @", Word 1, Word 2, Word 3, Word 4, Word 5, Word 6, Word 7, Word 8
Sentence 1, a, had, lamb, little, Mary
Sentence 2, Aesop, and, called, came, for, Peter, the, wolf
Sentence 3, Cinderella, likes, shoes
";

            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            var httpRouteData = new HttpRouteData(httpConfiguration.Routes["DefaultApi"], 
            new HttpRouteValueDictionary { { "controller", "converter" } });
            var controller = new ConverterController()
            {
                Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:50014/api/controller/csv")
                {
                    Properties = 
                    {
                        { HttpPropertyKeys.HttpConfigurationKey, httpConfiguration },
                        { HttpPropertyKeys.HttpRouteDataKey, httpRouteData } 
                    }
                }
            };  
            // Test
            var result = controller.Post("csv", inputString).Content.ReadAsStringAsync().Result;
            var result2 = controller.Post("csv", inputString2).Content.ReadAsStringAsync().Result;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(outputString, result);
            Assert.IsNotNull(result2);
            Assert.AreEqual(outputString, result2);
        }

        [TestMethod]
        public void ApiConvertXmlTest()
        {
            //setUp
            string inputString = @"Mary had a little lamb. Peter called for the wolf, and Aesop came. 
Cinderella likes shoes.
";
            string inputString2 = @"  Mary   had a little  lamb  . 


  Peter   called for the wolf   ,  and Aesop came .
 Cinderella  likes shoes."
;
            string outputString = @"<?xml version=""1.0"" encoding=""utf-8"" standalone=""yes""?>
<text>
  <sentence>
    <word>a</word>
    <word>had</word>
    <word>lamb</word>
    <word>little</word>
    <word>Mary</word>
  </sentence>
  <sentence>
    <word>Aesop</word>
    <word>and</word>
    <word>called</word>
    <word>came</word>
    <word>for</word>
    <word>Peter</word>
    <word>the</word>
    <word>wolf</word>
  </sentence>
  <sentence>
    <word>Cinderella</word>
    <word>likes</word>
    <word>shoes</word>
  </sentence>
</text>";
            // Setup  
            var httpConfiguration = new HttpConfiguration();
            WebApiConfig.Register(httpConfiguration);
            var httpRouteData = new HttpRouteData(httpConfiguration.Routes["DefaultApi"],
            new HttpRouteValueDictionary { { "controller", "converter" } });
            var controller = new ConverterController()
            {
                Request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:50014/api/controller/xml")
                {
                    Properties = 
                    {
                        { HttpPropertyKeys.HttpConfigurationKey, httpConfiguration },
                        { HttpPropertyKeys.HttpRouteDataKey, httpRouteData } 
                    }
                }
            };  

            // Test
            var result = controller.Post("xml", inputString).Content.ReadAsStringAsync().Result;
            var result2 = controller.Post("xml", inputString2).Content.ReadAsStringAsync().Result;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(outputString, result);
            Assert.IsNotNull(result2);
            Assert.AreEqual(outputString, result2);
        }
    }
}
