using System.Collections.Generic;
using System.Web.Http;

namespace Lazulisoft.WebApi2WithStartupClass.Api.Controllers
{
    [RoutePrefix("api/v1/values")]
    public class ValuesController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IEnumerable<string> GetAll()
        {
            return new[] { "string1", "string2" };
        }
    }
}