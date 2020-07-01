using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PWABlog.Models.Blog.Etiqueta
{
    public class EtiquetaOrmService
    {
        private readonly DatabaseContext _databaseContext;

        public EtiquetaOrmService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<EtiquetaEntity> ObterEtiquetas()
        {
            return _databaseContext.Etiquetas
                .Include(c => c.Categoria)
                .ToList();
        }

        public EtiquetaEntity ObterEtiquetaPorId(int idEtiqueta)
        {
            var etiqueta = _databaseContext.Etiquetas.Find(idEtiqueta);

            return etiqueta;
        }

        public List<EtiquetaEntity> PesquisarEtiquetasPorNome(string nomeEtiqueta)
        {
            return _databaseContext.Etiquetas.Where(c => c.Nome.Contains(nomeEtiqueta)).ToList();
        }

        public EtiquetaEntity CriarEtiqueta(string nome, int idCategoria)
        {
            // Verificar se um nome foi passado
            if (nome == null) {
                throw new Exception("A Etiqueta precisa de um nome!");
            }
            
            // Verificar existência da Categoria da Etiqueta
            var categoria = _databaseContext.Categorias.Find(idCategoria);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Etiqueta não foi encontrada!");
            }

            // Criar nova Etiqueta
            var novaEtiqueta = new EtiquetaEntity
            {
                Nome = nome, 
                Categoria = categoria
            };
            _databaseContext.Etiquetas.Add(novaEtiqueta);
            _databaseContext.SaveChanges();

            return novaEtiqueta;
        }

        public EtiquetaEntity EditarEtiqueta(int id, string nome, int idCategoria)
        {
            // Obter Etiqueta a Editar
            var etiqueta = _databaseContext.Etiquetas.Find(id);
            if (etiqueta == null) {
                throw new Exception("Etiqueta não encontrada!");
            }
            
            // Verificar existência da Categoria da Etiqueta
            var categoria = _databaseContext.Categorias.Find(idCategoria);
            if (categoria == null) {
                throw new Exception("A Categoria informada para a Etiqueta não foi encontrada!");
            }

            // Atualizar dados da Etiqueta
            etiqueta.Nome = nome;
            etiqueta.Categoria = categoria;
            _databaseContext.SaveChanges();

            return etiqueta;
        }

        public bool RemoverEtiqueta(int id)
        {
            // Obter Etiqueta a Remover
            var etiqueta = _databaseContext.Etiquetas.Find(id);
            if (etiqueta == null) {
                throw new Exception("Etiqueta não encontrada!");
            }

            // Remover Etiqueta
            _databaseContext.Etiquetas.Remove(etiqueta);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}