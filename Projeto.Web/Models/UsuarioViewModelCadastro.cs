using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //mapeamento..
using Projeto.Web.Validators; //validação customizada..

namespace Projeto.Web.Models
{
    public class UsuarioViewModelCadastro
    {
        [Required(ErrorMessage = "Por favor, informe seu nome.")]
        [Display(Name = "Informe seu Nome:")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe seu email.")]
        [EmailAddress(ErrorMessage = "Erro. Informe um email válido.")]
        [EmailValidator(ErrorMessage = "Erro. Este email já encontra-se em uso. Tente outro.")]
        [Display(Name = "Endereço de Email:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe seu login.")]
        [LoginValidator(ErrorMessage = "Erro. Este login já encontra-se em uso. Tente outro.")]
        [Display(Name = "Login de Acesso:")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Por favor, informe sua senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Informe sua Senha:")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Por favor, confirme sua senha.")]
        [Compare("Senha", ErrorMessage = "Erro. Senhas não conferem.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme sua Senha:")]
        public string SenhaConfirm { get; set; }

        [Required(ErrorMessage = "Por favor, envie sua foto.")]
        [FotoValidator(ErrorMessage = "Erro. Envie apenas imagens JPG de até 1MB.")]
        [Display(Name = "Envie sua Foto:")]
        public HttpPostedFileBase Foto { get; set; }

    }
}