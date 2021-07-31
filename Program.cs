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
using NovoPGs.Context;
using System.Diagnostics;

namespace NovoPGs
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //NormalInsert();
            BulkInsert();
            //NovoPgConsec();
        }

        public static void BulkInsert()
        {
            Stopwatch sw = new Stopwatch();

            ConexaoContext ctx = new ConexaoContext();
            Primo primo = new Primo();
            List<Primo> ListaPrimos = new List<Primo>();
            try
            {
                primo = ctx.UltimoPrimo().ElementAt(0);
            }
            catch
            {
                primo.NumPrimo = 1;
            }
            ulong ultimoPrimo = (ulong)(primo.NumPrimo + 1);

            Console.WriteLine("Digite até qual número deseja verificar? (Ultimo primo no DB: " + (ultimoPrimo - 1) + "): ");
            try
            {
                var verificarNumero = ulong.Parse(Console.ReadLine());
                sw.Start();
                if (verificarNumero <= ultimoPrimo)
                {
                    Console.WriteLine("Números já verificados!");
                }
                else
                {
                    ulong batchSize = 100;
                    Console.WriteLine("Escolha o BatchSize: ");
                    try
                    {
                        batchSize = Convert.ToUInt64(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Valor Inválido. BatchSize de 100 será utilizado.");
                    }
                    ulong i = ultimoPrimo;
                    ulong inicio = ultimoPrimo;
                    ulong fim = inicio + batchSize;
                    int numInserts = 0;
                    while (i <= verificarNumero)
                    {
                        for (i = inicio; i <= fim; i++)
                        {
                            if (TodosOsMetodos.VerificarSePrimo(i))
                            {
                                Primo primo2 = new Primo();
                                Console.Write(i + ", ");
                                primo2.NumPrimo = (long)i;
                                ListaPrimos.Add(primo2);
                            }
                        }
                        inicio = fim;
                        if (fim <= (verificarNumero - batchSize))
                        {
                            fim += batchSize;
                        }
                        else
                        {
                            fim += verificarNumero - fim;
                        }
                        ctx.BulkInsert(ListaPrimos.ToList());
                        ListaPrimos.Clear();
                        numInserts++;
                    }
                    sw.Stop();
                    Console.WriteLine("\nSalvo com sucesso.");
                    Console.WriteLine("Tempo ={0}", sw.Elapsed);
                    Console.WriteLine("\nNúmero de Inserts: " + numInserts);
                }

            }
            catch
            {
                Console.WriteLine("Valor inválido. Tente Novamente.");
            }
        }

        public static void NormalInsert()
        {
            Stopwatch sw = new Stopwatch();

            ConexaoContext ctx = new ConexaoContext();
            Primo primo = new Primo();
            try
            {
                primo = ctx.UltimoPrimo().ElementAt(0);
            }
            catch
            {
                primo.NumPrimo = 1;
            }
            ulong ultimoPrimo = (ulong)(primo.NumPrimo + 1);

            Console.WriteLine("Digite até qual número deseja verificar? (Ultimo primo no DB: " + (ultimoPrimo - 1) + "): ");
            try
            {
                var verificarNumero = ulong.Parse(Console.ReadLine());
                sw.Start();
                if (verificarNumero <= ultimoPrimo)
                {
                    Console.WriteLine("Números já verificados!");
                }
                else
                {
                    int numInserts = 0;
                    for (ulong i = ultimoPrimo; i <= verificarNumero; i++)
                    {
                        if (TodosOsMetodos.VerificarSePrimo(i))
                        {

                            Console.Write(i + ", ");
                            primo.NumPrimo = (long)i;
                            ctx.ListaPrimos.Add(primo);
                            ctx.SaveChanges();
                            numInserts++;
                        }
                    }
                    sw.Stop();
                    Console.WriteLine("\nSalvo com sucesso.");
                    Console.WriteLine("Tempo ={0}", sw.Elapsed);
                    Console.WriteLine("\nNúmero de Inserts: " + numInserts);
                }

            }
            catch
            {
                Console.WriteLine("Valor inválido. Tente Novamente.");
            }
        }

        public static void NovoPgConsec()
        {
            ConexaoContext ctx = new ConexaoContext();
            PgConsecutivo pgConsec = new PgConsecutivo();

            try
            {
                pgConsec = ctx.UltimaPosicao().ElementAt(0);
            }
            catch
            {
                pgConsec.Posicao = 1;
            }            

            ulong fimVer = 100;
            Console.WriteLine("Já verificado: "+pgConsec.Posicao+". Verificar até: ");
            try
            {
                fimVer = UInt64.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Valor inválido.");
            }
            ulong inicioVerificacao = (ulong)pgConsec.Posicao;
            ulong inicio;
            ulong contador = (ulong) pgConsec.Posicao-1;
            ulong intervalo;
            ulong intervaloAnt = 0;
            ulong proximo;
            //bool resultado = true;

            do
            {
                inicio = TodosOsMetodos.ProximoPrimo(inicioVerificacao);
                proximo = TodosOsMetodos.ProximoPrimo(inicio);
                inicioVerificacao = proximo - 1;
                contador++;
                intervalo = proximo - inicio;

                if (intervaloAnt == intervalo)
                {
                    //resultado = false;
                    pgConsec.Pg = (int)intervalo;
                    pgConsec.Posicao = (long)contador;
                    ctx.ListaPgConsecutivos.Add(pgConsec);
                    ctx.SaveChanges();
                }
                else
                {
                    intervaloAnt = intervalo;
                    Console.Write(contador + ", ");
                }

            } while (fimVer != contador);

        }
    }
}
