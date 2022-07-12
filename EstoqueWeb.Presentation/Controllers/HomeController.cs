using EstoqueWeb.Infra.Data.Interfaces;
using EstoqueWeb.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EstoqueWeb.Presentation.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;

        public HomeController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IActionResult Index()
        {
            var model = new DashboardViewModel();

            try
            {
                var dataAtual = DateTime.Now;

                //definindo as datas
                model.DataInicio = new DateTime(dataAtual.Year, dataAtual.Month, 1);
                model.DataFim = new DateTime(dataAtual.Year, dataAtual.Month, DateTime.DaysInMonth(dataAtual.Year,dataAtual.Month));

                //obtendo o usuario autenticado na sessão
                var json = HttpContext.Session.GetString("usuario");
                var usuario = JsonConvert.DeserializeObject<UserIdentityModel>(json);

                //calculando as informações da model
        
               
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            return View(model);
        }
    }
}
