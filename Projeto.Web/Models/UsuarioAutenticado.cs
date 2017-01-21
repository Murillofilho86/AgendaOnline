using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Web.Models
{
    //Classe para modelar os dados do ticket
    //do Usuario Autenticado no sistema
    public class UsuarioAutenticado
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string Foto { get; set; }
        public DateTime DataHoraAcesso { get; set; }
    }
}