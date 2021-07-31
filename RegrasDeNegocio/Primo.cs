using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovoPGs.RegrasDeNegocio
{
    [Table("primo", Schema = "public")]
    public class Primo
    {
        [Column("id")]
        public Int64 Id { get; set; }
        [Column("numprimo")]
        public Int64 NumPrimo { get; set; }

    }
}
