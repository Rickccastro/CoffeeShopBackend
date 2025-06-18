using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public required string Cpf { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
