using NUnit.Framework;
using dotnetapp.Controllers;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using Moq;
using System.Data;
using System.Data.SqlClient;


namespace CarserviceControllerTests
{
    [TestFixture]
    public class CarserviceControllerTests
    {
        private CarserviceController _controller;
        private Type controllerType;
        private Type carserviceType;
        private PropertyInfo[] properties;

        [SetUp]
        public void Setup()
        {
            // Initialize the controller before each test
            _controller = new CarserviceController();
            carserviceType = typeof(dotnetapp.Models.Carservice);
            properties = carserviceType.GetProperties();
            controllerType = typeof(CarserviceController);
        }
[Test]
public void Index_ReturnsViewResultWithCarservicesSortedByName()
{
    // Arrange
    var sortOrder = "car_name"; // Sort by car_name

    // Act
    var result = _controller.Index(sortOrder) as ViewResult;

    // Assert
    Assert.IsNotNull(result);
    Assert.IsInstanceOf<ViewResult>(result);
    Assert.IsAssignableFrom(typeof(List<Carservice>), result.Model);

    // Add more specific assertions based on your expected sorting behavior
    // For example, you can check if the carservices in the result are sorted by name.
    var model = result.Model as List<Carservice>;
    // Add assertions here to check if the model is sorted by car_name.
}


        [Test]
        public void Create_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

[Test]
public void Delete_NonExistingId_ReturnsNotFoundResult()
{
    // Arrange
    int nonExistingId = 100; // Assuming there is no Carservice item with ID 100

    // Act
    var result = _controller.Delete(nonExistingId) as NotFoundResult;

    // Assert
    Assert.IsNotNull(result);
    Assert.IsInstanceOf<NotFoundResult>(result);
}

[Test]
public void Delete_InvalidId_ReturnsBadRequestResult()
{
    // Arrange
    int invalidId = -1; // Assuming -1 is an invalid ID

    // Act
    var result = _controller.Delete(invalidId) as BadRequestResult;

    // Assert
    Assert.IsNotNull(result);
    Assert.IsInstanceOf<BadRequestResult>(result);
}

[Test]
public void TestIndexMethodExists()
{
    // Arrange
    var controllerType = typeof(CarserviceController);
    var methodName = "Index";

    // Act
    var indexMethod = controllerType.GetMethod(methodName);

    // Assert
    Assert.IsNotNull(indexMethod, $"{methodName} method should exist in CarserviceController.");
}

[Test]
        public void TestCreateGetMethodExists()
        {
            // Arrange
            MethodInfo createGetMethod = controllerType.GetMethod("Create", new Type[0]);

            // Assert
            Assert.IsNotNull(createGetMethod, "Create method should exist in CarserviceController.");
        }

        [Test]
        public void TestCreatePostMethodExists()
        {
            // Arrange
            MethodInfo createPostMethod = controllerType.GetMethod("Create", new Type[] { typeof(Carservice) });

            // Assert
            Assert.IsNotNull(createPostMethod, "Create POST method should exist in CarserviceController.");
        }

        [Test]
        public void TestDeleteMethodExists()
        {
            // Arrange
            MethodInfo createPostMethod = controllerType.GetMethod("Delete", new Type[] { typeof(int) });

            // Assert
            Assert.IsNotNull(createPostMethod, "Delete method should exist in CarserviceController.");
        }

        [Test]
        public void TestCarserviceClassExists()
        {
            // Arrange
            Type furnitureType = typeof(dotnetapp.Models.Carservice);

            // Assert
            Assert.IsNotNull(furnitureType, "Carservice class should exist.");            
        }

        [Test]
        public void TestEmployeePropertiesExist()
        {
            // Assert
            Assert.IsNotNull(properties, "Carservice class should have properties.");
            Assert.IsTrue(properties.Length > 0, "Carservice class should have at least one property.");
        }

        [Test]
        public void TestidProperty()
        {
            // Arrange
            PropertyInfo idProperty = properties.FirstOrDefault(p => p.Name == "id");

            // Assert
            Assert.IsNotNull(idProperty, "id property should exist.");
            Assert.AreEqual(typeof(int), idProperty.PropertyType, "id property should have data type 'int'.");
        }

        [Test]
        public void Testcar_nameProperty()
        {
            // Arrange
            PropertyInfo productProperty = properties.FirstOrDefault(p => p.Name == "car_name");

            // Assert
            Assert.IsNotNull(productProperty, "car_name property should exist.");
            Assert.AreEqual(typeof(string), productProperty.PropertyType, "car_name property should have data type 'string'.");
        }

        [Test]
        public void TestcarserviceDBContext_ClassExists_in_Models()
        {
            // Load the assembly at runtime
            Assembly assembly = Assembly.Load("dotnetapp");
            Type postType = assembly.GetType("dotnetapp.Models.carserviceDBContext");
            Assert.NotNull(postType, "carserviceDBContext class does not exist.");
        }

        [Test]
        public void Test_CreateViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/sriram-mvc-ado/mvc1/dotnetapp/Views/carservice", "Create.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Create.cshtml view file does not exist.");
        }

        [Test]
        public void Test_IndexViewFile_Exists()
        {
            string indexPath = Path.Combine(@"/home/coder/project/workspace/sriram-mvc-ado/mvc1/dotnetapp/Views/carservice", "Index.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Index.cshtml view file does not exist.");
        }

        




    }
}