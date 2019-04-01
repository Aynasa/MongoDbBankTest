namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [Key]
        public int Id { get; set; }

        public string FIO { get; set; }

        public string passport { get; set; }
        
        public ICollection<Vklad> Vklads;

        public Client()
        {
            Vklads = new HashSet<Vklad>();
        }
    }
}
