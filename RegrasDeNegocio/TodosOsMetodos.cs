using System;
using System.Collections.Generic;
using System.Linq;

namespace NovoPGs.RegrasDeNegocio
{
    public class TodosOsMetodos
    {
        public static bool VerificarSePrimo(ulong numero)
{
    if (numero <= 1) 
    {
        return false;
    }

    if (numero == 2 || numero == 3)
    {
        return true;
    }

    if (numero % 2 == 0 || numero % 3 == 0)
    {
        return false;
    }

    ulong divisor = 5;
    while (divisor * divisor <= numero)
    {
        if (numero % divisor == 0 || numero % (divisor + 2) == 0)
        {
            return false;
        }

        divisor += 6;
    }

    return true;
}


        /// <summary>
        /// Verifica todos os números de divisores que ocorrem entre 1 e fimVer, conta a quantidade de cada um, finalmente retorna uma lista string com os números, qtd repetições e sua porcentagem em relação aos outros.
        /// </summary>
        /// <param name="fimVer"></param>
        /// <returns></returns>
        public static List<string> PorcentagemDeNumDiv(ulong fimVer)
{
    List<ulong> listaInicial = ListaUlongQtdNumDiv(fimVer);
    List<string> numeros = new List<string>();
    numeros.Add("Número - Repetições - Porcentagem");

    ulong maiorNum = listaInicial.Max();
    double qtdElementosLista = listaInicial.Count;

    for (ulong i = 3; i <= maiorNum; i++)
    {
        double contador = listaInicial.Count(n => n == i);
        if (contador > 0)
        {
            double porcentagem = contador * 100.0 / qtdElementosLista;
            numeros.Add($"{i} - {contador} - {Math.Round(porcentagem, 4)}");
        }
    }

    return numeros;
}

        /// <summary>
        /// Lista ulong com a quantidade de divisores dos números analisados, entre 1 e fimVerificação.
        /// </summary>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static List<ulong> ListaUlongQtdNumDiv(ulong fimVerificacao)
        {
            List<ulong> Numeros = new List<ulong>();

            for (ulong i = 1; i <= fimVerificacao; i++)
            {
                ulong qtd = QtdNumDivisiveis(i);
                if (qtd != 0)
                {
                    Numeros.Add(qtd + 1);
                }
            }
            return Numeros;
        }

        /// <summary>
        /// Lista string dos números, de 1 a fimVerificacao, com a quantidade de divisores que cada número tem.
        /// </summary>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static List<string> ListaQtdNumDivisiveis(ulong fimVerificacao)
        {
            List<string> Numeros = new List<string>();
            Numeros.Add("Número / Quantidade");

            for (ulong i = 1; i <= fimVerificacao; i++)
            {
                ulong qtd = QtdNumDivisiveis(i);
                if (qtd != 0)
                {
                    Numeros.Add($"{i} - {qtd + 1}");
                }
            }

            return Numeros;
        }

        /// <summary>
        /// Retorna a quantidade de divisores que um número escolhido, ulong, tem. Input e Output ULONG.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static ulong QtdNumDivisiveis(ulong numero)
        {
            ulong contador = 0;
            ulong quantidade = 0;


            if (!VerificarSePrimo(numero))
            {
                for (ulong i = 1; i <= numero; i++)
                {
                    if (numero % i == 0)
                    {
                        contador++;

                        if (contador > 1)
                        {
                            quantidade++;
                        }
                    }
                }
            }

            return quantidade;
        }

        /// <summary>
        /// Busca se o primegap escolhidos ocorre de forma consecutiva em algum momento da sequencia de prime gaps.
        /// </summary>
        /// <param name="primeGap"></param>
        /// <returns></returns>
        public static ulong EncontrarNumeroConsecutivoPrimeGap(ulong primeGap)
        {
            ulong inicioVerificacao = 0;
            ulong contador = 0;
            ulong intervalo;
            ulong intervaloAnt = 0;
            ulong inicio;
            ulong proximo;
            bool resultado = true;

            do
            {
                inicio = ProximoPrimo(inicioVerificacao);
                proximo = ProximoPrimo(inicio);
                inicioVerificacao = proximo - 1;
                contador++;
                intervalo = proximo - inicio;

                if (intervaloAnt == primeGap && intervalo == primeGap)
                {
                    resultado = false;
                }
                else
                {
                    intervaloAnt = intervalo;
                    Console.Write(contador + ", ");
                }

            } while (resultado);

            return contador;
        }

