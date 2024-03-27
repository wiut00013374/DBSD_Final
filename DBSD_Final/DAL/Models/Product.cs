using System.ComponentModel.DataAnnotations;

namespace DBSD_Final.DAL.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double ProductPrice { get; set; }
    }
}
