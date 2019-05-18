using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace benchmark
{
    abstract class Benchmark  {
        abstract protected void benchmark();    // método abstrato
        public long rodaBenchmark(int numVezes)  {
            long inicio = System.Diagnostics.Stopwatch.GetTimestamp();
        
            for (int i = 0; i < numVezes; i++)
                this.benchmark();            // usa o método abstrato normalmente!

            return System.Diagnostics.Stopwatch.GetTimestamp() - inicio;
        }
    }

    class BuuuBenchmark : Benchmark {
        protected override void benchmark() { Console.WriteLine("Buuu Buuu Buuu :-()"); }
    }



    class DivideVetorIntBenchmark : Benchmark {
        int[] vetor;

        public DivideVetorIntBenchmark(object vetor) {
	      this.vetor = (int[]) vetor;
          for (int i = 0; i < this.vetor.Length; i++)
             this.vetor[i] = 1234;
	    }

        protected override void benchmark() {
            for (int i = 0; i < this.vetor.Length; i++)
                this.vetor[i] /= 1000;
        }
    }
    class DivideVetorDoubleBenchmark : Benchmark {
        double[] vetor;

        public DivideVetorDoubleBenchmark(object vetor) {
            this.vetor = (double[])vetor;
            for (int i = 0; i < this.vetor.Length; i++)
                this.vetor[i] = 1234.0;
        }

        protected override void benchmark() {
            for (int i = 0; i < this.vetor.Length; i++)
                this.vetor[i] /= 1000.0;
        }
    }

    class Program {
        /*
         * Este programa tem dois parâmetros qu lhe são necessários para aexecução.
         * Altere os parâmetros de dois modos. O nome do meu projeto é "benchmark"
         * no texto que segue:
         * 
         *  1) Vá ao menu Debug (Depurar), depois em benchmark properties (Propriedades de 
         *  benchmark)... Na caixa de texto Command line argumments (Argumentos da linha de
         *  comando): coloque três números. P. ex: 100 500000 10
         *  
         *  2) Abra o prompt de comando. Vá ao diretório (pasta) \benchmark\benchmark\bin\Debug
         *  e rode o programa com os argumentos. Por exemplo: benchmark 100 1000000 10  
         *   
         *  Você notou diferença nos tempos de execução nos dois modos? 
         *  Se houve, explique!...
         *  
         * */
        static void Main(string[] args) {
            int repeticoes = int.Parse(args[0]);
            int TAMANHO_VETOR = int.Parse(args[1]);
            int EXECUCOES = int.Parse(args[2]);

            /*
             * Siga  o modelo do código no "for" e crie uma classe com código para
             * benchmarking e poste no SGA.
             * 
             * */
            for (int i = 0; i < EXECUCOES; i++) {
                Console.WriteLine("\nRodada de benchmarks número " + (i + 1));
                //            Benchmark tester = new BuuuBenchmark();
                Benchmark tester = new DivideVetorIntBenchmark(new int[TAMANHO_VETOR]);
                long clocks = tester.rodaBenchmark(repeticoes);
                System.TimeSpan intervalo = new TimeSpan(clocks);

                Console.WriteLine("Divisão inteira: rodados " + repeticoes + " metodos em "
                                                 + intervalo.Milliseconds + " milissegundos.");
                Console.WriteLine("Média do tempo: " + (intervalo.Milliseconds / (double)repeticoes) + " ms por chamada do método.");


                tester = new DivideVetorDoubleBenchmark(new double[TAMANHO_VETOR]); 
                clocks = tester.rodaBenchmark(repeticoes);
                intervalo = new TimeSpan(clocks);

                Console.WriteLine("Divisão exata (double): rodados " + repeticoes + " metodos em "
                                                 + intervalo.Milliseconds + " milissegundos.");
                Console.WriteLine("Média do tempo: " + (intervalo.Milliseconds / (double)repeticoes) + " ms por chamada do método.");
            }
        Console.Write("\n\n\t\tPressione qualquer tecla para continuar...");
        Console.ReadKey();
        }
    }
}
