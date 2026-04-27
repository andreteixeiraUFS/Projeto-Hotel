using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel.Models
{
    public class Reserva
    {
       
        public int Id { get; set; }
        public string DataChegada { get; set; }
        public int Noites { get; set; }
        public int Hospedes { get; set; }
        public string Mensagem { get; set; }
     
        public int IdCliente { get; set; }

      

    }
}
