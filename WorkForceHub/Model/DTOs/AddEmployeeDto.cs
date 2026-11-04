using System.ComponentModel.DataAnnotations;

namespace WorkForceHub.Model.DTos
{
    public class AddEmployeeDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }
    }
}