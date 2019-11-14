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

        //[Required(ErrorMessage = "Please specify id")]
        [StringLength(15)]
        public string Nome { get; set; }

        [StringLength(15)]
        public string Sobrenome { get; set; }

        [StringLength(11)]
        public string Cpf { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public int? Sexo_Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Nascimento { get; set; }

        public virtual Tp_Sexo Tp_Sexo { get; set; }
    }
}
