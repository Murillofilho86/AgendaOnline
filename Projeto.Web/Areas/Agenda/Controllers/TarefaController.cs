using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Web.Areas.Agenda.Models;
using Projeto.DAL.Entities;
using Projeto.DAL.Persistence;
using Projeto.Web.Models;
using Newtonsoft.Json;
using Microsoft.Reporting.WebForms;

namespace Projeto.Web.Areas.Agenda.Controllers
{
    [Authorize]
    public class TarefaController : Controller
    {
        // GET: /Agenda/Tarefa/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Agenda/Tarefa/Cadastro
        public ActionResult Cadastro()
        {
            return View();
        }

        // GET: /Agenda/Tarefa/Consulta
        public ActionResult Consulta()
        {
            return View();
        }



        // POST: /Agenda/Tarefa/CadastrarTarefa
        [HttpPost] //recebe requisições do tipo POST
        [ValidateAntiForgeryToken] //requerer ticket de acesso
        public ActionResult CadastrarTarefa(TarefaViewModelCadastro model)
        {
            //verificar se os dados da model passaram na validação..
            if(ModelState.IsValid)
            {
                try
                {
                    //Resgatar o usuario to ticket..
                    string ticket = HttpContext.User.Identity.Name;
                    UsuarioAutenticado auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(ticket);

                    Tarefa t = new Tarefa(); //entidade..
                    t.Titulo = model.Titulo;
                    t.Descricao = model.Descricao;
                    t.DataHora = model.DataHora;
                    t.Status = model.Status;
                    t.Tipo = model.Tipo;
                    t.IdUsuario = auth.IdUsuario; //foreign key..

                    TarefaDal d = new TarefaDal();
                    d.Insert(t); //gravando..

                    ViewBag.Mensagem = "Tarefa " + t.Titulo + ", cadastrado com sucesso.";
                    ModelState.Clear(); //limpar o conteudo do formulário..
                }
                catch(Exception e)
                {
                    //imprimindo mensagem de erro..
                    ViewBag.Mensagem = e.Message;
                }
            }

            return View("Cadastro"); //nome da página..
        }


        //método para consultar as tarefas...
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConsultarTarefas(TarefaViewModelConsulta model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    //trazer o usuario autenticado..
                    string ticket = HttpContext.User.Identity.Name;
                    UsuarioAutenticado auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(ticket);
                    
                    TarefaDal d = new TarefaDal(); //persistencia..
                    model.Tarefas = d.Find(auth.IdUsuario, model.DataInicio, model.DataFim);
                    
                    if(model.Tarefas.Count > 0)
                    {
                        ViewBag.Mensagem = "Consulta realizada com sucesso.";

                        //armazenar as datas em sessão...
                        Session.Add("datainicio", model.DataInicio);
                        Session.Add("datafim", model.DataFim);
                    }
                    else
                    {
                        ViewBag.Mensagem = "Nenhuma tarefa foi encontrada.";
                    }
                }
            }
            catch(Exception e)
            {
                ViewBag.Mensagem = e.Message;
            }

            return View("Consulta", model);
        }


        [HttpGet]
        public ActionResult ExcluirTarefa(int id)
        {
            try
            {
                string ticket = HttpContext.User.Identity.Name;
                UsuarioAutenticado auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(ticket);

                //buscar a tarefa no banco de dados..
                TarefaDal d = new TarefaDal(); //persistencia..
                Tarefa t = d.FindById(id);

                //verificar se a tarefa pertence ao usuario logado..
                if(t.IdUsuario == auth.IdUsuario)
                {
                    d.Delete(t); //excluir a tarefa..
                    ViewBag.Mensagem = "Tarefa " + t.Titulo + ", excluido com sucesso.";
                }
            }
            catch(Exception e)
            {
                ViewBag.Mensagem = e.Message;
            }

            return View("Consulta");
        }



        //método para gerar o relatorio de tarefas da agenda..
        [HttpPost] //recebe requisição do tipo POST
        public ActionResult GerarRelatorio(string Tipo)
        {
            try
            {
                //resgatar as datas que estão em sessão...
                DateTime DataInicio = (DateTime) Session["datainicio"];
                DateTime DataFim = (DateTime) Session["datafim"];

                //resgatar o usuario gravado no ticket..
                string ticket = HttpContext.User.Identity.Name;
                UsuarioAutenticado auth = JsonConvert.DeserializeObject<UsuarioAutenticado>(ticket);

                //consultar as tarefas..
                TarefaDal d = new TarefaDal(); //persistencia..
                List<Tarefa> lista = d.Find(auth.IdUsuario, DataInicio, DataFim);

                //Gerando o relatório...
                //1º) Caminho do relatório...
                string path = HttpContext.Server.MapPath("/Reports/RelatorioAgenda.rdlc");

                //2º) Parametros do relatório...
                ReportParameter[] parametros = new ReportParameter[3];
                parametros[0] = new ReportParameter("DataInicio", DataInicio.ToString("ddd dd/MM/yyyy"));
                parametros[1] = new ReportParameter("DataTermino", DataFim.ToString("ddd dd/MM/yyyy"));
                parametros[2] = new ReportParameter("Usuario", auth.Nome);

                //3º) Gerar o relatório...
                ReportViewer relatorio = new ReportViewer();
                relatorio.LocalReport.ReportPath = path; //caminho do relatório..
                relatorio.LocalReport.DataSources.Add(new ReportDataSource("DataSetAgenda", lista));
                relatorio.LocalReport.SetParameters(parametros);

                switch(Int32.Parse(Tipo))
                {
                    case 1: //PDF                       
                        return File(relatorio.LocalReport.Render("PDF"), 
                            "application/pdf", "agenda.pdf");

                    case 2: //Word
                        return File(relatorio.LocalReport.Render("WORD"), 
                            "application/msword", "agenda.doc");

                    case 3: //Excel
                        return File(relatorio.LocalReport.Render("EXCEL"), 
                            "application/excel", "agenda.xls");

                    default:
                        throw new Exception("Erro. Opção inválida.");
                }
            }
            catch(Exception e)
            {
                //exibir mensagem de erro..
                ViewBag.Mensagem = e.Message;
            }

            return View("Consulta"); //nome da página..
        }


	}
}