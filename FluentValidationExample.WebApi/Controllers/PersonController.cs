using System;
using System.Web.Http;
using FluentValidationExample.Models;

namespace FluentValidationExample.WebApi.Controllers
{
    public class PersonController : ApiController
    {
        public IHttpActionResult Post([FromBody]PersonCreateRequestModel requestModel)
        {
            // If we get this far we have a vaild model.
            // If we then saved the person to the database we would get an id for the person and return it to the caller.
            requestModel.PersonId = Guid.NewGuid();

            return Ok(requestModel.PersonId);
        }
        
        // normally you would pass an id to Get for a single result, but this is easier to demo
        public IHttpActionResult Get()
        {
            PersonModel person = new PersonModel
            {
                PersonId = Guid.NewGuid(),
                Firstname = "Dan",
                Lastname = "Davis"
            };

            return Ok(person);
        }
    }
}
