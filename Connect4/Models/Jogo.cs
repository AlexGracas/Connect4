using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Jogo
    {
        public int Id { get; set; }
        public Jogador Jogador1 { get; set; }
        public Jogador Jogador2 { get; set; }
        Tabuleiro Tabuleiro { get; set; }
    }
}
