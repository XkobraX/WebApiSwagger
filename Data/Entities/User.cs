using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApi.Data.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        public string senha { get; set; }//Como se trata de uma aplicação de exemplo não achei necessário criptografar a senha.
        public string acessKey { get; set; }
       
    }
}
