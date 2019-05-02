using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;
using UserApi.Data;
using UserApi.Data.Entities;
using UserApi.Models;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize("Bearer")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public UserController(IUserRepository repository, IMapper mapper, LinkGenerator _linkGenerator)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._linkGenerator = _linkGenerator;
        }
        /// <summary>
        /// Recupera os Usuários da base.
        /// </summary>
        /// <returns> Retorna todos os usuários cadastrados</returns>
        [HttpGet]
        public async Task<ActionResult<UserModel[]>> Get()
        {
            try
            {
                var results = await _repository.GetAllUsersAsync();
                return _mapper.Map<UserModel[]>(results);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database error: " + ex.Message);
            }
        }

        /// <summary>
        /// Recupera um Usuário especifico por CPF.
        /// </summary>
        /// <param>String - CPF</param>
        /// <returns> Retorna um usuário cadastrado</returns>
        [HttpGet("{CPF}")]
        public async Task<ActionResult<UserModel>> Get(String CPF)
        {
            try
            {
                var result = await _repository.GetUserAsync(CPF);
                if (result == null) return NotFound();

                return _mapper.Map<UserModel>(result);

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database error: " + ex.Message);
            }

        }
        /// <summary>
        ///  Cria novo usuário.
        /// </summary>
        /// <param>UserModel</param>
        /// <returns> Usuário cadastrado - Created</returns>
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel model)
        {
            try
            {
                if (await _repository.GetUserAsync(model.CPF) != null)
                {
                    return BadRequest("CPF in use.");
                }

                var user = _mapper.Map<User>(model);
                _repository.Add(user);

                if (await _repository.SaveChangesAsync())
                {
                    var location = _linkGenerator.GetPathByAction("GET", "User", values: new { CPF = model.CPF });
                    return Created(location, _mapper.Map<UserModel>(user));
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database error: " + ex.Message);
            }
        }

        /// <summary>
        ///  Altera usuário.
        /// </summary>
        /// <param>UserModel e CPF</param>
        /// <returns> Usuário cadastrado - Atualizado</returns>
        [HttpPut("{CPF}")]
        public async Task<ActionResult<UserModel>> Put(string CPF, UserModel model)
        {
            try
            {
                var oldUser = await _repository.GetUserAsync(CPF);
                if (oldUser == null) return NotFound($"Erro, CPF not found");

                _mapper.Map(model, oldUser);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<UserModel>(oldUser);
                }
                return BadRequest();

            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database error " + ex.Message);
            }
        }
        /// <summary>
        ///  Deleta usuário cadastrado.
        /// </summary>
        /// <param>CPF</param>
        /// <returns> OK - 202</returns>
        [HttpDelete("{CPF}")]
        public async Task<IActionResult> Delete(string CPF)
        {
            try
            {
                var oldUser = await _repository.GetUserAsync(CPF);
                if (oldUser == null) return NotFound();

                _repository.Delete(oldUser);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database error: " + ex.Message);
            }
        }
    }
}