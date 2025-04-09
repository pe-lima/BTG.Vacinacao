using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.DTOs
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
    }
}
