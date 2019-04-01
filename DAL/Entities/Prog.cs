namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Prog")]
    public partial class Prog
    {
        [Key]
        public int Id { get; set; }

        public int percent { get; set; }

        public string name { get; set; }


        public ICollection<Vklad> Vklads { get; set; }

        public Prog()
        {
            Vklads = new HashSet<Vklad>();
        }
    }
}
