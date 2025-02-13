using FinaceiroAPI.Models;
using FinaceiroAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinaceiroAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PessoaController : Controller
    {

        private readonly IRepository _repo;

        public PessoaController(IRepository repo)
        {
            _repo = repo;
        }


        [HttpGet]
        public async Task<IActionResult> GetPessoa()
        {
            try
            {
                var result = await _repo.GetAllPessoas();
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }



        [HttpGet("ById/{id}")]
        public IActionResult GetPessoaById([FromRoute] int id)
        {
            try
            {
                var pessoa = _repo.GetPessoaById(id);
                if(pessoa == null)
                {
                    return BadRequest("Pessoa nao encontrada");
                }

                return Ok(pessoa);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }


        [HttpPost]
        [Authorize(Roles = "gerente,empregado")]
        public IActionResult Post([FromBody] Pessoa pessoa)
        {
            try
            {
                _repo.Insert(pessoa);
                if (_repo.SaveChanges())
                {
                    return Created($"/api/Pessoa/{pessoa.Id}", pessoa);
                }
                else
                {
                    return BadRequest("Pessoa nao incluida");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "gerente,empregado")]
        public IActionResult Put(int id, [FromBody] Pessoa pessoa)
        {
            try
            {
                var result = _repo.GetPessoaById(id);
                if(result == null)
                {
                    return BadRequest("Pessoa nao encontrada");
                }

                _repo.Update(pessoa);

                if (_repo.SaveChanges())
                {
                   return Created($"/api/Pessoa/{pessoa.Id}", pessoa);
                }
                else
                {
                    return BadRequest("Pessoa nao modificada.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public IActionResult Delete(int id) {

            var pessoa = _repo.GetPessoaById(id);

            if(pessoa == null)
            {
                return BadRequest("Pessoa nao encontrada.");
            }

            _repo.Delete(pessoa);

            if (_repo.SaveChanges())
            {
                return Ok("Pessoa Deletada");
            }
            else
            {
                return BadRequest("Pessoa nao deletada");
            }
        }


        [HttpGet("Paginacao")]
        public IActionResult GetPessoaPaginacao([FromQuery] string? valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                var result = _repo.GetPessoaPaginacao(valor, skip, take, ordemDesc);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }

    }

}

