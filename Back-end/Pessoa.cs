namespace Back_end
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pessoa")]
    public partial class Pessoa
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Sobrenome { get; set; }

        [StringLength(11)]
        public string Cpf { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public int? Sexo_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Nascimento { get; set; }

        public virtual Tp_Sexo Tp_Sexo { get; set; }
    }
}
