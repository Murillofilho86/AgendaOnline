using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Projeto.DAL.Persistence;

namespace Projeto.Web.Validators
{
    public class EmailValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string email = (string) value;

            UsuarioDal d = new UsuarioDal();
            return ! d.HasEmail(email);
        }
    }
}