﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Il nome della pizza è obbligatorio")]
        [MaxLength(50, ErrorMessage = "La lunghezza massima per il nome della pizza è di 50 caratteri spazi compresi")]
        public string Name { get; set; }

        [Column(TypeName = "text")]
        [Required(ErrorMessage ="La descrizione è obbligatoria")]
        public string Description { get; set; }

        [Url(ErrorMessage ="Devi inserire un link valido dell'immagine")]
        [MaxLength(500, ErrorMessage ="La lunghezza del link non deve superare i 500 caratteri")]
        public string Image { get; set; }

        [Required(ErrorMessage ="Devi inserire un numero intero o decimale")]
        public float Prize { get; set; }

        public Pizza() { }
        public Pizza(string name, string description, string image ) 
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
        }
    }
}
