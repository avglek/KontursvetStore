using System.Reflection;
using KontursvetStore.Core.Abstractions;
using KontursvetStore.Core.Models;
using KontursvetStore.DataAccess;
using KontursvetStore.DataAccess.Entities;
using KontursvetStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace KontursvetStore.Tests.DataAccessTests;

[TestFixture]
public class CategorySpec
{
    private IConfiguration _configuration;
    private string _workDir;

    [SetUp]
    public void SetUp()
    {
        var path = "/Users/aleksandr/dev/workspace/KontursvetStore/KontursvetStore.Tests/";
        
        _workDir = Path.GetPathRoot(Path.GetFullPath("."));
        
        _configuration =  new ConfigurationBuilder().AddJsonFile($"{path}/appsettings.json").Build();
    }
    
    [Test]
    public void Test()
    {

         var connection = _configuration.GetConnectionString("DefaultConnection");
         
         Assert.That(connection, Is.EqualTo("Host=localhost;Port=5430;Database=Store;Username=postgres;Password=postgres"));
         
         var context = new StoreDbContext(_configuration);

         var ce = new CategoryEntity()
         {
             Id = Guid.NewGuid(),
             Name = "Test"
         };
         
         var category = context.Categories.Add(ce);
         context.SaveChanges();
         
         
         Assert.That(category, Is.Not.Null);
         
    }

    [Test]
    public void Directory()
    {
        var directory = _workDir;
        Console.WriteLine(directory);
        
       // Assert.That(directory, Is.EqualTo(_workDir));
       

       string assemblyLocation = Assembly.GetExecutingAssembly().Location;
       string applicationDirectory = Path.GetDirectoryName(assemblyLocation);
       
       Console.WriteLine(applicationDirectory);
    }
}