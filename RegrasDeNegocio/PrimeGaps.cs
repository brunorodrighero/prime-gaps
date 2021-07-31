using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovoPGs.RegrasDeNegocio
{
    [Table("primegaps", Schema = "public")]
    public class PrimeGaps
    {
        [Column("id")]
        public Int64 Id { get; set; }
        [Column("primegaps")]
        public Int64 PrimeGap { get; set; }
    }
}
