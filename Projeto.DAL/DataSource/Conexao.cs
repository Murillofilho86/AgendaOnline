using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; //entityframework
using System.Configuration; //connectionstring
using Projeto.DAL.Entities; //entidades

namespace Projeto.DAL.DataSource
{
    //Regra 1) Classe de Conexao HERDA DbContext
    public class Conexao : DbContext
    {
        //Regra 2) Construtor que envie para a DbContext o caminho da conexão..
        public Conexao()
            : base(ConfigurationManager.ConnectionStrings["aula"].ConnectionString)
        {
            //base -> faz uma chamada ao construtor da superclasse DbContext
            //passando a connectionstring..
        }

        //Regra 3) Declarar um DbSet para cada classe de entidade mapeada..
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
    }
}
