using System.ComponentModel.DataAnnotations;

namespace EstoqueWeb.Presentation.Models
{
    public class ProdutoRelatorioViewModel
    {
        [Required(ErrorMessage = "Por favor, informe a data de início.")]
        public string? DataMin { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de término.")]
        public string? DataMax { get; set; }

        [Required(ErrorMessage = "Por favor, marque Ativo ou Inativo.")]
        public int? Ativo { get; set; }

        [Required(ErrorMessage = "Por favor, selecione o formato desejado.")]
        public int? Formato { get; set; }

    }
}
