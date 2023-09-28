using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.Models;

namespace WebAPI.ViewModels.ViewModels
{
    public class ProcedureViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string? DefaultUnit { get; set; }

        public decimal Price { get; set; }

        public decimal? Tax { get; set; }

        public bool IsActive { get; set; }

        public ProcedureViewModel()
        {

        }
        public ProcedureViewModel(ProcedureList p)
        {
            Id = p.Id;
            Name = p.Name;
            Description = p.Description;
            DefaultUnit = p.DefaultUnit;
            Price = Convert.ToDecimal(p.Price);
            Tax = Convert.ToDecimal(p.Tax);
            IsActive = Convert.ToBoolean(p.IsActive = true);
        }
    }
}
