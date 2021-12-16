using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jungle_Models
{
    public class Evaluation
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }


        [ForeignKey("Travel")]
        public int Travel_Id { get; set; }
    }
}
