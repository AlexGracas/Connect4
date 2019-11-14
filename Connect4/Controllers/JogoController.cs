using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Connect4.Controllers
{
    public class JogoController : Controller
    {
        private ApplicationDbContext _context { get; set; }

        private UserManager<ApplicationUser> _userManager { get; set; }
        private SignInManager<ApplicationUser> _signInManager { get; set; }

        public JogoController(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager )
        {
            this._context = dbContext;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Lobby(int id)
        {
            var jogo = _context.Jogos.Include(j => j.Jogador1).Include(j => j.Jogador2).Where(j => j.Id == id).Select(j => j).FirstOrDefault();
            if(jogo == null)
            {
                return NotFound();
            }
            return View(jogo);
        }

        public IActionResult CriarJogo()
        {
            Jogo jogo;
            JogadorPessoa jogadorAtual = _userManager.GetUserAsync(User).Result.jogador;

            jogo = (from t in _context.Jogos.Include(b => b.Jogador1).Include(b => b.Jogador2)
                    where t.Jogador1 == null || t.Jogador2 == null
                    select t).FirstOrDefault();

            if (jogo == null)
            {
                jogo = new Jogo();
                jogo.Jogador1 = jogadorAtual;
                _context.Add(jogo);
            }
            else
            {
                if (jogo.Jogador1 == null)
                {
                    jogo.Jogador1 = jogadorAtual;
                }
                else
                {
                    jogo.Jogador2 = jogadorAtual;
                }
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Lobby), new { id = jogo.Id});
        }
    }
}