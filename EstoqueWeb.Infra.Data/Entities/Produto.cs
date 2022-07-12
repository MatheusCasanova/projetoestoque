using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueWeb.Infra.Data.Entities
{
    /// <summary>
    /// Classe de entidade para evento
    /// </summary>
    public class Produto
    {
        #region Propriedades

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public decimal Preco { get; set; }
        public int? Quantidade { get; set; }
        public DateTime? DataValidade { get; set; }
        public string? Descricao { get; set; }
        public int? Ativo { get; set; }
        public DateTime? DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public Guid IdUsuario { get; set; }

        #endregion

        #region Relacionamentos

        public Usuario? Usuario { get; set; }

        #endregion
    }
}
