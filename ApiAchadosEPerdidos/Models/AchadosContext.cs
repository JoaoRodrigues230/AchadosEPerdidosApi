using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiAchadosEPerdidos.Models;

public partial class AchadosContext : DbContext
{
    public AchadosContext()
    {
    }

    public AchadosContext(DbContextOptions<AchadosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Devolucao> Devolucaos { get; set; }

    public virtual DbSet<Imagem> Imagems { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<Local> Locals { get; set; }

    public virtual DbSet<Logmovimentacao> Logmovimentacaos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categoria_pkey");

            entity.ToTable("categoria");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Devolucao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("devolucao_pkey");

            entity.ToTable("devolucao");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Datadevolucao)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("datadevolucao");
            entity.Property(e => e.Documentoretirada)
                .HasMaxLength(50)
                .HasColumnName("documentoretirada");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Nomeretirada)
                .HasMaxLength(100)
                .HasColumnName("nomeretirada");
            entity.Property(e => e.Observacao)
                .HasMaxLength(255)
                .HasColumnName("observacao");
            entity.Property(e => e.Usuarioresponsavelid).HasColumnName("usuarioresponsavelid");

            entity.HasOne(d => d.Item).WithMany(p => p.Devolucaos)
                .HasForeignKey(d => d.Itemid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("devolucao_itemid_fkey");

            entity.HasOne(d => d.Usuarioresponsavel).WithMany(p => p.Devolucaos)
                .HasForeignKey(d => d.Usuarioresponsavelid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("devolucao_usuarioresponsavelid_fkey");
        });

        modelBuilder.Entity<Imagem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("imagem_pkey");

            entity.ToTable("imagem");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Urlimagem1)
                .HasMaxLength(500)
                .HasColumnName("urlimagem1");
            entity.Property(e => e.Urlimagem2)
                .HasMaxLength(500)
                .HasColumnName("urlimagem2");
            entity.Property(e => e.Urlimagem3)
                .HasMaxLength(500)
                .HasColumnName("urlimagem3");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("item_pkey");

            entity.ToTable("item");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Categoriaid).HasColumnName("categoriaid");
            entity.Property(e => e.Dataachado)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dataachado");
            entity.Property(e => e.Descricao).HasColumnName("descricao");
            entity.Property(e => e.Imagemid).HasColumnName("imagemid");
            entity.Property(e => e.Localid).HasColumnName("localid");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'DISPONIVEL'::character varying")
                .HasColumnName("status");
            entity.Property(e => e.Usuarioqueachouid).HasColumnName("usuarioqueachouid");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Items)
                .HasForeignKey(d => d.Categoriaid)
                .HasConstraintName("item_categoriaid_fkey");

            entity.HasOne(d => d.Imagem).WithMany(p => p.Items)
                .HasForeignKey(d => d.Imagemid)
                .HasConstraintName("fk_item_imagem");

            entity.HasOne(d => d.Local).WithMany(p => p.Items)
                .HasForeignKey(d => d.Localid)
                .HasConstraintName("item_localid_fkey");

            entity.HasOne(d => d.Usuarioqueachou).WithMany(p => p.Items)
                .HasForeignKey(d => d.Usuarioqueachouid)
                .HasConstraintName("item_usuarioqueachouid_fkey");
        });

        modelBuilder.Entity<Local>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("local_pkey");

            entity.ToTable("local");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .HasColumnName("descricao");
            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Logmovimentacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("logmovimentacao_pkey");

            entity.ToTable("logmovimentacao");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Dataocorrencia)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("dataocorrencia");
            entity.Property(e => e.Detalhes)
                .HasColumnType("jsonb")
                .HasColumnName("detalhes");
            entity.Property(e => e.Itemid).HasColumnName("itemid");
            entity.Property(e => e.Tipoacao)
                .HasMaxLength(50)
                .HasColumnName("tipoacao");
            entity.Property(e => e.Usuarioactorid).HasColumnName("usuarioactorid");

            entity.HasOne(d => d.Item).WithMany(p => p.Logmovimentacaos)
                .HasForeignKey(d => d.Itemid)
                .HasConstraintName("logmovimentacao_itemid_fkey");

            entity.HasOne(d => d.Usuarioactor).WithMany(p => p.Logmovimentacaos)
                .HasForeignKey(d => d.Usuarioactorid)
                .HasConstraintName("logmovimentacao_usuarioactorid_fkey");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Cpf, "uq_usuario_cpf").IsUnique();

            entity.HasIndex(e => e.Ra, "uq_usuario_ra").IsUnique();

            entity.HasIndex(e => e.Email, "usuario_email_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Cpf)
                .HasMaxLength(20)
                .HasColumnName("cpf");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .HasColumnName("nome");
            entity.Property(e => e.Ra)
                .HasMaxLength(20)
                .HasColumnName("ra");
            entity.Property(e => e.Senha)
                .HasMaxLength(255)
                .HasColumnName("senha");
            entity.Property(e => e.Tipo)
                .HasMaxLength(20)
                .HasColumnName("tipo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
