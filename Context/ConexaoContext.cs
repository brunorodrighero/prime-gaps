using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using NovoPGs.RegrasDeNegocio;

namespace NovoPGs.Context
{
    public class ConexaoContext : DbContext
    {
        public ConexaoContext() : base("PgPrimos")
        {

        }

        public DbSet<Primo> ListaPrimos { get; set; }

        public DbSet<PrimeGaps> ListaPrimeGaps { get; set; }

        public DbSet<PgConsecutivo> ListaPgConsecutivos { get; set; }

        public List<Primo> UltimoPrimo()
        {
            string sql = "select * from primo order by primo.id desc limit 1";
            return Database.SqlQuery<Primo>(sql).ToList();
        }

        public List<PgConsecutivo> UltimaPosicao()
        {
            string sql = "select * from new_pg_consec order by id desc limit 1";
            return Database.SqlQuery<PgConsecutivo>(sql).ToList();
        }

    }
}
