using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace APITesting.Controllers
{
    public class EmployeeController : ApiController
    {
        //[Authorize]
        public IEnumerable<ApiTesting> Get()
        {
            using (testingEntities entities = new testingEntities())
            {
               return entities.ApiTestings.ToList();
            }
        }

        
         //Get Method with HttpResponse
         public HttpResponseMessage Get(int id)
        {
            using (testingEntities entities = new testingEntities())
            {
                var entity = entities.ApiTestings.FirstOrDefault(e => e.Sno == id);

                if(entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Employee with Id = " + id.ToString() + " not found");
                }
            }
        }

        //Post Method with Location of Inserted Record
        public HttpResponseMessage Post([FromBody] ApiTesting apiObject)
        {
            try
            {

                using (testingEntities entities = new testingEntities())
            {
                entities.ApiTestings.Add(apiObject);
                entities.SaveChanges();

                var message = Request.CreateResponse(HttpStatusCode.Created, apiObject);
                message.Headers.Location = new Uri(Request.RequestUri + apiObject.Sno.ToString());

                return message;
            }
        }
        catch (Exception ex)
        {
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
        }
       }


        // Delete of Database Entry
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (testingEntities entities = new testingEntities())
                {
                    var entity = entities.ApiTestings.FirstOrDefault(e => e.Sno == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.ApiTestings.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        // Altering Values of Database
        //Example{"Ename":"Denial","Ephone":"45665455","Eaddress":"Rajasthan"}
        public HttpResponseMessage Put(int id, [FromBody]ApiTesting employee)
        {
            try
            {
                using (testingEntities entities = new testingEntities())
                {
                    var entity = entities.ApiTestings.FirstOrDefault(e => e.Sno == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id " + id.ToString() + " not found to update");
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
