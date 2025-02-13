using FinaceiroAPI.Models;
using FinaceiroAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FinaceiroAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : Controller
    {

        private readonly IRepository _repo;

        public ContaController(IRepository repo)
        {
                _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetConta()
        {
            try
            {
                var result = await _repo.GetAllContas();
                return Ok(result);

            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }

        }

        [HttpGet("ContaById/{id}")]
        public IActionResult GetContaById([FromRoute] int id)
        {
            try
            {
                var conta = _repo.GetContaById(id);
                if (conta == null)
                {
                    return BadRequest("Conta nao encontrada");
                }

                return Ok(conta);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Conta conta)
        {
            try
            {
                _repo.Insert(conta);

                if (_repo.SaveChanges())
                {
                    return Created($"/api/Conta/{conta.Id}", conta);
                }
                else
                {
                    return BadRequest("Conta nao incluida.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }
        }


        [HttpPut("id")]
        public IActionResult Put(int id, [FromBody] Conta conta)
        {
            try
            {
                var result = _repo.GetContaById(id);

                if (result == null)
                {
                    return BadRequest("Conta nao encontrada.");
                }

                _repo.Update(conta);

                if (_repo.SaveChanges())
                {
                    return Created($"/api/Conta/{conta.Id}", conta);
                }
                else
                {
                    return BadRequest("Conta nao encontrada.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            try
            {
                var conta = _repo.GetContaById(id);

                if (conta == null)
                {
                    return BadRequest("Conta nao encontrada.");
                }

                _repo.Delete(conta);

                if (_repo.SaveChanges())
                {
                    return Ok("conta deletada.");
                }
                else
                {
                    return BadRequest("Conta nao deletada.");
                }
            }
            catch (Exception e)
            {

                return BadRequest($"Erro: {e.Message}");
            }
        }


        [HttpGet("Paginacao")]
        public IActionResult GetContaPaginacao([FromQuery] string valor, int skip, int take, bool ordemDesc)
        {
            try
            {
                var result = _repo.GetContaPaginacao(valor, skip, take, ordemDesc);
                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest($"Erro {e.Message}");
            }
        }

    }
}
