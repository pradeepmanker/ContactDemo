using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContactDemo.Models
{
    public class ContactMaster
    {
        public long ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "Email cannot be longer than 150 characters.")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid email address.")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Phone cannot be longer than 20 characters.")]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid phone number.")]
        [Display(Name = "Phone")]
        public string Phone { get; set; }

        [Display(Name = "Status")]
        public bool Status { get; set; }
    }
}