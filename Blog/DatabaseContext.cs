using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PWABlog.Models.Blog.Autor;
using PWABlog.Models.Blog.Categoria;
using PWABlog.Models.Blog.Etiqueta;
using PWABlog.Models.Blog.Postagem;
using Microsoft.EntityFrameworkCore;
using PWABlog.Models.Blog.Postagem.Revisao;
using PWABlog.Models.Blog.Postagem.Classificacao;
using PWABlog.Models.Blog.Postagem.Comentario;
using PWABlog.Models.ControleDeAcesso;

namespace PWABlog
{
    public class DatabaseContext : IdentityDbContext<Usuario, Papel, int>
    {
        public DbSet<CategoriaEntity> Categorias { get; set; }
        
        public DbSet<AutorEntity> Autores { get; set; }
        
        public DbSet<EtiquetaEntity> Etiquetas { get; set; }
        
        public DbSet<PostagemEntity> Postagens { get; set; }
        
        public DbSet<RevisaoEntity> Revisoes { get; set; }
        
        public DbSet<ComentarioEntity> Comentarios { get; set; }
        
        public DbSet<ClassificacaoEntity> Classificacoes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseMySql("Server=localhost; User=root; password=1234!@#$; Database=blogpwa2020");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapear relacionamentos entre as entidades
            // N:N (Postagens -> Etiquetas [PostagemEtiquetaEntity])
            modelBuilder.Entity<PostagemEtiquetaEntity>().ToTable("PostagensEtiquetas");
            
            modelBuilder.Entity<PostagemEtiquetaEntity>()
                .HasOne(pe => pe.Postagem)
                .WithMany(p => p.PostagensEtiquetas);
                
            modelBuilder.Entity<PostagemEtiquetaEntity>()
                .HasOne(pe => pe.Etiqueta)
                .WithMany(e => e.PostagensEtiquetas);
                
        }
    }
}