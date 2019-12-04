using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models.JoggoViewModel
{
    public class LobbyViewModel
    {
        public Jogo jogo { get; set; }

        public List<SelectListItem> Items { get; set; }
    }
}