        /// <summary>
        /// Verifica a quantidade de números até que o primegap escolhido ocorra.
        /// </summary>
        /// <param name="primeGap"></param>
        /// <returns></returns>
        public static ulong NumerosAtePrimeGap(ulong primeGap)
        {
            ulong inicioVerificacao = 0;
            ulong contador = 0;
            ulong intervalo;
            ulong inicio;
            ulong proximo;

            do
            {
                inicio = ProximoPrimo(inicioVerificacao);
                proximo = ProximoPrimo(inicio);
                inicioVerificacao = proximo - 1;
                contador++;
                intervalo = proximo - inicio;
            } while (intervalo != primeGap);

            return contador - 1;
        }

        /// <summary>
        /// Lista todos os primegaps até ao escolhido, com sua respectiva distancia até ocorrer.
        /// </summary>
        /// <param name="finalPrimeGap"></param>
        /// <returns></returns>
        public static List<string> ListaDistanciaDeCadaNumeroPrimeGap(ulong finalPrimeGap)
        {
            List<string> lista = new List<string>();

            if (finalPrimeGap % 2 != 0)
            {
                Console.WriteLine("O valor prime Gap deve ser par sempre!");
            }
            else
            {
                for (ulong i = 2; i <= finalPrimeGap; i += 2)
                {
                    lista.Add(Convert.ToString(i) + ", " + Convert.ToString(NumerosAtePrimeGap(i) - 1));
                }
            }

            return lista;
        }

        /// <summary>
        /// Verifica se o número é divisível e retorna a lista dos valores que são divisíveis do número.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static List<ulong> EhDivisivelPor(ulong numero)
        {
            ulong contador = 0;

            List<ulong> vetor = new List<ulong>();

            if (!VerificarSePrimo(numero))
            {
                for (ulong i = 1; i <= numero; i++)
                {
                    if (numero % i == 0)
                    {
                        contador++;

                        if (contador > 1)
                        {
                            vetor.Add(i);
                        }
                    }
                }
            }
            return vetor;
        }

        /// <summary>
        /// Verifica o prime gap entre dois números primos consecutivos. Retorna ULONG com o prime gap.
        /// </summary>
        /// <param name="inicioVerficacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static ulong PrimeGap(ulong inicioVerficacao, ulong fimVerificacao)
        {
            ulong primeGap = 0;

            if (!VerificarSePrimo(inicioVerficacao))
            {
                inicioVerficacao = ProximoPrimo(inicioVerficacao);
            }
            if (!VerificarSePrimo(fimVerificacao))
            {
                fimVerificacao = ProximoPrimo(fimVerificacao);
            }

            primeGap = fimVerificacao - inicioVerficacao;

            return primeGap;
        }

        /// <summary>
        /// Verifica o prime gap com base o número de input, se primo, entra o próximo primo e calcula o prime gap. Se o número de input não é primo, calcula o próximo primo - retorna o prime gap.
        /// </summary>
        /// <param name="inicioVerficacao"></param>
        /// <returns></returns>
        public static ulong PrimeGap(ulong inicioVerficacao)
        {
            ulong primeGap = 0;

            if (!VerificarSePrimo(inicioVerficacao))
            {
                inicioVerficacao = ProximoPrimo(inicioVerficacao);
            }

            primeGap = ProximoPrimo(inicioVerficacao) - inicioVerficacao;

            return primeGap;
        }

