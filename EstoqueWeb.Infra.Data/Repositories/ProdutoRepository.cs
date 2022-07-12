using Dapper;
using EstoqueWeb.Infra.Data.Entities;
using EstoqueWeb.Infra.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueWeb.Infra.Data.Repositories
{
    /// <summary>
    /// classe para implementar as operações
    /// de banco de dados para Produto
    /// </summary>
    public class ProdutoRepository : IProdutoRepository
    {
        //atributo
        private readonly string _connectionString;
        
        //método construtor utilizado para que possamos passar
        // o valor da connectionstring para a classe de repositorio
        public ProdutoRepository(string connectionstring)
        {
            _connectionString = connectionstring;
        }

        public void Create(Produto obj)
        {
            var query = @"
                INSERT INTO PRODUTO
                    (ID, NOME, PRECO, QUANTIDADE, DATAVALIDADE, DESCRICAO, DATAINCLUSAO, DATAALTERACAO, IDUSUARIO)
                VALUES
                    (@Id, @Nome, @Preco, @Quantidade, @DataValidade, @Descricao, @DataInclusao, @DataAlteracao, @IdUsuario)
            ";

            //conectando no banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                //executando a gravacao do evento na base de dados
                connection.Execute(query, obj);
            }
        }

        public void Update(Produto obj)
        {
            var query = @"
                UPDATE PRODUTO
                SET
                    NOME = @Nome,
                    PRECO = @Preco,
                    QUANTIDADE = @Quantidade,       
                    DATAVALIDADE = @DataValidade,
                    DESCRICAO = @Descricao,
                    DATAALTERACAO = @DataAlteracao,
                    ATIVO = @Ativo
                WHERE
                    ID = @Id
            ";

            //conectando no banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                //executando a alteracao do evento na base de dados
                connection.Execute(query, obj);
            }
        }

        public void Delete(Produto obj)
        {
            var query = @"
                DELETE FROM PRODUTO
                WHERE ID = @Id
            ";

            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, obj);
            }
        }

        public List<Produto> GetAll()
        {
            var query = @"
                SELECT * FROM PRODUTO
                ORDER BY DATAVALIDADE DESC
            ";

            using(var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Produto>(query)
                    .ToList();
            }
        }

        public Produto? GetById(Guid id)
        {
            var query = @"
                SELECT * FROM PRODUTO
                WHERE ID = @id
            ";

            using( var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Produto>(query, new { id })
                    .FirstOrDefault();
            }
        }

        public List<Produto> GetByDatas(DateTime? dataMin, DateTime? dataMax, int? ativo, Guid idUsuario)
        {
            var query = @"
                SELECT * FROM PRODUTO
                WHERE ATIVO = @Ativo AND DATAVALIDADE BETWEEN @dataMin AND @dataMax
                ORDER BY DATAVALIDADE ASC
            ";

            using(var connection = new SqlConnection(_connectionString))
            {
                return connection
                    .Query<Produto>(query, new { ativo, dataMin, dataMax, idUsuario})
                    .ToList();
            }
        }
    }
}
