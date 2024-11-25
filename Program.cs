using Microsoft.EntityFrameworkCore;
using FixsyWebApi.Data.Identity;
using FixsyWebApi.InstanceProvider;

var builder = WebApplication.CreateBuilder(args);


IdentityFactory.SetCurrent(new IdentityGeneratorFactory());

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerConfig(builder.Environment.EnvironmentName);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddServiceDependency();


builder.Services.AddDataContextDependency(builder.Configuration.GetConnectionString("FixsyDatabase"));

builder.Services.AddUnitOfWork();
builder.Services.AddRepositories();
builder.Services.AddCorsSettings();


var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseMySwagger();
//}

app.UseMySwagger();

app.UseCors("ClientPermission");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

