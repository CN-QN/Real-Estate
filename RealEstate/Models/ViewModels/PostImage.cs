using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstate.Models.ViewModels
{
    public class PostImage
    {
        [Key]
        public int PostImageId { get; set; }

        [Required, MaxLength(400)]
        public string FilePath { get; set; } 

        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public virtual Post Post { get; set; }
    }
}