        /// <summary>
        /// PRIVATE - Verifica se o número é primo e retorna true ou false. Entrada LONG. Saida BOOL.
        /// </summary>
        /// <param name="verificarNumeroSePrimo"></param>
        /// <returns></returns>
       public static bool VerificarSePrimo(long numero)
{
    if (numero <= 1) 
    {
        return false;
    }

    if (numero == 2 || numero == 3)
    {
        return true;
    }

    if (numero % 2 == 0 || numero % 3 == 0)
    {
        return false;
    }

    long divisor = 5;
    while (divisor * divisor <= numero)
    {
        if (numero % divisor == 0 || numero % (divisor + 2) == 0)
        {
            return false;
        }

        divisor += 6;
    }

    return true;
}


        /// <summary>
        /// Retorna o próximo número primo em relação ao número de input. Entrada e Saida ULONG.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static ulong ProximoPrimo(ulong numero)
        {
            bool result;

            do
            {
                numero++;
                result = VerificarSePrimo(numero);
            } while (!result);

            return numero;
        }

        /// <summary>
        /// PRIVATE - Retorna o próximo número primo em relação ao número de input. Entrada e Saida LONG.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        private static long ProximoPrimoLong(long numero)
        {
            bool result;

            do
            {
                numero++;
                result = VerificarSePrimoLong(numero);
            } while (!result);

            return numero;
        }

        /// <summary>
        /// Retorna o número primo anterior em relação ao número de input. Entrada e Saida ULONG.
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public static ulong AnteriorPrimo(ulong numero)
        {
            bool result;
            do
            {
                numero--;
                result = VerificarSePrimo(numero);
            } while (!result);

            return numero;
        }

        /// <summary>
        /// Retorna uma string com os números primos entre incioVerificacao e fimVerificacao. Também indica a quantidade de primos encontrada. Entrada ULONG e Saida STRING.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static string StringPrimosEmIntervalo(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            ulong intervalo = fimVerificacao - inicioVerificacao;
            string listaDePrimos = string.Empty;
            ulong qtdPrimos = 0;
            if (intervalo < 1) throw new Exception("Intervalo inválido.");
            else
            {
                do
                {
                    if (VerificarSePrimo(inicioVerificacao))
                    {
                        listaDePrimos += inicioVerificacao.ToString() + ", ";
                        qtdPrimos++;
                    }

                    inicioVerificacao++;
                } while (inicioVerificacao != fimVerificacao + 1);

            }
            string listaDePrimosFinal = listaDePrimos.Remove(listaDePrimos.Length - 2, 2);
            listaDePrimosFinal += $". Quantidade de primos encontrada: {qtdPrimos}.";
            return listaDePrimosFinal;
        }

        /// <summary>
        /// Retorna uma string com os números primos entre incioVerificacao e fimVerificacao, mas coloca o inicio como 1. Também indica a quantidade de primos encontrada. Entrada ULONG e Saida STRING.
        /// </summary>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static string PrimosEmIntervaloSTRING(ulong fimVerificacao)
        {
            return StringPrimosEmIntervalo(1, fimVerificacao);
        }

