using Converter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Converter.Controllers
{
    public class ConverterController : ApiController
    {
        [HttpPost] // POST api/converter
        public HttpResponseMessage Post([FromUri]string type, [FromBody]string text)
        {
            SortedDocument doc;
            try
            {
                doc = SortedDocumentFactory.Instance.CreateSortedDocument(type, text);
                return Request.CreateResponse(HttpStatusCode.OK, doc.GetFormattedString());
            }
            catch(InvalidOperationException err)
            {
                HttpError error = new HttpError(err.Message);
                return Request.CreateResponse(HttpStatusCode.BadRequest, error);
            }

        }
    }
}
