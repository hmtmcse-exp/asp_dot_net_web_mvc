using System.ComponentModel.DataAnnotations;

namespace MyConsole
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required (ErrorMessage = "Please Enter Name.")]
        public string Name { get; set; }
    }
}