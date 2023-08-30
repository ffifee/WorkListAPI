using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WorkListAPI.Models
{
    public class ListItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(20)")]
        public string ListItemName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string ListItemDesc { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool ListItemStatus { get; set; }
    }
}
