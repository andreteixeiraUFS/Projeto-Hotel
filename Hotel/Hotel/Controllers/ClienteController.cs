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



        [HttpGet("{id}")]
        public IActionResult RetornaCliente(int id)
        {
            var clientes = _context.Clientes.Find(id);
            if (clientes == null)
            {
                return NotFound("Cliente não encontrada");
            }
            return Ok(clientes);
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
