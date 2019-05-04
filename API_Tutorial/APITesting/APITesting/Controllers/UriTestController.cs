using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APITesting.Controllers
{
    public class UriTestController : ApiController
    {
        // Querry Strin Testing  
        //http://localhost:49965/api/UriTest?addresses=delhi
        public HttpResponseMessage Get(string addresses = "All")
        {
            using (testingEntities entities = new testingEntities())
            {
                switch (addresses.ToLower())
                {
                    case "all":
                        return Request.CreateResponse(HttpStatusCode.OK,entities.ApiTestings.ToList());
                    case "delhi":
                        return Request.CreateResponse(HttpStatusCode.OK,entities.ApiTestings.Where(e => e.Eaddress.ToLower() == "delhi").ToList());
                    case "haryana":
                        return Request.CreateResponse(HttpStatusCode.OK,entities.ApiTestings.Where(e => e.Eaddress.ToLower() == "haryana").ToList());
                    default:
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for addresses . " + addresses + " is invalid.");
                }
            }
        }


        //inserting values using Url or in a single querry string
        //http://localhost:49965/api/UriTest?Sno=1&Ename=FN2&Ephone=9999&Eaddress=biharipur
        public HttpResponseMessage Put(int Sno, [FromUri]ApiTesting employee)
        {
            try
            {
                using (testingEntities entities = new testingEntities())
                {
                    var entity = entities.ApiTestings.FirstOrDefault(e => e.Sno == Sno);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id " + Sno.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.Ename = employee.Ename;
                        entity.Ephone = employee.Ephone;
                        entity.Eaddress = employee.Eaddress;
                       

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
