// using System;
// using Microsoft.EntityFrameworkCore;

// namespace dotnetapp.Models
// {
//    public class EmployeeDBContext : DbContext
// {
//     // public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }
//     public DbSet<EmployeeModel> Employees { get; set; }
// }
// }

using System;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
   public class EmployeeDBContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }
}
}