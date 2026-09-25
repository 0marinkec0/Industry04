using API;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.ConfigureServices()
        .Build()
        .ConfigureApplication()
        .Run();
}
catch (Exception ex)
{ 
    Console.WriteLine(ex.ToString(), "Failed to start the application!");
}

