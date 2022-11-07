using Catalog.Api.Extensions;
using Catalog.Api.Middlewares;
using EmailService.Extensions;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    
    // custom extensions
    builder.Services.AddDI();
    builder.Services.AddEmailServiceDI();
    builder.Services.AddDatabaseContexts(builder.Configuration);
    builder.Services.ApplySwaggerSettings();
    builder.Services.UseCustomAuth();
    builder.Services.ConfigurationsSetUp(builder.Configuration);
}

var app = builder.Build();
{
    var isSwaggerEnabled = app.Configuration.GetSection("EnableSwagger").Value;
    if (isSwaggerEnabled != null && bool.Parse(isSwaggerEnabled))
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantCatalog"));
    }
    
    app.UseMiddleware<ErrorHandlerMiddleware>();
    
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
