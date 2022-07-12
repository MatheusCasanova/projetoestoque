using EstoqueWeb.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueWeb.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface de repositorio para a entidade Usuario
    /// </summary>
    public interface IUsuarioRepository : IBaseReposity<Usuario>
    {
        /// <summary>
        /// Método para retornar os dados de 1 usuário baseado no email
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <returns>objeto usuario ou null se não for encontrado</returns>
        Usuario? GetByEmail(string email);

        /// <summary>
        /// Método para retornar os dados de 1 usuário baseado no email e na senha
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Objeto usuario ou null se não for encontrado</returns>
        Usuario? GetByEmailESenha(string email, string senha);

    }
}
