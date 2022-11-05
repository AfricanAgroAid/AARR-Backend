using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Farms
{
    public class CreateFarmRequestModel
    {
        [Required]
        public string FarmName { get; set; }

        [Required]
        public string LocatedCity { get; set; }
        
        [Required]
        public int CropTypeId { get; set; }
        
        [Required]
        public string FarmerId { get; set; }
    }
}
