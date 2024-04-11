using System;
using System.ComponentModel.DataAnnotations; // For data annotations
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity; // For database schema-related attributes

namespace BuildsByBrickwellNew.Models
{
    public partial class Customer
    {
        [Key] // Marks CustomerId as the primary key
        public int CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        // Assuming BirthDate should be a DateTime to store actual dates
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        public string CountryOfResidence { get; set; }

        public string Gender { get; set; }

        // Age can be computed from BirthDate, so you might not need to store it
        // But if you do, consider using int instead of double
        public int? Age { get; set; }

        // Foreign key property to link to AspNetUsers
        [ForeignKey("IdentityUser")]
        public string AspNetUserId { get; set; }

        // Navigation property to the IdentityUser
        public virtual IdentityUser? IdentityUser { get; set; }
    }
}
