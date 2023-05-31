using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnetapp.Models;
using System.Data;
using Microsoft.Data.SqlClient;

public class EmployeeController : Controller
{
    private string connectionString = "User ID=sa;password=examlyMssql@123;server=dfebcbcfaafeabdbcfacbdcbaeadbebabcdebdca-0;Database=EmployeeDB;trusted_connection=false;Persist Security Info=False;Encrypt=False";

    public ActionResult Index()
    {
        List<Employee> employees = new List<Employee>();
try
{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM Employee";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                Employee employee = new Employee();
                employee.id = Convert.ToInt32(reader["id"]);                   
                employee.Name = reader["Name"].ToString();
                employee.DOB = Convert.ToDateTime(reader["DOB"]);
                employee.Gender = reader["Gender"].ToString();
                employee.Department = reader["Department"].ToString();
                employee.Position = reader["Position"].ToString();
                employee.salary = Convert.ToDecimal(reader["salary"]);
                employee.Email = reader["Email"].ToString();
                employee.phoneNumber = reader["phoneNumber"].ToString();
                employee.Address = reader["Address"].ToString();
                employees.Add(employee);
                }

                reader.Close();
            }
        } 
        return View(employees);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    return BadRequest("An error occurred while retrieving the employee item.");

}
        // return View(employees);

    }
    public ActionResult Create()
    {
        return View();
    }
    
    
    [HttpPost]
    public ActionResult Create(Employee employee)
    {
        try{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Employee (Name, DOB, Gender, Department, Position, Salary, Email, phoneNumber,Address) VALUES (@Name, @DOB, @Gender, @Department, @Position, @Salary, @Email, @phoneNumber, @Address)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // command.Parameters.AddWithValue("@id", Employee.id);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@DOB", employee.DOB);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@salary", employee.salary);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@phoneNumber", employee.phoneNumber);
                command.Parameters.AddWithValue("@Address", employee.Address);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        }
        catch(Exception ex)
{
    Console.WriteLine(ex.Message);
            return BadRequest("An error occurred while creating the furniture item.");

}
        return RedirectToAction("Index");
    }


    public ActionResult Edit(int id)
    {
        try{
    Employee employee = null;
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string query = "SELECT * FROM Employee WHERE id = @id";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                employee = new Employee();
                    employee.id = Convert.ToInt32(reader["id"]);
                    employee.Name = reader["Name"].ToString();
                    employee.DOB = DateTime.Parse(reader["DOB"].ToString());
                    employee.Gender = reader["Gender"].ToString();
                    employee.Department = reader["Department"].ToString();
                    employee.Position = reader["Position"].ToString();
                    employee.salary = Convert.ToDecimal(reader["salary"]);;
                    employee.Email = reader["Email"].ToString();
                    employee.phoneNumber = reader["phoneNumber"].ToString();
                    employee.Address = reader["Address"].ToString();                   
            }

            reader.Close();
        }
    }
    if (employee == null)
        {
            return NotFound();
        }
    return View(employee);
        }catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        // You can handle the exception as per your requirements (e.g., logging, displaying an error message)
        return BadRequest("An error occurred while retrieving the employee item.");
    }

}
    [HttpPost]
    public ActionResult Edit(Employee employee )
    {
        try{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Employee SET Name = @Name, DOB = @DOB, Gender = @Gender, Department = @Department, Position = @Position, Salary = @Salary, Email = @Email, phoneNumber = @phoneNumber, Address = @Address WHERE id = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", employee.id);
                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@DOB", employee.DOB);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@salary", employee.salary);
                command.Parameters.AddWithValue("@Email", employee.Email);
                command.Parameters.AddWithValue("@phoneNumber", employee.phoneNumber);
                command.Parameters.AddWithValue("@Address", employee.Address);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    // The provided ID does not exist in the Furniture table
                    return NotFound();
                }
            }
        }
        }catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
        return RedirectToAction("Index");
        
    }
    public ActionResult Delete(int id)
    {
        try{
            if (id <= 0)
        {
            return BadRequest();
        }
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string query = "DELETE FROM Employee WHERE id = @id";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return NotFound();
                }
        }
    }}
    catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    return BadRequest("An error occurred while deleting the employee item.");

}
    return RedirectToAction("Index");
    }
}