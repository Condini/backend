namespace Back_end
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class PessoaContext : DbContext
    {
        public PessoaContext()
            : base("name=PessoaContext")
        {
        }

        public virtual DbSet<Pessoa> Pessoa { get; set; }
        public virtual DbSet<Tp_Sexo> Tp_Sexo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoa>()
                .Property(e => e.Sobrenome)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoa>()
                .Property(e => e.Cpf)
                .IsUnicode(false);

            modelBuilder.Entity<Pessoa>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Tp_Sexo>()
                .Property(e => e.Sexo)
                .IsUnicode(false);

            modelBuilder.Entity<Tp_Sexo>()
                .HasMany(e => e.Pessoa)
                .WithOptional(e => e.Tp_Sexo)
                .HasForeignKey(e => e.Sexo_Id);
        }
    }
}
