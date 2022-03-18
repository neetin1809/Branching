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

// Delegate implementation for app.Map("/first", handleFirstBranch)
void handlefirstBranch(IApplicationBuilder app)
{   //first/level-2
    app.Map("/level-2", app =>
    {
        //further level-3 i.e first/level-2/level-3
        app.Map("/level-3", app =>
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Branch: first/level-1/level-2/level-3");
            });
        });

        app.Run(async context =>
        {
            await context.Response.WriteAsync("Branch: first/level-1/level-2");
        });
    });
    app.Map("/step-2/step-3", app =>
     {
         app.Map("/step-4", app =>
          {
              app.Run(async context =>
              {
                  await context.Response.WriteAsync("Branch: /step-2/step-3/step-4");
              });
          });
         app.Run(async context =>
         {
             await context.Response.WriteAsync("Branch: first/step-2/step-3");
         });
     });
    //localhost/first
    app.Run(async context =>
    {
        await context.Response.WriteAsync("First Branch is executed");
    });
}