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



        [HttpGet("reservasCliente")]
        public IActionResult ReservasCliente(int identCliente)
        {
            var sessaoUsuario = HttpContext.Session.GetString("IdLogado");
            if (sessaoUsuario == null)
            {
                return Unauthorized("Faça login Antes");
            }

            var idCliente = Request.Cookies["IdLogado"];
            if (idCliente != null)
            {

                var resultado = from c in _context.Clientes
                                join r in _context.Reservas
                                on c.Id equals r.IdCliente
                                where c.Id == int.Parse(idCliente)
                                select new
                            {
                                Cliente = c.Nome, c.Email,
                                Reservas = r.Id, r.DataChegada, r.Noites, r.Hospedes, r.Mensagem
                            };
                return Ok(resultado.ToList());
            }
            return Unauthorized("Faça login Antes");
        }

        [HttpPost]
        public IActionResult CadastraReserva(Reserva reserva)
        {

            var sessaoUsuario = HttpContext.Session.GetString("IdLogado");
            if (sessaoUsuario == null)
            {
                return Unauthorized("Faça login Antes");
            }
            var idLogado = Request.Cookies["IdLogado"];
            if (idLogado != null)
                reserva.IdCliente = int.Parse(idLogado);

            _context.Add(reserva);
            _context.SaveChanges();
            return Created("", reserva);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaReserva(int id, Reserva reserva)
        {
            var sessaoUsuario = HttpContext.Session.GetString("IdLogado");
            if (sessaoUsuario == null)
            {
                return Unauthorized("Faça login Antes");
            }

            var reservaDoBanco = _context.Reservas.Find(id);
            if (reservaDoBanco == null)
            {
                return NotFound("Reserva não existe no banco!");
            }
            reservaDoBanco.DataChegada = reserva.DataChegada;
            reservaDoBanco.Noites = reserva.Noites;
            reservaDoBanco.Hospedes = reserva.Hospedes;
            reservaDoBanco.Mensagem = reserva.Mensagem;
          
            _context.SaveChanges();
            return Ok("Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaReserva(int id)
        {
            var sessaoUsuario = HttpContext.Session.GetString("IdLogado");
            if (sessaoUsuario == null)
            {
                return Unauthorized("Faça login Antes");
            }

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

