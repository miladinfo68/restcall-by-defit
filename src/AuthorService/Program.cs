using Refit;
using Shared;
using Shared.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStoreDbAsSingleton();

builder.Services.AddRefitClient<IRefitRestClient>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["RestApi:Uri"]);
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();