﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Torneio
    {
        public int Id { get; set; }

        public String NomeTorneio { get; set; }

        public int QuantidadeJogadores { get; set; }

        public DateTime Inicio { get; set; }
    }
}