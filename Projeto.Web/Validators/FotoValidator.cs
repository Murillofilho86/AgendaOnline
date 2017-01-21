using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Projeto.Web.Validators
{
    public class FotoValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            HttpPostedFileBase foto = (HttpPostedFileBase) value;

            //Regra: imagens de extensão JPG e até 1MB de tamanho

            return foto.ContentType.Equals("image/jpeg")
                && foto.ContentLength <= 1 * Math.Pow(1024, 2);
        }
    }
}