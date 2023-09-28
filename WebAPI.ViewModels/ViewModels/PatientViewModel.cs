using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Models;

namespace WebAPI.ViewModels.ViewModels
{
    public class PatientViewModel
    {
        public long Id { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public DateTime Dob { get; set; }

        public string Contact { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; }

        public PatientViewModel()
        {

        }
        public PatientViewModel(Patient obj)
        {
            Id = obj.Id;
            Firstname = obj.FirstName;
            Lastname = obj.LastName;
            Dob = Convert.ToDateTime(obj.Dob);
            Contact = obj.Contact;
            Address = obj.Address;
            City = obj.City;
            Country = obj.Country;
            Email = obj.Email;
            IsActive = Convert.ToBoolean(obj.IsActive);
        }
    }
}
