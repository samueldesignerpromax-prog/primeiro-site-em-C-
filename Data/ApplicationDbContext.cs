using Microsoft.EntityFrameworkCore;
using PadariaWeb.Models;

namespace PadariaWeb.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Produto>().HasData(
                new Produto
                {
                    Id = 1,
                    Nome = "Pão Francês",
                    Descricao = "Pão francês tradicional, crocante por fora e macio por dentro",
                    Preco = 0.50m,
                    Estoque = 100,
                    Categoria = "Pães",
                    Destaque = true,
                    DataCadastro = DateTime.Now
                },
                new Produto
                {
                    Id = 2,
                    Nome = "Bolo de Chocolate",
                    Descricao = "Bolo fofinho de chocolate com cobertura",
                    Preco = 45.00m,
                    Estoque = 20,
                    Categoria = "Bolos",
                    Destaque = true,
                    DataCadastro = DateTime.Now
                },
                new Produto
                {
                    Id = 3,
                    Nome = "Croissant",
                    Descricao = "Croissant folhado e amanteigado",
                    Preco = 8.00m,
                    Estoque = 50,
                    Categoria = "Salgados",
                    Destaque = false,
                    DataCadastro = DateTime.Now
                }
            );
        }
    }
}
