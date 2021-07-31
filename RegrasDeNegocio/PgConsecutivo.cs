using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovoPGs.RegrasDeNegocio
{
    [Table("new_pg_consec", Schema = "public")]
    public class PgConsecutivo
    {
        [Column("id")]
        public Int64 Id { get; set; }
        [Column("posicao")]
        public Int64 Posicao{ get; set; }
        [Column("pg")]
        public Int32 Pg { get; set; }
    }
}
