using GraGasJM.Models;
using Microsoft.EntityFrameworkCore;

namespace GraGasJM.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<ItemVenda> ItensVenda { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<ItemCompra> ItensCompra { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>()
            .Property(p => p.PrecoVenda)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Produto>()
            .Property(p => p.PrecoCompra)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Venda>()
            .Property(v => v.ValorTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ItemVenda>()
            .Property(i => i.PrecoUnitario)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Compra>()
            .Property(c => c.ValorTotal)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ItemCompra>()
            .Property(i => i.PrecoUnitario)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ItemVenda>()
            .HasOne(i => i.Venda)
            .WithMany(v => v.Itens)
            .HasForeignKey(i => i.VendaId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemVenda>()
            .HasOne(i => i.Produto)
            .WithMany(p => p.ItensVenda)
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ItemCompra>()
            .HasOne(i => i.Compra)
            .WithMany(c => c.Itens)
            .HasForeignKey(i => i.CompraId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ItemCompra>()
            .HasOne(i => i.Produto)
            .WithMany(p => p.ItensCompra)
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Produto>().HasData(
            new Produto
            {
                Id = 1,
                Nome = "Botijão de Gás P13",
                Categoria = CategoriaProduto.Combustivel,
                Descricao = "Botijão de gás GLP 13kg",
                PrecoVenda = 115.00m,
                PrecoCompra = 90.00m,
                QuantidadeEstoque = 20,
                EstoqueMinimo = 5,
                Ativo = true,
                DataCadastro = DateTime.Now
            },
            new Produto
            {
                Id = 2,
                Nome = "Galão de Água Mineral 20L",
                Categoria = CategoriaProduto.Bebidas,
                Descricao = "Galão de água mineral 20 litros",
                PrecoVenda = 18.00m,
                PrecoCompra = 12.00m,
                QuantidadeEstoque = 30,
                EstoqueMinimo = 10,
                Ativo = true,
                DataCadastro = DateTime.Now
            },
            new Produto
            {
                Id = 3,
                Nome = "Saco de Carvão",
                Categoria = CategoriaProduto.Carvao,
                Descricao = "Saco de carvão vegetal para churrasco",
                PrecoVenda = 25.00m,
                PrecoCompra = 16.00m,
                QuantidadeEstoque = 15,
                EstoqueMinimo = 5,
                Ativo = true,
                DataCadastro = DateTime.Now
            }
        );
    }
}