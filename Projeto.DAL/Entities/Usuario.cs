using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; //ORM
using System.ComponentModel.DataAnnotations.Schema; //ORM

namespace Projeto.DAL.Entities
{
    [Table("Usuario")] //nome da tabela..
    public class Usuario
    {
        [Key] //chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Nome")]
        public string Nome { get; set; }

        [Required]
        [Index("UniqueEmail", IsUnique = true)]
        [StringLength(50)]
        [Column("Email")]
        public string Email { get; set; }

        [Required]
        [Index("UniqueLogin", IsUnique = true)]
        [StringLength(25)]
        [Column("Login")]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Senha")]
        public string Senha { get; set; }

        [Required]
        [Index("UniqueFoto", IsUnique = true)]
        [StringLength(50)]
        [Column("Foto")]
        public string Foto { get; set; }

        #region Relacionamentos

        public virtual ICollection<Tarefa> Tarefas { get; set; } //TER-Muitos

        #endregion

    }
}
