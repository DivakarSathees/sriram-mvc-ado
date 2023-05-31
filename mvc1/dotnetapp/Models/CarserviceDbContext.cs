using System;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Models
{
   public class carserviceDBContext : DbContext
{
    public DbSet<Carservice> Carservices { get; set; }
}
}