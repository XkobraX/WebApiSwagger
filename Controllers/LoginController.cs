using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using UserApi.Data.Entities;
using UserApi.Data.Migrations;
using UserApi.Models;
using UserApi.Security;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMapper _mapper;

        public LoginController(IMapper mapper)
        {
            this._mapper = mapper;
        }
        /// <summary>
        /// Loga no sistema e valida o Token JWT
        /// ex de utilização : "{ "login": "PedroADM","acessKey": "94be650011cf412ca906fc335f615cdc" }"
        /// </summary>
        /// <param>Usuario</param>
        /// <returns> Autentica por JWT</returns>
        [AllowAnonymous]
        [HttpPost]
        //Exemplo de Login só para se fazer da utilização do JWT
        public async Task<IActionResult> Post(
            [FromBody]  User usuario,
            [FromServices] UserRepository usersDAO,
            [FromServices] SigningConfigurations signingConfigurations,
            [FromServices] TokenConfigurations tokenConfigurations)
        {
            bool credenciaisValidas = false;
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Login))
            {
                var usuarioBase = _mapper.Map<UserModel>(usersDAO.GetUserbyLogin(usuario.Login));
                credenciaisValidas = (usuarioBase != null &&
                    usuario.Login == usuarioBase.Login &&
                    usuario.acessKey == usuarioBase.acessKey);
            }

            if (credenciaisValidas)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.Login, "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Login)
                    }
                );

                DateTime dataCriacao = DateTime.Now;
                DateTime dataExpiracao = dataCriacao +
                    TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                var handler = new JwtSecurityTokenHandler();
                var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                {
                    Issuer = tokenConfigurations.Issuer,
                    Audience = tokenConfigurations.Audience,
                    SigningCredentials = signingConfigurations.SigningCredentials,
                    Subject = identity,
                    NotBefore = dataCriacao,
                    Expires = dataExpiracao
                });
                var token = handler.WriteToken(securityToken);

                return Ok();
            }
            else
            {
                return BadRequest("Não foi possivel fazer a validação do usuário");
            }
        }
    }
}