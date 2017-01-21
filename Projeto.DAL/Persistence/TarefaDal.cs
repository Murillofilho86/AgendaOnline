using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity; //entityframework
using Projeto.DAL.Generics;
using Projeto.DAL.DataSource;
using Projeto.DAL.Entities;

namespace Projeto.DAL.Persistence
{
    public class TarefaDal : GenericDal<Tarefa>
    {
        //método para listar as tarefas de um usuario
        //dentro de um perido de datas...
        public List<Tarefa> Find(int IdUsuario, DateTime DataIni, DateTime DataFim)
        {
            using(Conexao Con = new Conexao())
            {
                //SQL -> select * from Tarefa where IdUsuarioFK = @v1
                //       and DataHora between @v1 and @v2
                //       order by DataHora asc

                return Con.Tarefa
                        .Where(t => t.IdUsuario == IdUsuario
                                 && t.DataHora >= DataIni
                                 && t.DataHora <= DataFim)
                        .OrderBy(t => t.DataHora)
                        .ToList();
            }
        }
    }
}
