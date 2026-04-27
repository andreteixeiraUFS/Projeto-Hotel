using Hotel.Data;
using Hotel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ReservaController : ControllerBase
    {

        private readonly HotelContext _context;

        public ReservaController(HotelContext context)
        {
            _context = context;
        }



        [HttpGet("{id}")]
        public IActionResult RetornaReserva(int id)
        {
            var reservas = _context.Reservas.Find(id);
            if (reservas == null)
            {
                return NotFound("Reserva não encontrada");
            }
            return Ok(reservas);
        }

        [HttpGet("reservasCliente/{identCliente}")]
        public IActionResult ReservasCliente(int identCliente)
        {
            var resultado = from c in _context.Clientes
                            join r in _context.Reservas
                            on c.Id equals r.IdCliente 
                            where identCliente == c.Id
                            select new
                            {
                                Cliente = c.Nome, c.Email,
                                Reservas = r.DataChegada, r.Noites, r.Hospedes
                            };
            return Ok(resultado.ToList());
        }


        [HttpPost]
        public IActionResult CadastraReserva(Reserva reserva)
        {
            _context.Add(reserva);
            _context.SaveChanges();
            return Created("", reserva);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaReserva(int id, Reserva reserva)
        {
            var reservaDoBanco = _context.Reservas.Find(id);
            if (reservaDoBanco == null)
            {
                return NotFound("Reserva não existe no banco!");
            }
            reservaDoBanco.DataChegada = reserva.DataChegada;
            reservaDoBanco.Noites = reserva.Noites;
            reservaDoBanco.Hospedes = reserva.Hospedes;
            reservaDoBanco.Mensagem = reserva.Mensagem;
            reservaDoBanco.IdCliente = reserva.IdCliente;
            _context.SaveChanges();
            return Ok("Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaReserva(int id)
        {
            var reservaDoBanco = _context.Reservas.Find(id);
            if (reservaDoBanco == null)
            {
                return NotFound("Não encontrado!");
            }
            _context.Remove(reservaDoBanco);
            _context.SaveChanges();
            return Ok("Deletado");
        }
    }

}

