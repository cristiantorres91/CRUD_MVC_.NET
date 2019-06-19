using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudPersonaMVC.Models.ViewModels
{
    public class PersonaViewModel
    {
        
        public int Id { get; set; }
        //indica que es requerido
        [Required]
        //nombre a mostrar 
        [Display (Name ="Nombre")]
        //valida el numero de caracteres igual a varchar(50) de nuestra bd
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido")]
        [StringLength(50)]
        public string Apellido { get; set; }
        [Required]
        public int? Edad { get; set; }
    }
}