using FinaceiroAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using FinaceiroAPI.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FinaceiroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstadoController : Controller
    {

        private readonly IRepository _repo;

        public EstadoController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstado()
        {
            try
            {
                var result = await _repo.GetAllEstados();
                return Ok(result);

            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }

        }

        [HttpGet("BySigla/{sigla}")]
        public IActionResult GetEstadoBySigla([FromRoute] string sigla )
        {
            try
            {
                var estado = _repo.GetEstadoBySigla(sigla);
                if(estado == null)
                {
                    return BadRequest("Estado nao encontrado");
                }

                return Ok(estado);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "gerente,empregado")]
        public IActionResult Post([FromBody] Estado estado)
        {
            try
            {
                _repo.Insert(estado);

                if (_repo.SaveChanges())
                {
                    return Created($"/api/Estado/{estado.Sigla}", estado);
                }
                else
                {
                    return BadRequest("Estado nao incluido.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }
        }


        [HttpPut("sigla")]
        [Authorize(Roles = "gerente,empregado")]
        public IActionResult Put(string sigla, [FromBody] Estado estado)
        {
            try
            {
                var result = _repo.GetEstadoBySigla(sigla);

                if (result == null)
                {
                    return BadRequest("Estado nao encontrado.");
                }

                _repo.Update(estado);

                if (_repo.SaveChanges())
                {
                    return Created($"/api/Estado/{estado.Sigla}", estado);
                }
                else
                {
                    return BadRequest("Estado nao modificado.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }
        }


        [HttpDelete("{sigla}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete([FromRoute]string sigla)
        {
            try
            {
                var estado = _repo.GetEstadoBySigla(sigla);

                if(estado == null)
                {
                    return BadRequest("Estado nao encontrado.");
                }

                _repo.Delete(estado);

                if (_repo.SaveChanges())
                {
                    return Ok("Estado deletado.");
                }
                else
                {
                    return BadRequest("Estado nao deletado.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }
        }


     [HttpGet("Paginacao")]
     public IActionResult GetEstadoPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
     {
            try
            {
                var result = _repo.GetEstadoPaginacao(valor, skip, take, ordemDesc);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}") ;
            }
     }




    }
}
