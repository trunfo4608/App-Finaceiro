using FinaceiroAPI.Models;
using FinaceiroAPI.Repository;
using FinaceiroAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace FinaceiroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly IRepository _repo;
        private readonly TokenService _service;

        public UsuarioController(IRepository repo, TokenService service)
        {
            _repo = repo;
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            try
            {
                var result = await _repo.GetAllUsuario();
                return Ok(result);

            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }


        [HttpGet("ContaByLogin/{login}")]
        public IActionResult GetContaById([FromRoute] string login)
        {
            try
            {
                var usuario = _repo.GetUsuarioByLogin(login);
                if (usuario == null)
                {
                    return BadRequest("Usuario nao encontrado");
                }

                return Ok(usuario);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }


        [HttpPost]
        [Authorize(Roles ="gerente,empregado")]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                var result = _repo.GetUsuarioByLogin(usuario.Login);

                if(result == null)
                {

                    var senhaHash = MD5Hash.GeraHash(usuario.Senha);
                    usuario.Senha = senhaHash;

                    _repo.Insert(usuario);

                    if (_repo.SaveChanges())
                    {
                        return Created($"/api/Usuario/{usuario.Id}", usuario);
                    }
                    else
                    {
                        return BadRequest("Usuario nao incluido.");
                    }

                }
                else
                {
                    return BadRequest("Usuario já existe.");
                }

               
            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }
        }


        [HttpPut("login")]
        [Authorize(Roles = "gerente,empregado")]
        public IActionResult Put(string login, [FromBody] Usuario usuario)
        {
            try
            {
                var result = _repo.GetUsuarioByLogin(login);

                if (result == null)
                {
                    return BadRequest("Usuario nao encontrado.");
                }

                var senhaHash = MD5Hash.GeraHash(usuario.Senha);

                _repo.Update(usuario);

                if (_repo.SaveChanges())
                {
                    return Created($"/api/Usuario/{usuario.Id}", usuario);
                }
                else
                {
                    return BadRequest("Usuario nao encontrado.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }
        }


        [HttpDelete("{login}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete([FromRoute] string login)
        {
            try
            {
                var usuario = _repo.GetUsuarioByLogin(login);

                if (usuario == null)
                {
                    return BadRequest("Usuario nao encontrado.");
                }

                _repo.Delete(usuario);

                if (_repo.SaveChanges())
                {
                    return Ok("Usuario deletado.");
                }
                else
                {
                    return BadRequest("Usuario nao deletado.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }
        }


        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UsuarioLogin usuarioLogin)
        {
            var usuario = _repo.GetUsuarioLogin(usuarioLogin);
            if(usuario == null)
            {
                return BadRequest("Usuario invalido.");
            }

            string senhaHash = MD5Hash.GeraHash(usuarioLogin.Senha);

            if(usuario.Senha == senhaHash)
            {
                var token = _service.GeraToken(usuario);
                
                usuario.Senha = string.Empty;

                var result = new UsuarioResponse()
                {
                    Usuario = usuario,
                    Token = token
                };

                return Ok(result);

            }
            else
            {
                return BadRequest("Senha invalida.");
            }
        }

    }
}
