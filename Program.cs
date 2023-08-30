using INTEC.INFRASTRUCTURE;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<DBContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("INTECContext") ?? throw new InvalidOperationException("Connection string 'INTECContext' not found.")));

// Add services to the container.

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "origenes",
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200",
                                              "http://www.contoso.com");
                      });
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("origenes");

app.UseAuthorization();

app.MapControllers();

app.Run();
