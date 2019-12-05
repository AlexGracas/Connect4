using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Connect4.Data;
using Connect4.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Connect4.Controllers
{
    [Produces("application/json")]
    [Route("api/Jogo")]
    public class JogoAPIController : Controller
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private SignInManager<ApplicationUser> _signInManager { get; set; }
        private ILogger<ManageController> _logger { get; set; }

        private ApplicationDbContext _context { get; set; }
        public JogoAPIController(
          UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager,
          ILogger<ManageController> logger,
          ApplicationDbContext context
          )
        {
            _userManager = userManager;
            _signInManager = signInManager;           
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Obter")]
        [Route("Obter")]
        [Authorize]
        public Tabuleiro ObterJogo()
        {
            Tabuleiro t = null;
            try
            {
                t = new Tabuleiro();
                _context.Tabuleiros.Add(t);
                _context.SaveChanges();
            }catch(Exception e)
            {
                _logger.LogCritical(e, e.Message, null);
            }
            return t;
        }


        [HttpGet(Name = "Obter")]
        [Route("Obter/{id}")]
        [Authorize]
        public Tabuleiro ObterJogo(int id)
        {
            var jogo = _context.Jogos
                .Include(j => j.Tabuleiro)
                .Include(j => j.Jogador1)
                .Include(j => j.Jogador2)
                .Where(j => j.Id == id)
                .FirstOrDefault();

            if (jogo == null)
            {
                throw new ApplicationException("Não Existe o Jogo");
            }
            //Caso inicie pelo turno do Computador.
            VerificarJogarComputador(jogo);
            _context.SaveChanges();
            //TODO: Verificar Permissão antes.
            if (jogo.Tabuleiro != null)
            {
                return jogo.Tabuleiro;
            }            
            jogo.Tabuleiro = new Tabuleiro();

            return jogo.Tabuleiro;
        }

        [HttpPost(Name = "Vencedor")]
        [Route("Vencedor")]
        public int Vencedor(Tabuleiro t)
        {
            return t.Vencedor();
        }

        [HttpPost(Name = "Jogar")]
        [Route("Jogar")]
        //(...)/Jogar?JogoId=1&Pos=4
        public IActionResult Jogar([FromQuery] int JogoId, 
            [FromQuery]int Pos)
        {
            var jogo = _context.Jogos
                .Include(j => j.Tabuleiro)
                .Include(j=> j.Jogador1)
                .Include(j => j.Jogador2)
                .Where(j => j.Id == JogoId)
                .FirstOrDefault();
            if(jogo == null)
            {
                return NotFound();
            }
            if(jogo.Tabuleiro == null)
            {
                return BadRequest();
            }
            //TODO: Pegar o usuário autenticado. 
            //Verificar se ele é o jogador 1 ou 2.
            //Verificar se ele pode fazer a jogada.
            //Por último executar a jogada ou exceção.
            jogo.Tabuleiro.Jogar(jogo.Tabuleiro.Turno, Pos);

            VerificarJogarComputador(jogo);

            _context.SaveChanges();

            return Ok(jogo.Tabuleiro);
        }


        private void VerificarJogarComputador(Jogo jogo)
        {
            var outroJogador =
                jogo.Tabuleiro.Turno == 1 ?
                jogo.Jogador1 :
                jogo.Jogador2;
            if (outroJogador is JogadorComputador)
            {
                var jogada = this.JogarComputador(
                        jogo.Tabuleiro,
                        (JogadorComputador)outroJogador);
                jogo.Tabuleiro.Jogar(
                jogo.Tabuleiro.Turno,
                jogada.Result);             
            }
        }
        private async Task<int> JogarComputador(Tabuleiro t, JogadorComputador jc)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = 
                    new StringContent(JsonConvert.SerializeObject(t), 
                    Encoding.UTF8, 
                    "application/json");

                using (var response = await httpClient.PostAsync(jc.URLServico, 
                    content))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = 
                            await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<int>(apiResponse);
                    }
                    else
                    {
                        throw new ApplicationException("Erro ao conectar ao serviço de Inteligência Artificial. Código:" + response.StatusCode.ToString());
                    }                                        
                }
            }
        }
    }
}