﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTG.Vacinacao.Application.DTOs.Vaccine
{
    public class VaccineDto
    {
        public Guid Id { get; set; }    
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
