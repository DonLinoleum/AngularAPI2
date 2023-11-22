using AngularAPI2;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlite(connection));
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder=>builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapGet("/api/users", async (ApplicationContext db,HttpContext context) =>
{
    Console.WriteLine(context.Request.Method);
    return await db.Users.ToListAsync();
});

app.MapPost("/api/users", async (UserModel user,ApplicationContext db, HttpContext context) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return user;
});

app.MapPut("/api/users", async (UserModel userData, ApplicationContext db, HttpContext context) =>
{
    UserModel? user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);
    if (user == null) return Results.NotFound(new { message = "NotFound" });

    user.Name = userData.Name;
    user.Age = userData.Age;
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{
    UserModel? user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
    if (user == null) return Results.NotFound(new { message = "NotFound" });

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.Run();
