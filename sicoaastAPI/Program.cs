using Microsoft.EntityFrameworkCore;
using sicoaastAPI.Data;
using sicoaastAPI.Mapper;
using sicoaastAPI.Repository;
using sicoaastAPI.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Conexión a sql server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnSql"));
});

//Automaper
builder.Services.AddAutoMapper(typeof(SicoaastMapper));

//Repositorios
builder.Services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
builder.Services.AddScoped<IOrganismoRepositorio, OrganismoRepositorio>();
builder.Services.AddScoped<ICcostoRepositorio, CcostoRepositorio>();
builder.Services.AddScoped<IDepartamentoRepositorio, DepartamentoRepositorio>();
builder.Services.AddScoped<IMovEmpRepositorio, MovEmpRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
