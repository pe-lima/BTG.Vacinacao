using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Entities
{
    public class Vaccine : BaseEntity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }

        public Vaccine(string name, string code)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreatedAt = DateTime.UtcNow;
            Code = code;
        }
    }
}
