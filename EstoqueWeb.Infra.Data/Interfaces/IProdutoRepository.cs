using EstoqueWeb.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueWeb.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface de repositorio para a entidade produto
    /// </summary>
    public interface IProdutoRepository : IBaseReposity<Produto>
    {
        /// <summary>
        /// método para retornar todos os eventos dentro de um periodo de datas
        /// </summary>
        /// <param name="dataMin">Data de inicio do periodo</param>       
        /// <param name="dataMax">Data de termino do periodo</param>
        /// <param name="ativo">Flag 0 para inativo ou 1 para ativo</param>
        /// <param name="idUsuario">ID do usuário</param>
        /// <returns>Lista de produtos</returns>

        List<Produto> GetByDatas(DateTime? dataMin, DateTime? dataMax, int? ativo, Guid idUsuario);

    }
}
