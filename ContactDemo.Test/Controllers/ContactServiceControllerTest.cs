using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ContactDemo.Controllers;
using ContactDemo.Controllers.ApiControllers;
using ContactDemo.Models;
using ContactDemo.Models.Validations;
using ContactDemo.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;
using System.Web.Http;

namespace ContactDemo.Test.Controllers
{
    [TestClass]
    public class ContactServiceControllerTest
    {
        private Mock<IContactService> _manager;


        [TestInitialize]
        public void Initialize()
        {
            _manager = new Mock<IContactService>();
        }

        [TestMethod]
        public void CreateValidContact()
        {
            // Arrange
            var contact = new ContactMaster();
            _manager.Expect(s => s.CreateContact(contact)).Returns(true);
            var controller = new ContactServiceController(_manager.Object);

            // Act
            var result = (IHttpActionResult)controller.Create(contact);

            // Assert
            Assert.AreEqual("Create", "Create");
        }


        [TestMethod]
        public void CreateInvalidContact()
        {
            // Arrange
            var contact = new ContactMaster();
            var apiResponse = new ApiResponse();
            _manager.Expect(s => s.CreateContact(contact)).Returns(false);
            var controller = new ContactServiceController(_manager.Object);

            // Act
            var result = (IHttpActionResult)controller.Create(contact);

            // Assert
            Assert.AreEqual("Create", "Create");
        }
    }
}
