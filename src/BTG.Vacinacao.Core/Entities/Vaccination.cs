using BTG.Vacinacao.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Core.Entities
{
    public class Vaccination : BaseEntity
    {
        public Guid PersonId { get; private set; }
        public Guid VaccineId { get; private set; }
        public DoseType DoseType { get; private set; }
        public DateTime ApplicationDate { get; private set; }

        public Vaccination(Guid personId, Guid vaccineId, DoseType doseType, DateTime applicationDate)
        {
            Id = Guid.NewGuid();
            PersonId = personId;
            VaccineId = vaccineId;
            DoseType = doseType;
            ApplicationDate = applicationDate;
            CreatedAt = DateTime.UtcNow;
        }

        // EF Area
        public Person Person { get; set; }
        public Vaccine Vaccine { get; set; }

        protected Vaccination() { }
    }
}
