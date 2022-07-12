using System.ComponentModel.DataAnnotations;

namespace EstoqueWeb.Presentation.Models
{
    public class ProdutoEdicaoViewModel
    {
        //campo oculto
        public Guid Id { get; set; }

        [MinLength(6, ErrorMessage = "Por Favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por Favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por Favor, informe o nome do produto.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por Favor, informe o preço do produto.")]
        public string? Preco { get; set; }

        [Required(ErrorMessage = "Por Favor, informe a quantidade do produto.")]
        public string? Quantidade { get; set; }

        [Required(ErrorMessage = "Por Favor, informe a data de validade do produto.")]
        public string? DataValidade { get; set; }

        [MaxLength(500, ErrorMessage = "Por Favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por Favor, informe a descrição do produto.")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Por favor, informe se o evento está ativo ou inativo.")]
        public int? Ativo { get; set; }
    }
}
