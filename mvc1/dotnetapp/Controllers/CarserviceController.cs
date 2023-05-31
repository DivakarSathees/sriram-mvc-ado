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

public class CarserviceController : Controller
{
    private string connectionString = "User ID=sa;password=examlyMssql@123;server=dfebcbcfaafeabdbcfacbdcbaeadbebabcdebdca-0;Database=carserviceDB;trusted_connection=false;Persist Security Info=False;Encrypt=False";

    public ActionResult Index()
    {
        List<Carservice> carservices = new List<Carservice>();
try
{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "SELECT * FROM carservice";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                Carservice carservice = new Carservice();
                carservice.id = Convert.ToInt32(reader["id"]);                   
                carservice.car_name = reader["car_name"].ToString();
                carservice.car_number = reader["car_number"].ToString();
                carservice.car_varient = reader["car_varient"].ToString();
                carservice.customer_name = reader["customer_name"].ToString();
                carservice.complaint = reader["complaint"].ToString();
                carservice.phonenumber = reader["phonenumber"].ToString();
                carservice.address = reader["address"].ToString();
                carservices.Add(carservice);
                }

                reader.Close();
            }
        }return View(carservices);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    return BadRequest("An error occurred while retrieving the carservices item.");

}
        // return View(carservices);

    }
    public ActionResult Create()
    {
        return View();
    }
    
    
    [HttpPost]
    public ActionResult Create(Carservice carservice)
    {
        try{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO carservice (car_name, car_number, car_varient, customer_name, complaint, phonenumber, address) VALUES (@car_name, @car_number, @car_varient, @customer_name, @complaint, @phonenumber, @address)";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                // command.Parameters.AddWithValue("@id", Carservice.id);
                command.Parameters.AddWithValue("@car_name", carservice.car_name);
                command.Parameters.AddWithValue("@car_number", carservice.car_number);
                command.Parameters.AddWithValue("@car_varient", carservice.car_varient);
                command.Parameters.AddWithValue("@customer_name", carservice.customer_name);
                command.Parameters.AddWithValue("@complaint", carservice.complaint);
                command.Parameters.AddWithValue("@phonenumber", carservice.phonenumber);
                command.Parameters.AddWithValue("@address", carservice.address);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }}
        catch(Exception ex)
{
    Console.WriteLine(ex.Message);
            return BadRequest("An error occurred while creating the carservices item.");

}
        return RedirectToAction("Index");
    }
    public ActionResult Edit(int id)
    {
        try{
    Carservice carservice = null;
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        string query = "SELECT * FROM Carservice WHERE id = @id";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                carservice = new Carservice();

                    carservice.id = Convert.ToInt32(reader["id"]);                   
                carservice.car_name = reader["car_name"].ToString();
                carservice.car_number = reader["car_number"].ToString();
                carservice.car_varient = reader["car_varient"].ToString();
                carservice.customer_name = reader["customer_name"].ToString();
                carservice.complaint = reader["complaint"].ToString();
                carservice.phonenumber = reader["phonenumber"].ToString();
                carservice.address = reader["address"].ToString();                  
            }

            reader.Close();
        }
    }if (carservice == null)
        {
            return NotFound();
        }
    return View(carservice);
        }catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        // You can handle the exception as per your requirements (e.g., logging, displaying an error message)
        return BadRequest("An error occurred while retrieving the carservices item.");
    }
}
    [HttpPost]
    public ActionResult Edit(Carservice carservice )
    {
        try{
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string query = "UPDATE Carservice SET car_name = @car_name, car_number = @car_number, car_varient = @car_varient, customer_name = @customer_name, complaint = @complaint, phonenumber = @phonenumber, address = @address WHERE id = @id";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", carservice.id);
                command.Parameters.AddWithValue("@car_name", carservice.car_name);
                command.Parameters.AddWithValue("@car_number", carservice.car_number);
                command.Parameters.AddWithValue("@car_varient", carservice.car_varient);
                command.Parameters.AddWithValue("@customer_name", carservice.customer_name);
                command.Parameters.AddWithValue("@complaint", carservice.complaint);
                command.Parameters.AddWithValue("@phonenumber", carservice.phonenumber);
                command.Parameters.AddWithValue("@address", carservice.address);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    // The provided ID does not exist in the Furniture table
                    return NotFound();
                }
            }
        }
        } catch(Exception ex)
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
        string query = "DELETE FROM carservice WHERE id = @id";

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
    }
        }catch(Exception ex)
{
    Console.WriteLine(ex.Message);
    return BadRequest("An error occurred while deleting the carservices item.");

}

    return RedirectToAction("Index");
    }
}