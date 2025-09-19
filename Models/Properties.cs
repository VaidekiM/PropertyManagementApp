using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyManagementApp.Models {
    public class Properties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
    }
}