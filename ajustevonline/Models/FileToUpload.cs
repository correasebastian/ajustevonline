using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ajustevonline.Models
{
    public class FileToUpload
    {
        [Required]
        public string base64Data { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string idFoto { get; set; }
        [Required]
        public string idInspeccion { get; set; }
        public string placa { get; set; }
    }
}