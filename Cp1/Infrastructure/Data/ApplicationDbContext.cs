using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;
using Cp1.Domain.Entities;

namespace Cp1.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // DbSets para as entidades
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Especialidade> Especialidades { get; set; }
    public DbSet<Consulta> Consultas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração da entidade Paciente
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.ToTable("PACIENTES");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            entity.Property(p => p.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
            entity.Property(p => p.DataNascimento).HasColumnName("DATA_NASCIMENTO").IsRequired();
            entity.Property(p => p.Cpf).HasColumnName("CPF").HasMaxLength(11).IsRequired();
            entity.Property(p => p.Telefone).HasColumnName("TELEFONE").HasMaxLength(20);
            entity.Property(p => p.Endereco).HasColumnName("ENDERECO").HasMaxLength(200);
            
            // Índice único para CPF
            entity.HasIndex(p => p.Cpf).IsUnique();
        });

        // Configuração da entidade Medico
        modelBuilder.Entity<Medico>(entity =>
        {
            entity.ToTable("MEDICOS");
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            entity.Property(m => m.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
            entity.Property(m => m.Crm).HasColumnName("CRM").HasMaxLength(20).IsRequired();
            entity.Property(m => m.Telefone).HasColumnName("TELEFONE").HasMaxLength(20);
            entity.Property(m => m.Email).HasColumnName("EMAIL").HasMaxLength(100);
            
            // Índice único para CRM
            entity.HasIndex(m => m.Crm).IsUnique();
        });

        // Configuração da entidade Especialidade
        modelBuilder.Entity<Especialidade>(entity =>
        {
            entity.ToTable("ESPECIALIDADES");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            entity.Property(e => e.Nome).HasColumnName("NOME").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Descricao).HasColumnName("DESCRICAO").HasMaxLength(500);
            
            // Índice único para Nome
            entity.HasIndex(e => e.Nome).IsUnique();
        });

        // Configuração da entidade Consulta
        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.ToTable("CONSULTAS");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            entity.Property(c => c.DataHora).HasColumnName("DATA_HORA").IsRequired();
            entity.Property(c => c.Status).HasColumnName("STATUS").HasMaxLength(1).IsRequired();
            entity.Property(c => c.Observacoes).HasColumnName("OBSERVACOES").HasMaxLength(1000);
            
            // Chaves estrangeiras
            entity.Property(c => c.PacienteId).HasColumnName("PACIENTE_ID").IsRequired();
            entity.Property(c => c.MedicoId).HasColumnName("MEDICO_ID").IsRequired();
            entity.Property(c => c.EspecialidadeId).HasColumnName("ESPECIALIDADE_ID").IsRequired();
            
            // Relacionamentos
            entity.HasOne(c => c.Paciente)
                .WithMany(p => p.Consultas)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(c => c.Medico)
                .WithMany(m => m.Consultas)
                .HasForeignKey(c => c.MedicoId)
                .OnDelete(DeleteBehavior.Restrict);
            
            entity.HasOne(c => c.Especialidade)
                .WithMany(e => e.Consultas)
                .HasForeignKey(c => c.EspecialidadeId)
                .OnDelete(DeleteBehavior.Restrict);
            
            // Índice para DataHora para melhor performance em consultas
            entity.HasIndex(c => c.DataHora);
            entity.HasIndex(c => c.Status);
        });
    }
}

