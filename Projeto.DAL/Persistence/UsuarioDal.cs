using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; //entityframework
using Projeto.DAL.Entities; //entidades
using Projeto.DAL.Generics; //genericdal
using Projeto.DAL.DataSource; //conexão

namespace Projeto.DAL.Persistence
{
    public class UsuarioDal : GenericDal<Usuario>
    {
        //método para verificar se um login ja existe no banco..
        public bool HasLogin(string Login)
        {
            using(Conexao Con = new Conexao())
            {
                //SQL -> select count(*) from Usuario where Login = @v1
                return Con.Usuario
                        .Where(u => u.Login.Equals(Login))
                        .Count() > 0;
            }
        }

        //método para verificar se um email ja existe no banco..
        public bool HasEmail(string Email)
        {
            using(Conexao Con = new Conexao())
            {
                //SQL -> select count(*) from Usuario where Email = @v1
                return Con.Usuario
                        .Where(u => u.Email.Equals(Email))
                        .Count() > 0;
            }
        }

        //método para buscar 1 Usuario pelo Login e Senha
        public Usuario Find(string Login, string Senha)
        {
            using(Conexao Con = new Conexao())
            {
                //SQL -> select * from Usuario where Login = @v1 and Senha = @v2
                return Con.Usuario
                        .Where(u => u.Login.Equals(Login)
                                 && u.Senha.Equals(Senha))
                        .FirstOrDefault(); //primeiro registro encontrado ou null
            }
        }

    }
}
