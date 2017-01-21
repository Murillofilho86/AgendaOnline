using System;
using Projeto.DAL.Entities.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.DAL.Entities
{
    [Table("Tarefa")]
    public class Tarefa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdTarefa")]
        public int IdTarefa { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Titulo")]
        public string Titulo { get; set; }

        [Required]
        [StringLength(250)]
        [Column("Descricao")]
        public string Descricao { get; set; }

        [Required]
        [Column("DataHora")]
        public DateTime DataHora { get; set; }

        [Required]
        [Column("Status")]
        public Status Status { get; set; }

        [Required]
        [Column("Tipo")]
        public Tipo Tipo { get; set; }

        [Required]
        [Column("IdUsuarioFK")] //chave estrangeira
        public int IdUsuario { get; set; } //foreign key

        #region Relacionamentos

        [ForeignKey("IdUsuario")] //nome da propriedade da classe
        public virtual Usuario Usuario { get; set; } //Associação (TER-1)

        #endregion
    }
}
