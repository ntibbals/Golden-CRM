using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Golden_CRM.Models.ViewModels
{
    public class UploadCustViewModel
    {
        /// <summary>  
        /// Gets or sets Image file.  
        /// </summary>  
        [Required]
        [Display(Name = "Upload File")]
        public IFormFile FileAttach { get; set; }
    }
}
