using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //mapeamento..

namespace Projeto.Web.Models
{
    public class UsuarioViewModelLogin
    {
        [Required(ErrorMessage = "Por favor, informe seu Login.")]
        [Display(Name = "Login de Acesso:")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Por favor, informe sua Senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha de Acesso:")]
        public string Senha { get; set; }

        [Display(Name = "Mantenha-me conectado?")]
        public bool ManterConectado { get; set; }
    }
}