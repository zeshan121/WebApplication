using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyWebApp.Web.Models.GamingMachine
{
    public class GamingMachineInfoModel
    {

        [Display(Name = "Serial Number")]
        [Required(ErrorMessage = "Please enter gaming serial number.")]
        public long GamingSerialNumber { get; set; }
               
        [Display(Name = "Gaming Machine Position")]
        [Required(ErrorMessage = "Please enter gaming machine position.")]
        [Range(0, 1000)]
        public int? GamingMachinePosition { get; set; }

        [Display(Name = "Game Name")]
        [Required(ErrorMessage = "Please enter game name.")]
        public string GameName { get; set; }
    }
}