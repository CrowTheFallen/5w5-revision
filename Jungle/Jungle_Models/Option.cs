using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jungle_Models
{
    public class Option
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        public double Price { get; set; }

        [ForeignKey("Travel")]
        public int Travel_Id { get; set; }

        [ForeignKey("Guide")]
        public int Guide_Id { get; set; }
    }
}
