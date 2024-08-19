using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CrudDapper.Services.LivroService;
using CrudDapper.Models;

namespace CrudDapper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroInterface _livroService;

        public LivroController(ILivroInterface livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livro>>> GetAllLivros()
        {
            IEnumerable<Livro> livros = await _livroService.GetAllLivros();

            if (!livros.Any())
            {
                return NotFound("Nenhum livro encontrado");
            }

            return Ok(livros);
        }

        [HttpGet("{livroId}")]
        public async Task<ActionResult<Livro>> GetLivroById(int livroId)
        {
            Livro livro = await _livroService.GetLivroById(livroId);

            if (livro == null)
            {
                return NotFound("Livro não encontrado");
            }

            return Ok(livro);
        }

        [HttpPost]
        public async Task<ActionResult<Livro>> CreateLivro(Livro livro)
        {
            IEnumerable<Livro> livros = await _livroService.CreateLivro(livro);

            return Ok(livros);
        }

        [HttpPut]
        public async Task<ActionResult<Livro>> UpdateLivro(Livro livro)
        {
            IEnumerable<Livro> livros = await _livroService.UpdateLivro(livro);

            return Ok(livros);
        }

        [HttpDelete("{livroId}")]
        public async Task<ActionResult<IEnumerable<Livro>>> DeleteLivro(int livroId)
        {
            Livro resgistro = await _livroService.GetLivroById(livroId);

            if (resgistro == null)
            {
                return NotFound("Livro não encontrado");
            }

            IEnumerable<Livro> livros = await _livroService.DeleteLivro(livroId);

            return Ok(livros);
        }
    }
}
