using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Projeto.DAL.Persistence;

namespace Projeto.Web.Validators
{
    //Classe de validação customizada para verificar
    //se o login informado no cadastro ja existe no banco
    public class LoginValidator : ValidationAttribute
    {
        //para que a classe possa executar uma validação, 
        //ela deverá sobrescrever (override) um método da
        //sua superclasse chamado IsValid

        public override bool IsValid(object value)
        {
            //value -> representa o valor que será validado..
            string login = (string) value; //casting

            //procurar se o login existe na base de dados..
            UsuarioDal d = new UsuarioDal(); //persistencia..
            return ! d.HasLogin(login); //se login não existir..
        }
    }
}