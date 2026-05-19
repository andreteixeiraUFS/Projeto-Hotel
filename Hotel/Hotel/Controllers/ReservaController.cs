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
        public IActionResult ReservasCliente()
        {
            var usuarioLogado = HttpContext.Session.GetString("IdLogado");
            if (usuarioLogado == null)
            {
                return Unauthorized("Faça login antes!");
            }
            var idUsuarioLogado = Request.Cookies["IdLogado"];
            
            if (idUsuarioLogado != null)
            {
                Console.WriteLine("TESTE: " + int.Parse(idUsuarioLogado));
                var resultado = from c in _context.Clientes
                                join r in _context.Reservas
                                on c.Id equals r.IdCliente
                                where c.Id == int.Parse(idUsuarioLogado)
                                select new
                                {
                                    Cliente = c.Nome,
                                    c.Email,
                                    Reservas = r.Id, r.DataChegada,
                                    r.Noites,
                                    r.Hospedes, r.Mensagem
                                };
                return Ok(resultado.ToList());
            }
            return Unauthorized("Faça login antes!");
        }


        [HttpPost]
        public IActionResult CadastraReserva(Reserva reserva)
        {
            var usuarioLogado = HttpContext.Session.GetString("IdLogado");
            if (usuarioLogado == null)
            {
                return Unauthorized("Faça login antes!");
            }
            var idUsuarioLogado = Request.Cookies["IdLogado"];
            if (idUsuarioLogado != null)
                reserva.IdCliente = int.Parse(idUsuarioLogado);

            _context.Add(reserva);
            _context.SaveChanges();
            return Created("", reserva);
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaReserva(int id, Reserva reserva)
        {
            var usuarioLogado = HttpContext.Session.GetString("IdLogado");
            if (usuarioLogado == null)
            {
                return Unauthorized("Faça login antes!");
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
           // reservaDoBanco.IdCliente = reserva.IdCliente;
            _context.SaveChanges();
            return Ok("Atualizado");
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaReserva(int id)
        {
            var usuarioLogado = HttpContext.Session.GetString("IdLogado");
            if (usuarioLogado == null)
            {
                return Unauthorized("Faça login antes!");
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

