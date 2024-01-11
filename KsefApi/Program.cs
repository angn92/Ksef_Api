using Autofac;
using Autofac.Extensions.DependencyInjection;
using KsefClient.ClientHttp;
using KsefInfrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

//Autofac container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(x => x.RegisterModule(new AutofacModule()));

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<IAuthChallenge, KsefApiHttp>(client =>
{
    client.BaseAddress = new Uri("https://ksef-test.mf.gov.pl/api/online/Session/AuthorisationChallenge");
});

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
