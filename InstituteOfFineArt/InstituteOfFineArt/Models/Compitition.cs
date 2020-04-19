//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InstituteOfFineArt.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class Compitition
    {
        public int Id { get; set; }

        public string Name { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Type { get; set; }
        [Required(ErrorMessage = "Required")]
        public string despcription { get; set; }
        [Required(ErrorMessage = "Enter the issued date.")]
        [DataType(DataType.Date)]
        [DisplayName("Issue Date")]
        public System.DateTime StartDate { get; set; }
        [Required(ErrorMessage = "Enter the End date.")]
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public System.DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Conditions { get; set; }
        [Required(ErrorMessage = "Required")]
        public string AwardDetils { get; set; }
        [Required(ErrorMessage = "Required")]
        [DisplayName("Insert Image")]
        public string Image { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}
