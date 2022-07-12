using EstoqueWeb.Infra.Data.Entities;
using EstoqueWeb.Reports.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueWeb.Reports.Services
{
    /// <summary>
    /// Classe para geração do relatório em formato EXCEL
    /// </summary>
    public class ProdutoReportServiceExcel : IProdutoReportService
    {
        /// <summary>
        /// Método para geração do relatório
        /// </summary>
        /// <param name="dataMin">Data de inicio da pesquisa</param>
        /// <param name="dataMax">Data de termino da pesquisa</param>
        /// <param name="produtos">Lista de produtos</param>
        /// <returns>Arquivo em formato byte</returns>
        public byte[] Create(DateTime dataMin, DateTime dataMax, List<Produto> produtos)
        {
            //configurando a biblioteca para uso não comercial
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //criando a planilha excel
            using (var excelPackage = new ExcelPackage())
            {
                //criando a planilha
                var sheet = excelPackage.Workbook.Worksheets.Add("Produtos");

                //título da planila
                sheet.Cells["A1"].Value = "Relatório de produtos";
                var titulo = sheet.Cells["A1:I1"];
                titulo.Merge = true;
                titulo.Style.Font.Size = 18;
                titulo.Style.Font.Bold = true;

                sheet.Cells["A2"].Value = "Matheus Estoques - Estoque Web";
                var subtitulo = sheet.Cells["A2"];
                titulo.Style.Font.Size = 14;

                //inserindo as datas de pesquisa
                sheet.Cells["A4"].Value = "Data de início:";
                sheet.Cells["B4"].Value = dataMin.ToString("dd/MM/yyyy");

                sheet.Cells["A5"].Value = "Data de término:";
                sheet.Cells["B5"].Value = dataMax.ToString("dd/MM/yyyy");

                //cabeçalho das colunas para impressão dos eventos
                sheet.Cells["A7"].Value = "ID do Produto";
                sheet.Cells["B7"].Value = "Nome do Produto";
                sheet.Cells["C7"].Value = "Preço";
                sheet.Cells["D7"].Value = "Quantidade";
                sheet.Cells["E7"].Value = "Data de Validade";
                sheet.Cells["F7"].Value = "Descrição";
                sheet.Cells["G7"].Value = "Ativo";
                sheet.Cells["H7"].Value = "Data de Inclusão";
                sheet.Cells["I7"].Value = "Data de Alteração";

                var cabecalho = sheet.Cells["A7:I7"];
                cabecalho.Style.Font.Color.SetColor(ColorTranslator.FromHtml("#FFFFFF"));
                cabecalho.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cabecalho.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#000000"));

                var linha = 8;

                //varrendo e imprimindo os eventos
                foreach (var item in produtos)
                {
                    sheet.Cells[$"A{linha}"].Value = item.Id;
                    sheet.Cells[$"B{linha}"].Value = item.Nome;
                    sheet.Cells[$"C{linha}"].Value = (item.Preco).ToString();
                    sheet.Cells[$"D{linha}"].Value = item.Quantidade.ToString();
                    sheet.Cells[$"E{linha}"].Value = ((DateTime)item.DataValidade).ToString("dd/MM/yyyy");
                    sheet.Cells[$"F{linha}"].Value = item.Descricao;
                    sheet.Cells[$"G{linha}"].Value = item.Ativo == 1 ? "Sim" : "Não";
                    sheet.Cells[$"H{linha}"].Value = ((DateTime)item.DataInclusao).ToString("dd/MM/yyyy HH:mm");
                    sheet.Cells[$"I{linha}"].Value = ((DateTime)item.DataAlteracao).ToString("dd/MM/yyyy HH:mm");
                   
                    if (linha % 2 != 0) //linha é impar
                    {
                        var conteudo = sheet.Cells[$"A{linha}:I{linha}"];
                        conteudo.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        conteudo.Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#EEEEEE"));
                    }

                    linha++;
                }

                //ajustar a largura das colunas
                sheet.Cells["A:I"].AutoFitColumns();

                //borda no grid
                var dados = sheet.Cells[$"A7:I{linha - 1}"];
                dados.Style.Border.BorderAround(ExcelBorderStyle.Medium);

                //retornar a planilha excel em formato de arquivo
                return excelPackage.GetAsByteArray();
            }
        }
    }
}
