using System.ComponentModel.DataAnnotations;

namespace ajustevonline.Models
{
    public class SyncParams
    {
        [Required]
        public string IdOnline { get; set; }
        [Required]
        public int IdSiva { get; set; }

    }
}