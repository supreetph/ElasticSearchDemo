using Elasticsearch.Net;
using Nest;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var settings = new ConnectionSettings(new Uri("http://localhost:9200/") ).DefaultIndex("products");//"bpENKXKiRAmX7fL9xVLwpQ",new  BasicAuthenticationCredentials("elastic","g1spIrfvKH9nwsGqI701")).DefaultIndex("products");
    
var client = new ElasticClient(settings);
builder.Services.AddSingleton(client);
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
app.UseCors(options => options.AllowAnyOrigin());
app.UseCors(options => options.AllowAnyMethod());
app.UseCors(options => options.AllowCredentials());
app.Run();
