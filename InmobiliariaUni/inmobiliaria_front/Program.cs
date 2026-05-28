//Libreria relaciona a la autenticacion por cookies
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Dice que el metodo por defecto seran cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Index";  //Si alguien intenta entrar a otra pagina lo redirecciona al login
        options.AccessDeniedPath = "/Index";   //Si esta logueado pero no tiene el rol necesario tambien
        options.ExpireTimeSpan = TimeSpan.FromHours(8);  //La cookie dura aprox 8 horas, despues el usuario tiene que volver a iniciar seccion
    });

//Esto lo que nos permite es darle funcionalidad a los autorizados de las diferentes paginas
builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();   //Lee la cookie del navegador y revisa quien es el usuario
app.UseAuthorization();  //Revisa si el usuario tiene permisos para acceder donde esta solicitando

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();