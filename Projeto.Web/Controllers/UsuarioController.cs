using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projeto.Web.Models; //camada de modelo
using Projeto.DAL.Entities; //entidades
using Projeto.DAL.Persistence; //acesso a banco
using Projeto.Util.Seguranca; //criptografia
using System.Web.Security; //autenticação
using Newtonsoft.Json; //JSON.NET

namespace Projeto.Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: /Usuario/Login
        public ActionResult Login()
        {
            return View(); //abre a view..
        }

        // GET: /Usuario/Cadastro
        public ActionResult Cadastro()
        {
            return View(); //abre a view..
        }

        [HttpPost] //recebe requisições do tipo POST
        [ValidateAntiForgeryToken] //exibir que a página envie um Token de segurança...
        public ActionResult CadastrarUsuario(UsuarioViewModelCadastro model)
        {
            //verificar se a classe de modelo passou nas validações..
            if(ModelState.IsValid)
            {
                try
                {
                    Usuario u = new Usuario(); //classe de entidade..
                    u.Nome  = model.Nome;
                    u.Email = model.Email;
                    u.Login = model.Login;
                    u.Senha = Criptografia.EncriptarMD5(model.Senha);
                    u.Foto  = Guid.NewGuid().ToString() + ".jpg";

                    UsuarioDal d = new UsuarioDal(); //persistencia..
                    d.Insert(u); //gravando..

                    //fazendo o upload do arquivo..
                    string pasta = HttpContext.Server.MapPath("/Images/");
                    model.Foto.SaveAs(pasta + u.Foto); //salvando..

                    ViewBag.Mensagem = "Usuario " + u.Nome + ", cadastrado com sucesso.";
                    ModelState.Clear(); //limpar o formulário..
                }
                catch(Exception e)
                {
                    //imprimir mensagem de erro..
                    ViewBag.Mensagem = e.Message;
                }
            }

            //nome da página..
            return View("Cadastro");
        }


        //Método para autenticar o usuario..
        [HttpPost] //recebe requisição do tipo POST
        [ValidateAntiForgeryToken] //exibir um token de verificação
        public ActionResult AutenticarUsuario(UsuarioViewModelLogin model)
        {
            //testar se a model passou nas validações..
            if(ModelState.IsValid)
            {
                try
                {                    
                    UsuarioDal d = new UsuarioDal(); //acesso a banco..
                    Usuario u = d.Find(model.Login, Criptografia.EncriptarMD5(model.Senha));

                    //verificar se o usuario foi encontrado..
                    if(u != null) //usuario encontrado?
                    {
                        //instanciar a model..
                        UsuarioAutenticado auth = new UsuarioAutenticado();
                        auth.IdUsuario = u.IdUsuario;
                        auth.Nome  = u.Nome;
                        auth.Email = u.Email;
                        auth.Login = u.Login;
                        auth.Foto  = u.Foto;
                        auth.DataHoraAcesso = DateTime.Now;

                        //criando o ticket de acesso..
                        var ticket = new FormsAuthenticationTicket(JsonConvert.SerializeObject(auth), model.ManterConectado, 10);
                        //gravar o ticket em cookie..
                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                                           FormsAuthentication.Encrypt(ticket));
                        //cookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(cookie); //gravando..

                        //redirecionamento...
                        return RedirectToAction("Index", "Tarefa", new { area = "Agenda" });
                    }
                    else
                    {
                        throw new Exception("Acesso Negado. Tente novamente.");
                    }
                }
                catch(Exception e)
                {
                    //exibir mensagem de erro..
                    ViewBag.Mensagem = e.Message;
                }
            }

            return View("Login"); //retornar para a página de Login..
        }


        //Método para logout do sistema..
        [Authorize]
        public ActionResult Logout()
        {
            //destruir o ticket de acesso do usuario..
            FormsAuthentication.SignOut();
            //redirecionar para a página de login..
            return RedirectToAction("Login", "Usuario"); //nome da view..
        }


	}
}