        /// <summary>
        /// PRIVATE - Retorna uma string com os números primos entre incioVerificacao e fimVerificacao. SEM a quantidade de primos encontrada. Entrada ULONG e Saida STRING.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        private static string PrimosEmIntervaloSemQtd(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            ulong intervalo = fimVerificacao - inicioVerificacao;
            string listaDePrimos = string.Empty;
            ulong qtdPrimos = 0;
            if (intervalo < 1) throw new Exception("Intervalo inválido.");
            else
            {
                do
                {
                    if (VerificarSePrimo(inicioVerificacao))
                    {
                        listaDePrimos += inicioVerificacao.ToString() + ", ";
                        qtdPrimos++;
                    }

                    inicioVerificacao++;
                } while (inicioVerificacao != fimVerificacao + 1);

            }
            string listaDePrimosFinal = listaDePrimos.Remove(listaDePrimos.Length - 2, 2);
            return listaDePrimosFinal;
        }

        /// <summary>
        /// Lista os primos no referido intervalo e retorna um ARRAY LONG com seus dados.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        private static long[] PrimosEmIntervalo(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            ulong qtd = QtdPrimosEmIntervalo(inicioVerificacao, fimVerificacao);

            long fimVer = Convert.ToInt64(fimVerificacao);
            long inicioVer = Convert.ToInt64(inicioVerificacao);

            int j = 0;

            long[] primos = new long[qtd];

            for (long i = inicioVer; i < fimVer; i++)
            {
                if (i > fimVer) { }
                else
                {
                    if (VerificarSePrimoLong(i))
                    {
                        primos[j] = i;
                        j++;
                    }
                }
            }

            return primos;
        }

        /// <summary>
        /// PRIVATE - Idêntico ao anterior, mas coloca o inicio como 1. Entrada: ULONG. Saída: STRING.
        /// </summary>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        private static string PrimosEmIntervaloSemQtd(ulong fimVerificacao)
        {
            return PrimosEmIntervaloSemQtd(1, fimVerificacao);
        }

        /// <summary>
        /// Retorna quantos primos existem entre inicioVerificacao e fimVerificacao. Entrada e Saida ULONG
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static ulong QtdPrimosEmIntervalo(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            ulong intervalo = fimVerificacao - inicioVerificacao;
            ulong qtdPrimos = 0;
            if (intervalo < 1) throw new Exception("Intervalo inválido.");
            else
            {
                do
                {
                    if (VerificarSePrimo(inicioVerificacao))
                    {
                        qtdPrimos++;
                    }

                    inicioVerificacao++;
                } while (inicioVerificacao != fimVerificacao + 1);
            }

            return qtdPrimos;
        }

        /// <summary>
        /// Indica a quantidade de primos no intervalo entre 1 e o número input: fimVerificacao. Entrada e Saida ULONG.
        /// </summary>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static ulong QtdPrimosEmIntervalo(ulong fimVerificacao)
        {
            return QtdPrimosEmIntervalo(1, fimVerificacao);
        }

        /// <summary>
        /// PRIVATE - Retorna quantos primos existem entre inicioVerificacao e fimVerificacao. Entrada e Saida LONG.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        private static long QtdPrimosEmIntervaloLong(long inicioVerificacao, long fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            long intervalo = fimVerificacao - inicioVerificacao;
            long qtdPrimos = 0;
            if (intervalo < 1) throw new Exception("Intervalo inválido.");
            else
            {
                do
                {
                    if (VerificarSePrimoLong(inicioVerificacao))
                    {
                        qtdPrimos++;
                    }

                    inicioVerificacao++;
                } while (inicioVerificacao != fimVerificacao + 1);
            }

            return qtdPrimos;
        }

        /// <summary>
        /// PRIVATE - Indica a quantidade de primos no intervalo entre 1 e o número input: fimVerificacao. Entrada e Saida LONG.
        /// </summary>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        private static long QtdPrimosEmIntervaloLong(long fimVerificacao)
        {
            return QtdPrimosEmIntervaloLong(1, fimVerificacao);
        }

        /// <summary>
        /// Indica a quantidade de números não-primos existem entre dois primos indicados em inicioVerificacao e fimVerificacao. Entrada e Saida ULONG.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static ulong QtdNumerosEntreDoisPrimos(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            bool res1 = VerificarSePrimo(inicioVerificacao);
            bool res2 = VerificarSePrimo(fimVerificacao);
            ulong intervalo = 0;

            if (res1 && res2)
            {
                return intervalo = fimVerificacao - inicioVerificacao;
            }
            else
            {
                throw new Exception("Um dos números não é primo.");
            }
        }

        /// <summary>
        /// Returna uma array ULONG com os valores de todos os PRIME GAPS no intervalo. Entrada e Saida ARRAY ULONG.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static ulong[] PrimeGapsArrayUlong(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            ulong[] valores = new ulong[QtdPrimosEmIntervalo(inicioVerificacao, fimVerificacao)];

            ulong inicio = ProximoPrimo(inicioVerificacao);
            ulong proximo = ProximoPrimo(inicio);
            ulong intervalo = proximo - inicio;
            ulong i = 1;

            valores[0] = intervalo;

            while (proximo < fimVerificacao)
            {
                inicio = proximo;
                proximo = ProximoPrimo(inicio);
                intervalo = proximo - inicio;
                valores[i] = intervalo;
                i++;
            }

            return valores;
        }

        /// <summary>
        /// PRIVATE - Returna uma array ULONG com os valores de todos os PRIME GAPS no intervalo. Entrada e Saida ARRAY LONG.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        private static long[] PrimeGapsArrayLong(long inicioVerificacao, long fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            long qtdPrimos = QtdPrimosEmIntervaloLong(inicioVerificacao, fimVerificacao) - 1;

            long[] primeGap = new long[qtdPrimos];

            long inicio = ProximoPrimoLong(inicioVerificacao);
            long proximo = ProximoPrimoLong(inicio);
            long intervalo = proximo - inicio;

            primeGap[0] = intervalo;

            for (long j = 1; j < qtdPrimos; j++)
            {
                inicio = proximo;
                proximo = ProximoPrimoLong(inicio);
                intervalo = proximo - inicio;
                primeGap[j] = intervalo;
            }

            return primeGap;
        }

        /// <summary>
        /// Lista em uma string somente os valores dos PRIME GAPS no intervalo selecionado. Entrada ULONG. Saida STRING.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static string PrimeGapsString(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            ulong[] valores = PrimeGapsArrayUlong(inicioVerificacao, fimVerificacao);

            string listaValores = string.Empty;

            foreach (ulong elemento in valores)
            {
                listaValores += elemento + ", ";
            }

            string listaFinal = listaValores.Remove(listaValores.Length - 2, 2) + ".";

            return listaFinal;
        }

        /// <summary>
        /// Retorna uma string com os números primos e entre eles os PRIME GAPS. Entrada ULONG. Saida STRING.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static string ListaDePrimosEPrimeGaps(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            string lista = string.Empty;

            ulong inicio = ProximoPrimo(inicioVerificacao);
            ulong proximo = ProximoPrimo(inicio);
            ulong intervalo = proximo - inicio;

            lista += inicio + ", (" + intervalo + "), " + proximo + ", (";

            while (proximo < fimVerificacao)
            {
                inicio = proximo;
                proximo = ProximoPrimo(inicio);
                intervalo = proximo - inicio;

                lista += intervalo + "), " + proximo + ", (";
            }

            string listaFinal = lista.Remove(lista.Length - 3, 3);
            listaFinal += ".";

            return listaFinal;

        }

        /// <summary>
        /// PRIVATE - Retorna a próxima linha como STRING e recebe ULONG.
        /// </summary>
        /// <param name="inicial"></param>
        /// <returns></returns>
        private static string ProximaLinhaString(ulong[] inicial)
        {
            ulong contador = 0;
            string linha = string.Empty;
            foreach (ulong element in inicial)
            {
                contador++;
            }

            ulong[] proximaLinha = new ulong[contador - 1];

            for (ulong i = 0; i < contador - 1; i++)
            {
                proximaLinha[i] = inicial[i + 1] - inicial[i];
            }

            foreach (ulong element in proximaLinha)
            {
                linha += element + ", ";
            }

            string linhaFinal = linha.Remove(linha.Length - 2, 2) + ".";

            return linhaFinal;
        }

        /// <summary>
        /// PRIVATE - Retorna a próxima linha como ARRAY ULONG e recebe ARRAY ULONG.
        /// </summary>
        /// <param name="inicial"></param>
        /// <returns></returns>
        private static ulong[] ProximaLinhaUlong(ulong[] inicial)
        {
            ulong contador = 0;

            foreach (ulong element in inicial)
            {
                contador++;
            }

            ulong[] proximaLinha = new ulong[contador - 1];

            for (ulong i = 0; i < contador - 1; i++)
            {
                proximaLinha[i] = inicial[i + 1] - inicial[i];
            }

            return proximaLinha;
        }

        /// <summary>
        /// PRIVATE - Retorna a próxima linha como ARRAY LONG e recebe ARRAY LONG.
        /// </summary>
        /// <param name="inicial"></param>
        /// <returns></returns>
        private static long[] ProximaLinhaLong(long[] inicial)
        {
            long contador = 0;

            foreach (long element in inicial)
            {
                contador++;
            }

            long[] proximaLinha = new long[contador - 1];

            for (long i = 0; i < contador - 1; i++)
            {
                proximaLinha[i] = inicial[i + 1] - inicial[i];
            }

            return proximaLinha;
        }


        /// <summary>
        /// Convert ARRAY LONG para STRING.
        /// </summary>
        /// <param name="inicial"></param>
        /// <returns></returns>
        private static string ConverterLongParaString(long[] inicial)
        {
            string final = string.Empty;

            foreach (long element in inicial)
            {
                final += element + ", ";
            }

            return final;
        }


        /// <summary>
        /// Convert ARRAY ULONG para STRING.
        /// </summary>
        /// <param name="inicial"></param>
        /// <returns></returns>
        private static string ConverterUlongParaString(ulong[] inicial)
        {
            string final = string.Empty;

            foreach (ulong element in inicial)
            {
                final += element + ", ";
            }

            return final;
        }

        /// <summary>
        /// NOT WORKING PROPERLY - Lista todas as camadas de cálculo. Entrada: ULONG. Saida STRING.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        private static string ImprimirTodasAsCamadas2(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            ulong qtdPrimos = QtdPrimosEmIntervalo(inicioVerificacao, fimVerificacao);

            string[] linha = new string[qtdPrimos];

            linha[0] = StringPrimosEmIntervalo(inicioVerificacao, fimVerificacao); //primos

            linha[1] = PrimeGapsString(inicioVerificacao, fimVerificacao); //intervalor entre primos

            long aux1 = Convert.ToInt64(inicioVerificacao);
            long aux2 = Convert.ToInt64(fimVerificacao);

            long[] valor = PrimeGapsArrayLong(aux1, aux2); //em ulong anterior

            for (ulong i = 2; i < qtdPrimos; i++)
            {
                linha[i] = ConverterLongParaString(ProximaLinhaLong(valor));
                valor = ProximaLinhaLong(valor);
            }

            string listaLinhas = string.Empty;

            foreach (string element in linha)
            {
                listaLinhas += element + "\n";
            }

            return listaLinhas;
        }

        /// <summary>
        /// WORKING!!! Lista todas as camadas de cálculo. Entrada: ULONG. Saida STRING.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static string ImprimirTodasAsCamadas(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            ulong qtdPrimos = QtdPrimosEmIntervalo(inicioVerificacao, fimVerificacao) + 1;

            long aux1 = Convert.ToInt64(inicioVerificacao);
            long aux2 = Convert.ToInt64(fimVerificacao);

            string[] linha = new string[qtdPrimos];

            linha[0] = ConverterLongParaString(PrimosEmIntervalo(inicioVerificacao, fimVerificacao));

            linha[1] = ConverterLongParaString(PrimeGapsArrayLong(aux1, aux2));

            long[] valor = PrimeGapsArrayLong(aux1, aux2); //em ulong anterior

            for (ulong i = 2; i < qtdPrimos; i++)
            {
                linha[i] = ConverterLongParaString(ProximaLinhaLong(valor));
                valor = ProximaLinhaLong(valor);
            }

            string listaLinhas = string.Empty;

            foreach (string element in linha)
            {
                listaLinhas += element + "\n";
            }

            return listaLinhas;
        }

        public static string Imprimir3Camadas(ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            long aux1 = Convert.ToInt64(inicioVerificacao);
            long aux2 = Convert.ToInt64(fimVerificacao);

            string[] linha = new string[3];

            linha[0] = ConverterLongParaString(PrimosEmIntervalo(inicioVerificacao, fimVerificacao));

            linha[1] = ConverterLongParaString(PrimeGapsArrayLong(aux1, aux2));

            long[] valor = PrimeGapsArrayLong(aux1, aux2); //em ulong anterior

            for (ulong i = 2; i < 3; i++)
            {
                linha[i] = ConverterLongParaString(ProximaLinhaLong(valor));
                valor = ProximaLinhaLong(valor);
            }

            string listaLinhas = string.Empty;

            foreach (string element in linha)
            {
                listaLinhas += element + "\n";
            }

            return listaLinhas;
        }

        /// <summary>
        /// Verifica se o inicio e o fim da verificação são válidos. Entrada ULONG e Saida VOID.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        private static void VerificarExceptionInput(ulong inicioVerificacao, ulong fimVerificacao)
        {
            if (fimVerificacao - inicioVerificacao < 1)
            {
                throw new Exception("Valores inválidos.");
            }
        }

        /// <summary>
        /// Verifica se o inicio e o fim da verificação são válidos. Entrada LONG e Saida VOID.
        /// </summary>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        private static void VerificarExceptionInput(long inicioVerificacao, long fimVerificacao)
        {
            if (fimVerificacao - inicioVerificacao < 1)
            {
                throw new Exception("Valores inválidos.");
            }
        }

        /// <summary>
        /// Retorna ARRAY LONG com as distancias entre PRIME GAPS específicos. Ex: entre 1 a 100, analisando 2, lista a distancia entre cada PRIME GAP 2 presente no intervalo selecionado.
        /// </summary>
        /// <param name="primeGapNumero"></param>
        /// <param name="inicioVerificacao"></param>
        /// <param name="fimVerificacao"></param>
        /// <returns></returns>
        public static long[] DistanciaEntrePrimeGaps(long primeGapNumero, ulong inicioVerificacao, ulong fimVerificacao)
        {
            VerificarExceptionInput(inicioVerificacao, fimVerificacao);

            long inicioVer = Convert.ToInt64(inicioVerificacao);
            long fimVer = Convert.ToInt64(fimVerificacao);

            long[] primeGaps = PrimeGapsArrayLong(inicioVer, fimVer);

            long qtdPrimos = QtdPrimosEmIntervaloLong(inicioVer, fimVer);

            long qtdRepeticoes = QtdRepeticoesArray(primeGaps, primeGapNumero);

            long[] soDistancia = new long[qtdRepeticoes];

            long contador = -1;
            int posicao = 0;

            for (long i = 0; i < qtdPrimos; i++)
            {
                contador++;
                if (primeGaps[i] == primeGapNumero)
                {
                    soDistancia[posicao] = contador;
                    contador = -1;
                    posicao++;
                    if (posicao == qtdRepeticoes)
                    {
                        break;
                    }
                }
            }

            return soDistancia;

        }

        /// <summary>
        /// Indica a quantidade de vezes que um determinado número se repete no array. LONG
        /// </summary>
        /// <param name="vetor"></param>
        /// <param name="verificarNumero"></param>
        /// <returns></returns>
        public static long QtdRepeticoesArray(long[] vetor, long verificarNumero)
        {
            long contador = 0;

            foreach (long element in vetor)
            {
                if (element == verificarNumero)
                {
                    contador++;
                }
            }

            return contador;
        }

        /// <summary>
        ///  Indica a quantidade de vezes que um determinado número se repete no array. ULONG.
        /// </summary>
        /// <param name="vetor"></param>
        /// <param name="verificarNumero"></param>
        /// <returns></returns>
        public static long QtdRepeticoesArray(ulong[] vetor, ulong verificarNumero)
        {
            long contador = 0;

            foreach (ulong element in vetor)
            {
                if (element == verificarNumero)
                {
                    contador++;
                }
            }

            return contador;
        }
        /// <summary>
        /// Soma os valores do vetor e retorna o somatório. Entrada ARRAY LONG. Saida LONG.
        /// </summary>
        /// <param name="vetor"></param>
        /// <returns></returns>
        public static long SomaDoVetorLong(long[] vetor)
        {
            long somatorio = 0;

            foreach (long element in vetor)
            {
                somatorio += element;
            }

            return somatorio;
        }

    }
}
