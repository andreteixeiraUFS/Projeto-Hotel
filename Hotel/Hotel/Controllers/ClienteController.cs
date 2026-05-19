using Microsoft.AspNetCore.Mvc;
using Hotel.Data;
using Hotel.Models;
namespace Hotel.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ClienteController : ControllerBase
    {

        private readonly HotelContext _context;

        public ClienteController(HotelContext context)
        {
            _context = context;
        }



        [HttpPost("login")]
        public IActionResult Login(Cliente cliente)
        {
            var clienteBanco = _context.Clientes.Where(c =>
            c.Email.Equals(cliente.Email) && c.Senha.Equals(cliente.Senha)).ToList();
            if (clienteBanco.Count == 0)
                return NotFound("Email ou senha incorretos");

            HttpContext.Session.SetString("IdLogado", clienteBanco[0].Id.ToString());

            Response.Cookies.Append("IdLogado", clienteBanco[0].Id.ToString(),
                 new CookieOptions
                 {
                     HttpOnly = true,
                     Secure = true,
                     SameSite = SameSiteMode.None
                 });

            return Ok("Logado com sucesso");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("IdLogado");
            Response.Cookies.Delete(".AspNetCore.Session");
            return Ok("Logout realizado");
        }

        [HttpPost]
        public IActionResult CadastraCliente(Cliente cliente)
        {
            _context.Add(cliente);
            _context.SaveChanges();
            return Created("", cliente);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaCliente(int id, Cliente cliente)
        {
            var clienteDoBanco = _context.Clientes.Find(id);
            if (clienteDoBanco == null)
            {
                return NotFound("Cliente não existe no banco!");
            }
            clienteDoBanco.Nome = cliente.Nome;
            clienteDoBanco.Email = cliente.Email;
            clienteDoBanco.Sexo = cliente.Sexo;
            _context.SaveChanges();
            return Ok("Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCliente(int id)
        {
            var clienteDoBanco = _context.Clientes.Find(id);
            if (clienteDoBanco == null)
            {
                return NotFound("Não encontrado!");
            }
            _context.Remove(clienteDoBanco);
            _context.SaveChanges();
            return Ok("Deletado");
        }
    }


}
