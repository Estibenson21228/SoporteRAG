using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoporteRAG.Infrastructure.Data;
using SoporteRAG.Domain.Entities;

namespace SoporteRAG.Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Ticket> Tickets => Set<Ticket>();
        public DbSet<DocumentChunk> DocumentChunks => Set<DocumentChunk>();
        public DbSet<Embedding> Embeddings => Set<Embedding>();
        public DbSet<RagQuery> RagQueries => Set<RagQuery>();
        public DbSet<RagQuerySources> RagQuerySources => Set<RagQuerySources>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.ToTable("Ticket"); 
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TicketId).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Titulo).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Problema).IsRequired();
                entity.Property(e=>e.Solucion).IsRequired();
                entity.Property(e => e.Categoria).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Fecha).HasColumnType("datetime2");
                entity.Property(e => e.FechaCreacion).HasColumnType("datetime2");
            });

            modelBuilder.Entity<DocumentChunk>(entity =>
            {
                entity.ToTable("documentChunks");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.NombreDocumento).HasMaxLength(200).IsRequired();
                entity.Property(e => e.Contenido).IsRequired();
             
            });

            modelBuilder.Entity<Embedding>(entity =>
            {
                entity.ToTable("Embeddings");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.TipoRecurso).HasMaxLength(50).IsRequired();
                entity.Property(e => e.EmbeddingVector).IsRequired();
                entity.Property(e => e.TextoOriginal).IsRequired();
            });

            modelBuilder.Entity<RagQuery>(entity =>
            {
                entity.ToTable("ragQueries");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Pregunta).IsRequired();
                entity.Property(e => e.RespuestaGenerada);
                entity.Property(e => e.Fecha).HasColumnType("datetime2");
            });

            modelBuilder.Entity<RagQuerySources>(entity =>
            {
                entity.ToTable("RagQuerySources");
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.RagQuery).WithMany(r => r.Sources).HasForeignKey(e => e.RagQueryId);
            });
        }
    }
}
