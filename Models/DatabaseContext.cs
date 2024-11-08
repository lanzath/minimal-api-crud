﻿using Microsoft.EntityFrameworkCore;

namespace MinimalCrud.Models;

// Classe gerada pela comando `dotnet ef dotnet ef dbcontext scaffold "Data Source=database.db" Microsoft.EntityFrameworkCore.Sqlite --output-dir Models`.
// Classe responsável pelo mapeamento das propriedades da classe Contact para as colunas da tabela contacts.
public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    { }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    { }

    public virtual DbSet<Contact> Contacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite("Data Source=database.db"); // Configuração da fonte de dados (DB)

    // Mapeamento da classe para tabela.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .HasColumnName("createdAt");
            entity.Property(e => e.Email)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("NULL")
                .HasColumnType("TIMESTAMP")
                .HasColumnName("updatedAt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
