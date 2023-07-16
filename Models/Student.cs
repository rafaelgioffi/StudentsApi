using System.ComponentModel.DataAnnotations;

namespace StudentsApi.Models
{
    public class Student
    {
        [Key]
        //[Range(100101, 199999)]        
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set;}

        [Required]
        public int Age { get; set; }
    }
}
