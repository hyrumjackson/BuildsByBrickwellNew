using System.ComponentModel.DataAnnotations;

namespace BuildsByBrickwellNew.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
    }
}
