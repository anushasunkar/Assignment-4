//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FuelQuoteApplication14.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;


    public partial class Client_Info
    {
        public int Id { get; set; }

        [DisplayName("User Name")]
        [RegularExpression(@"^[A-Za-z][A-Za-z0-9._]+$", ErrorMessage = "Please enter valid User Name")]
        public string Username { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Full Name is required.")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter valid Full Name")]
        public string FullName { get; set; }

        [DisplayName("Address 1")]
        [Required(ErrorMessage = "Address is required")]
        [MaxLength(100, ErrorMessage = "Address should be less than 100 characters")]
        public string Address1 { get; set; }

        [DisplayName("Address 2")]
        [MaxLength(100, ErrorMessage = "Address should be less than 100 characters")]
        public string Address2 { get; set; }

        [DisplayName("City")]
        [Required(ErrorMessage = "city is required")]
        public string City { get; set; }

        [DisplayName("State")]
        [Required(ErrorMessage = "state is required")]
        public string State { get; set; }

        [DisplayName("Zipcode")]
        [Required(ErrorMessage = "zipcode is required")]
        [Range(00000, 999999, ErrorMessage = "zipcode should be 5-9 characters")]
        public float ZipCode { get; set; }
    }
}
