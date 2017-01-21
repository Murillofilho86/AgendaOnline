using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography; //namespace..

namespace Projeto.Util.Seguranca
{
    public class Criptografia
    {
        //método para encriptar um valor em formato MD5
        public static string EncriptarMD5(string valor)
        {
            MD5 md5 = new MD5CryptoServiceProvider(); //criptografia..

            //executar a criptografia e obter o resultado..
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(valor));

            //retornar o hash em formato string..
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }


        //método para encriptar um valor em formato SHA1
        public static string EncriptarSHA1(string valor)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider(); //criptografia..

            //executar a criptografia e obter o resultado..
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(valor));

            //retornar o hash em formato string..
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}
