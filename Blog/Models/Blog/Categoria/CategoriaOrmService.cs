using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PWABlog.Models.Blog.Categoria
{
    public class CategoriaOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public CategoriaOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<CategoriaEntity> ObterCategorias()
        {
            // INÍCIO DOS EXEMPLOS

            /**********************************************************************************************************/
            /*** OBTER UM ÚNICO OBJETO                                                                                */
            /**********************************************************************************************************/

            // First = Obter a primeira categoria retornada pela consulta
            //var primeiraCategoria = _databaseContext.Categorias.First();

            // FirstOrDefault = Mesmo do First, porém retorna nulo caso não encontre nenhuma
            //var primeiraCategoriaOuNulo = _databaseContext.Categorias.FirstOrDefault();

            // Single = Obter um único registro do banco de dados
            //var algumaCategoriaEspecifica = _databaseContext.Categorias.Single(c => c.Id == 3);

            // SingleOrDefault = Mesmo do Sigle, porém retorna nulo caso não encontre nenhuma
            //var algumaCategoriaOuNulo = _databaseContext.Categorias.SingleOrDefault(c => c.Id == 3);

            // Find = Equivalente ao SingleOrDefault, porém fazendo uma busca por uma propriedade chave
            //var encontrarCategoria = _databaseContext.Categorias.Find(3);


            /**********************************************************************************************************/
            /*** OBTER MÚLTIPLOS OBJETOS                                                                              */
            /**********************************************************************************************************/

            // ToList
            //var todasCategorias = _databaseContext.Categorias.ToList();


            /***********/
            /* FILTROS */
            /***********/

            //var categoriasComALetraG = _databaseContext.Categorias.Where(c => c.Nome.StartsWith("G")).ToList();
            /*
            var categoriasComALetraMouL = _databaseContext.Categorias
                .Where(c => c.Nome.StartsWith("M") || c.Nome.StartsWith("L"))
                .ToList();
            */


            /*************/
            /* ORDENAÇÃO */
            /*************/

            //var categoriasEmOrdemAlfabetica = _databaseContext.Categorias.OrderBy(c => c.Nome).ToList();
            /*
            var categoriasEmOrdemAlfabeticaInversa =
                _databaseContext.Categorias.OrderByDescending(c => c.Nome).ToList();
            */

            
            /**************************/
            /* ENTIDADES RELACIONADAS */
            /**************************/

            /*
            var categoriasESuasEtiquetas = _databaseContext.Categorias
                .Include(c => c.Etiquetas)
                .ToList();

            var categoriasSemEtiquetas = _databaseContext.Categorias
                .Where(c => c.Etiquetas.Count == 0)
                .ToList();

            var categoriasComEtiquetas = _databaseContext.Categorias
                .Where(c => c.Etiquetas.Count > 0)
                .ToList();
            */

            // FIM DOS EXEMPLOS


            // Código de fato necessário para o método
            return _databaseContext.Categorias
                .Include(c => c.Etiquetas)
                .ToList();
        }

        public CategoriaEntity ObterCategoriaPorId(int idCategoria)
        {
            var categoria = _databaseContext.Categorias.Find(idCategoria);

            return categoria;
        }

        public List<CategoriaEntity> PesquisarCategoriasPorNome(string nomeCategoria)
        {
            return _databaseContext.Categorias.Where(c => c.Nome.Contains(nomeCategoria)).ToList();
        }

        public CategoriaEntity CriarCategoria(string nome)
        {
            // Criar nova Categoria
            var novaCategoria = new CategoriaEntity {Nome = nome};
            _databaseContext.Categorias.Add(novaCategoria);
            _databaseContext.SaveChanges();

            return novaCategoria;
        }

        public CategoriaEntity EditarCategoria(int id, string nome)
        {
            // Obter Categoria a Editar
            var categoria = _databaseContext.Categorias.Find(id);
            if (categoria == null) {
                throw new Exception("Categoria não encontrada!");
            }

            // Atualizar dados da Categoria
            categoria.Nome = nome;
            _databaseContext.SaveChanges();

            return categoria;
        }

        public bool RemoverCategoria(int id)
        {
            // Obter Categoria a Remover
            var categoria = _databaseContext.Categorias.Find(id);
            if (categoria == null) {
                throw new Exception("Categoria não encontrada!");
            }

            // Remover Categoria
            _databaseContext.Categorias.Remove(categoria);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}