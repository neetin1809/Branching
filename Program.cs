var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

//Map: used to branch the request pipeline. It takes input path(complete or partial) and if the incoming request matches,
//the new branch is executed
app.Map("/first", handlefirstBranch);
app.Map("/second", handleSecondBranch);
app.Run();


void handleSecondBranch(IApplicationBuilder app)
{
    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Second branch is executed");
    });
}

void handlefirstBranch(IApplicationBuilder app)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("First Branch is executed");
    });
}