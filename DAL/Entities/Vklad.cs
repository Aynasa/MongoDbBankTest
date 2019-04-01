namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vklad")]
    public partial class Vklad
    {
        [Key]
        public int Id { get; set; }

        public DateTime DateOpen { get; set; }

        public int Balance { get; set; }

        public int Prog_code { get; set; }

        public int Client_code { get; set; }

        [ForeignKey("Prog")]
        public int Prog_id { get; set; }

        [ForeignKey("Client")]
        public int Client_id { get; set; }

        public virtual Client Client { get; set; }

        public virtual Prog Prog { get; set; }

    }
}
