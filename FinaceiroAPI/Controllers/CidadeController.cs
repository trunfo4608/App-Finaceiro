using FinaceiroAPI.Models;
using FinaceiroAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinaceiroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CidadeController : Controller
    {
        private readonly IRepository _repo;

        public CidadeController(IRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetCidade()
        {
            try
            {
                var result = await _repo.GetAllCidades();
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }

        [HttpGet("ById/{id}")]
        public IActionResult GetCidadeById([FromRoute] int id)
        {
            try
            {
                var cidade = _repo.GetCidadeById(id);
                if(cidade == null)
                {
                    return BadRequest("Cidade nao encontrada");
                }

                return Ok(cidade);

            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "gerente,empregado")]
        public IActionResult Post([FromBody] Cidade cidade)
        {
            try
            {
                _repo.Insert(cidade);
                if (_repo.SaveChanges())
                {
                    return Created($"/api/Estado/{cidade.Id}", cidade);
                }
                else
                {
                    return BadRequest("Cidade nao incluida.");
                }

            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
     
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "gerente,empregado")]
        public IActionResult Put(int id, [FromBody] Cidade cidade)
        {
            try
            {
                var result = _repo.GetCidadeById(id);
                if(result == null)
                {
                    return BadRequest("Cidade nao encontrada");
                }

                _repo.Update(cidade);

                if (_repo.SaveChanges())
                {
                    return Created($"/api/Cidade/{cidade.Id}", cidade);
                }
                else
                {
                    return BadRequest("Cidade nao modificada");
                }

            }
            catch (Exception)
            {

                return BadRequest("Cidade nao modificada");
            }
        }




        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete([FromRoute] int id)
        {
            var cidade = _repo.GetCidadeById(id);

            if (cidade == null)
            {
                return BadRequest("Cidade nao encontrada");
            }

            _repo.Delete(cidade);

            if (_repo.SaveChanges())
            {
                return Ok("Cidade deletada.");
            }
            else
            {
                return BadRequest("Cidade nao deletada.");
            }
        }



        [HttpGet("Pesquisa")]
        public IActionResult GetCidadePesquisa([FromQuery] string valor)
        {
            try
            {
                var cidade = _repo.GetCidadePesquisa(valor);
                if (cidade == null)
                {
                    return BadRequest("Cidade nao encontrada");
                }

                return Ok(cidade);

            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }


        [HttpGet("Paginacao")]
        public IActionResult GetCidadePaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                var result = _repo.GetCidadePaginacao(valor, skip, take, ordemDesc);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }

    }
}
