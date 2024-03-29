﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Connect4.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public String Nome { get; set; }

        [DataType(DataType.Date)]
        public DateTime Nascimento { get; set; }

        [ForeignKey("Jogador")]
        public int? JogadorId { get; set; }
        public JogadorPessoa Jogador { get; set; } 
            = new JogadorPessoa();
    }
}
