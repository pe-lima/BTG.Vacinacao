using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; private set; }
        public string Cpf { get; private set; }

        public Person(string name, string cpf)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedAt = DateTime.UtcNow;
            Cpf = cpf;
        }
    }
}
