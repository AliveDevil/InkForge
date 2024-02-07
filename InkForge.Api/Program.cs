using InkForge.Api.Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient();
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
	.AddEntityFrameworkStores<ApiDbcontext>()
	.AddDefaultUI().AddDefaultTokenProviders();
var provider = builder.Configuration.GetValue<string>("DbProvider");
builder.Services.AddDbContext<ApiDbcontext>(options => _ = provider switch
{
	"Sqlite" => options.UseSqlite(
		builder.Configuration.GetConnectionString("AuthDb"),
		x => x.MigrationsAssembly("InkForge.Api.Sqlite")
	),

	_ => throw new Exception($"Invalid Provider: {provider}")
});

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<IdentityUser>();
app.MapControllers();
app.MapRazorPages();

app.Run();
