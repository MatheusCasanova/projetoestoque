using EstoqueWeb.Infra.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace EstoqueWeb.Presentation.Models
{
    public class ProdutoConsultaViewModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de ínicio.")]
        public string? DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string? DataMax { get; set; }

        [Required(ErrorMessage = "Por favor, marque Ativo ou Inativo")]
        public int? Ativo { get; set; }

        /*
         lista de produtos que será utilizado para exibir
         na página o resultado da consulta feita no banco
        */
        public List<Produto>? Produtos { get; set; }
    }
}
