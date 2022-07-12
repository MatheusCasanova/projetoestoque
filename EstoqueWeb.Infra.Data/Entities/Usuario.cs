using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueWeb.Infra.Data.Entities
{
    /// <summary>
    /// classe de entidade para usuario
    /// </summary>
    public class Usuario
    {
        #region Propriedades

        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }
        public DateTime DataInclusao { get; set; }

        #endregion

        #region Relacionamentos

        public List<Produto>? Produtos { get; set; }

        #endregion

    }
}
