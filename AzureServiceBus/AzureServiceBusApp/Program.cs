using AzureHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//string serviceBusUrl = "weatherforecastsb.servicebus.windows.net";
//string queue = "add-weather-data";
//builder.Services.AddSingleton<IServiceBusHelper>(new ServiceBusHelper(serviceBusUrl, queue));


string serviceBusUrl = "courseservicebus.servicebus.windows.net";
string queue = "courseadded";
builder.Services.AddSingleton<IServiceBusHelper>(new ServiceBusHelper(serviceBusUrl, queue));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
