using EstoqueWeb.Infra.Data.Entities;
using EstoqueWeb.Infra.Data.Interfaces;
using EstoqueWeb.Presentation.Models;
using EstoqueWeb.Reports.Interfaces;
using EstoqueWeb.Reports.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EstoqueWeb.Presentation.Controllers
{
    [Authorize]
    public class EstoqueController : Controller
    {
        //atributo
        private readonly IProdutoRepository _produtoRepository;

        //construtor para inicializar o atributo
        public EstoqueController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost] //annotation indica que o método será executado no SUBMIT
        public IActionResult Cadastro(ProdutoCadastroViewModel model)
        {
            //verificar se todos os campos passaram nas regras de validação
            if(ModelState.IsValid)
            {
                try
                {
                    //ler o usuario autenticado na sessão
                    var json = HttpContext.Session.GetString("usuario");
                    var usuario = JsonConvert.DeserializeObject<UserIdentityModel>(json);

                    var produto = new Produto
                    {
                        Id = Guid.NewGuid(),
                        Nome = model.Nome,
                        Preco = Convert.ToDecimal(model.Preco),
                        Quantidade = Convert.ToInt32(model.Quantidade),
                        DataValidade = Convert.ToDateTime(model.DataValidade),
                        Descricao = model.Descricao,
                        DataInclusao = DateTime.Now,
                        DataAlteracao = DateTime.Now,
                        IdUsuario = usuario.Id //foreign key
                    };
                    
                    //gravando no banco de dados
                    _produtoRepository.Create(produto);

                    TempData["Mensagem"] = $"Produto {produto.Nome}, cadastrado com sucesso.";
                    ModelState.Clear(); //limpando os campos do formulário (model)
                }
                catch (Exception e)
                {
                    TempData["Mensagem"] = e.Message;
                }

            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário";
            }

            return View();
        }

        public IActionResult Consulta()
        {
            return View();
        }

        [HttpPost] //annotation indica que o método será executado no SUBMIT
        public IActionResult Consulta(ProdutoConsultaViewModel model)
        {
            //verificando se todos os campos da model passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //converter a data
                    var dataMin = Convert.ToDateTime(model.DataMin);
                    var dataMax = Convert.ToDateTime(model.DataMax);

                    //verificando se a data de ínicio é menor ou igual a data do término
                    if (dataMin <= dataMax)
                    {
                        //ler o usuario autenticado na sessão
                        var json = HttpContext.Session.GetString("usuario");
                        var usuario = JsonConvert.DeserializeObject<UserIdentityModel>(json);

                        //realizando a consulta de eventos
                        model.Produtos = _produtoRepository.GetByDatas(dataMin, dataMax, model.Ativo, usuario.Id);

                        //verificando se algum evento foi obtido
                        if (model.Produtos.Count > 0)
                        {
                            TempData["MensagemSuscesso"] = $"{model.Produtos.Count} produto(s) obtido(s) para a pesquisa";
                        }
                        else
                        {
                            TempData["MensagemAlerta"] = "Nenhum produto foi encontrado para a pesquisa realizada";
                        }
                    }
                    else
                    {
                        TempData["MensagemErro"] = "A data de inicio deve ser menor ou igual a data de término";
                    }
                }
                catch (Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário";
            } 
            
            //voltando para a página
            return View(model);
        }

        public IActionResult Edicao(Guid id)
        {
            var model = new ProdutoEdicaoViewModel();

            try
            {
                //consultar o produto no banco de dados(repositorio) atráves do ID
                var produto = _produtoRepository.GetById(id);

                //preencher os dados da classe model com as informações do produto
                model.Id = produto.Id;
                model.Nome = produto.Nome;
                model.Preco = produto.Preco.ToString();
                model.Quantidade = produto.Quantidade.ToString();
                model.DataValidade = Convert.ToDateTime(produto.DataValidade).ToString("yyyy-MM-dd");
                model.Descricao = produto.Descricao;
                model.Ativo = produto.Ativo;
            }
            catch(Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }

            //enviando o model para a página
            return View(model);
        }

        [HttpPost]//annotation indica que o método será executado no SUBMIT
        public IActionResult Edicao(ProdutoEdicaoViewModel model)
        {
            //verificar se todos os campos passaram nas regras de validação
            if (ModelState.IsValid)
            {
                try
                {
                    //obtendo os dados do produto no banco de dados
                    var produto = _produtoRepository.GetById(model.Id);

                    //ler o usuario autenticado na sessão
                    var json = HttpContext.Session.GetString("usuario");
                    var usuario = JsonConvert.DeserializeObject<UserIdentityModel>(json);

                    //modificar os dados do evento
                    produto.Nome = model.Nome;
                    produto.Preco = Convert.ToDecimal(model.Preco);
                    produto.Quantidade = Convert.ToInt32(model.Quantidade);
                    produto.DataValidade = Convert.ToDateTime(model.DataValidade);
                    produto.Descricao = model.Descricao;
                    produto.Ativo = model.Ativo;
                    produto.DataAlteracao = DateTime.Now;
                    produto.IdUsuario = usuario.Id;

                    //atualizando no banco de dados
                    _produtoRepository.Update(produto);

                    TempData["MensagemSucesso"] = "Dado(s) do produto atualizado(s) com sucesso";

                    //redirecionamento para a página de consulta
                    return RedirectToAction("Consulta");
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            else
            {
                TempData["MensagemAlerta"] = "Ocorreram erros de validação no preenchimento do formulário";
            }

            return View();
        }

        public IActionResult Exclusao(Guid id)
        {
            try
            {
                //buscar produto no banco de dados
                var produto = _produtoRepository.GetById(id);

                //excluindo o produto
                _produtoRepository.Delete(produto);

                TempData["MensagemSucesso"] = $"Produto '{produto.Nome}', excluído com sucesso";
            }
            catch (Exception e)
            {
                TempData["MensagemErro"] = e.Message;
            }
            //redirecionando de volta para a página de consulta
            return RedirectToAction("Consulta");
        }

        public IActionResult Relatorio()
        {
            return View();
        }
        
        [HttpPost] //annotation para indicar que o método será executado no SUBMIT 
        public IActionResult Relatorio(ProdutoRelatorioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //capturando as datas enviadas
                    DateTime dataMin = Convert.ToDateTime(model.DataMin);
                    DateTime dataMax = Convert.ToDateTime(model.DataMax);

                    //ler o usuario autenticado na sessão
                    var json = HttpContext.Session.GetString("usuario");
                    var usuario = JsonConvert.DeserializeObject<UserIdentityModel>(json);

                    //consultar os produtos no banco atraves das datas
                    var produtos = _produtoRepository.GetByDatas(dataMin, dataMax, model.Ativo, usuario.Id);

                    //verificar se algum produto foi obtido
                    if(produtos.Count > 0)
                    {
                        //criando um objeto para interface
                        IProdutoReportService produtoReportService = null; //vazio

                        //variaveis para defirnir os parametros de download
                        var contentType = string.Empty; //MIME TYPE
                        var fileName = string.Empty; 

                        switch (model.Formato)
                        {
                            case 1: //Polimorfismo
                                produtoReportService = new ProdutoReportServicePdf();
                                contentType = "application/pdf";
                                fileName = $"produtos_{DateTime.Now.ToString("dddMMyyyyHHmmss")}.pdf";

                                break;

                            case 2: //Polimorfismo
                                produtoReportService = new ProdutoReportServiceExcel();
                                contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                                fileName = $"produtos_{DateTime.Now.ToString("dddMMyyyyHHmmss")}.xlsx";

                                break;
                        }

                        //gerando o arquivo do relatório
                        var arquivo = produtoReportService.Create(dataMin, dataMax, produtos);

                        //download do arquivo
                        return File(arquivo, contentType, fileName);
                    }
                    else
                    {
                        TempData["MensagemAlerta"] = "Nenhum produto foi obtido para a pesquisa informada.";
                    }
                        
                }
                catch(Exception e)
                {
                    TempData["MensagemErro"] = e.Message;
                }
            }
            return View();
        }
    }
}
