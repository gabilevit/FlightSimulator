using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public abstract class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FlightNum { get; set; }
        public string Contry { get; set; }
        public bool IsDeparture { get; set; }
        public bool IsArrival { get; set; }
        public DateTime Date { get; set; }
    }
}
