using System;
using System.ComponentModel.DataAnnotations;

namespace UserApi.Models
{
    public class UserModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Login { get; set; }
        public DateTime DataNascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }
        [Required]
        public string senha { get; set; }//Como se trata de uma aplicação de exemplo não achei necessário criptografar a senha.
        public string acessKey { get; set; }
    }
}
