using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ContactDemo.Models;
using ContactDemo.Repository;
using ContactDemo.Models.Validations;

namespace ContactDemo.Controllers.ApiControllers
{
    public class ContactServiceController : ApiController
    {
        private IContactService _service;

        public ContactServiceController()
        {
            _service = new ContactService(new ModelStateWrapper(this.ModelState));
        }

        public ContactServiceController(IContactService service)
        {
            _service = service;
        }

        //[ResponseType(typeof(ContactMaster))]
        [HttpPost]
        public IHttpActionResult Create(ContactMaster contactToCreate)
        {
            _service.CreateContact(contactToCreate);
            if (this.ModelState.IsValid)
                return Ok(contactToCreate);
            return BadRequest(this.ModelState);
        }

        //[ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Edit(ContactMaster contactToEdit)
        {
            _service.EditContact(contactToEdit);
            if (this.ModelState.IsValid)
                return Ok(contactToEdit);
            return BadRequest(this.ModelState);
        }

        //[ResponseType(typeof(void))]
        [HttpDelete]
        public IHttpActionResult Delete(Int64 id)
        {
            var contactToDelete = _service.GetContact(id);
            _service.DeleteContact(contactToDelete);
            return StatusCode(HttpStatusCode.NoContent);
        }

        //[ResponseType(typeof(ContactMaster))]
        public IHttpActionResult Get(Int64 id)
        {
            return Ok(_service.GetContact(id));
        }

        [HttpGet]
        //[ResponseType(typeof(IEnumerable<ContactMaster>))]
        public IHttpActionResult Get()
        {
            return Ok(_service.GetAllContacts());
        }

    }

    
}
