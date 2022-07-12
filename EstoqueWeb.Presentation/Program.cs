using EstoqueWeb.Infra.Data.Interfaces;
using EstoqueWeb.Infra.Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//configurando o projeto para MVC
builder.Services.AddControllersWithViews();

//habilitando o uso de sessões no projeto
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//habilitando o projeto para usar permissões de acesso
builder.Services.Configure<CookiePolicyOptions>(options => { options.MinimumSameSitePolicy = SameSiteMode.None; });
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

//capturar a connectionstring mapeada no appseting.json
var connectionstring = builder.Configuration.GetConnectionString("EstoqueWeb");

//enviar a connectionstring para a classe ProdutoRepository - injeção de dependecia para as classes do repositorio
builder.Services.AddTransient<IProdutoRepository>(map => new ProdutoRepository(connectionstring));
builder.Services.AddTransient<IUsuarioRepository>(map => new UsuarioRepository(connectionstring));

var app = builder.Build();

//habilitando o uso de sessões no projeto
app.UseSession();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

//autenticação e autorização
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

//definindo a página inicial do projeto
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}"
    );

app.Run();