using NUnit.Framework;
using dotnetapp.Controllers;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmployeeControllerTests
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private EmployeeController _controller;

        [SetUp]
        public void Setup()
        {
            // Initialize the controller before each test
            _controller = new EmployeeController();
        }

        [Test]
        public void test_case1()
        {
            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<List<Employee>>(result.Model);
        }

        [Test]
        public void test_case2()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void test_case3()
        {
            // Arrange
            int nonExistingId = 100; // Assuming there is no employee with ID 100

            // Act
            var result = _controller.Edit(nonExistingId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void test_case4()
        {
            // Arrange
            int nonExistingId = 100; // Assuming there is no employee with ID 100

            // Act
            var result = _controller.Delete(nonExistingId) as NotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void test_case5()
        {
            // Arrange
            int invalidId = -1; // Assuming -1 is an invalid ID

            // Act
            var result = _controller.Delete(invalidId) as BadRequestResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestResult>(result);
        }
    }
}
