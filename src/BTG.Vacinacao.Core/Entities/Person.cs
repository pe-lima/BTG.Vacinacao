using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }

        public Person(string name, string cpf)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedAt = DateTime.UtcNow;
            Cpf = cpf;
        }
    }
}
