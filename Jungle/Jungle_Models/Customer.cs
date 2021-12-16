using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Jungle_Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(25)]
        public string LastName { get; set; }
        [MaxLength(25)]
        public string FristName { get; set; }
        [MaxLength(30)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public List<Travel> Travels { get; set; }
    }
}
