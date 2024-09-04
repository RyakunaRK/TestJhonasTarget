using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TestJhonasTarget
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //Questão 1
            questaoUm();

            //Questão 2
            int num;
            Console.Write("Informe um valor para verificar se o mesmo pertence a sequência de Fibonacci: ");
            num = int.Parse(Console.ReadLine());
            Console.WriteLine(questaoDois(num));
            
            //Questão 3
            questaoTres();
            
            //Questão 4
            questaoQuatro();

            //Questão 5
            Console.WriteLine("\nDigite qualquer coisa para inverter: ");
            string texto = Console.ReadLine();
            questaoCinco(texto);
        }
        public static void questaoUm()
        {
            /*1) Observe o trecho de código abaixo: 
            * int INDICE = 13, SOMA = 0, K = 0;
            * Enquanto K < INDICE faça { K = K + 1; SOMA = SOMA + K; }
            * Imprimir(SOMA);
            * Ao final do processamento, qual será o valor da variável SOMA?*/

            int indice = 13;
            int soma = 0;
            int k;

            for (k = 0; k < indice; k = k + 1)
            {
                soma += k;
            }

            Console.WriteLine(soma);
        }

        public static String questaoDois(int num)
        {
            /*2) Dado a sequência de Fibonacci, onde se inicia por 0 e 1 e o próximo valor sempre será a soma dos 2 valores anteriores
             * (exemplo: 0, 1, 1, 2, 3, 5, 8, 13, 21, 34...), escreva um programa na linguagem que desejar onde, informado um número, 
             * ele calcule a sequência de Fibonacci e retorne uma mensagem avisando se o número informado pertence ou não a sequência.
             * IMPORTANTE: Esse número pode ser informado através de qualquer entrada de sua preferência ou pode ser previamente definido no código;*/
            
            int fibonacci = 0;
            int fibonacciAnterior = 1;

            do
            {
                if(fibonacci == num)
                    return "O número pertence a sequencia de fibonacci!";
                int temp = fibonacci;
                fibonacci += fibonacciAnterior;
                fibonacciAnterior = temp;

            } while (fibonacci <= num);

            return "O número não pertence a sequência de fibonacci";
        }

        public static void questaoTres()
        {
            /*3) Dado um vetor que guarda o valor de faturamento diário de uma distribuidora, faça um programa,
                 na linguagem que desejar, que calcule e retorne:
                • O menor valor de faturamento ocorrido em um dia do mês;
                • O maior valor de faturamento ocorrido em um dia do mês;
                • Número de dias no mês em que o valor de faturamento diário foi superior à média mensal.

                IMPORTANTE:
                a) Usar o json ou xml disponível como fonte dos dados do faturamento mensal;
                b) Podem existir dias sem faturamento, como nos finais de semana e feriados. Estes dias devem ser ignorados no cálculo da média;*/

            string json = File.ReadAllText("dados.json");
            List<FaturamentoDiario> faturamentos = JsonSerializer.Deserialize<List<FaturamentoDiario>>(json);
            var diasComFaturamento = faturamentos.Where(f => f.valor > 0).ToList();
            decimal menorValor = diasComFaturamento.Min(f => f.valor);
            decimal maiorValor = diasComFaturamento.Max(f => f.valor);
            decimal mediaMensal = diasComFaturamento.Average(f => f.valor);
            int diasAcimaDaMedia = diasComFaturamento.Count(f => f.valor > mediaMensal);

            Console.WriteLine($"Menor valor de faturamento: {menorValor:C}");
            Console.WriteLine($"Maior valor de faturamento: {maiorValor:C}");
            Console.WriteLine($"Número de dias com faturamento acima da média: {diasAcimaDaMedia}");

        }

        public static void questaoQuatro()
        {
            /*4) Dado o valor de faturamento mensal de uma distribuidora, detalhado por estado:
                • SP – R$67.836,43
                • RJ – R$36.678,66
                • MG – R$29.229,88
                • ES – R$27.165,48
                • Outros – R$19.849,53

                Escreva um programa na linguagem que desejar onde calcule o percentual de representação
                que cada estado teve dentro do valor total mensal da distribuidora.*/
            var faturamentoSP = new FaturamentoMensal("SP", 67836.43m);
            var faturamentoRJ = new FaturamentoMensal("RJ", 36678.66m);
            var faturamentoMG = new FaturamentoMensal("MG", 29229.88m);
            var faturamentoES = new FaturamentoMensal("ES", 27165.48m);
            var faturamentoOutros = new FaturamentoMensal("Outros", 19849.53m);

            decimal faturamentoTotal = faturamentoSP.valor + faturamentoRJ.valor + faturamentoMG.valor + faturamentoES.valor + faturamentoOutros.valor;

            Console.WriteLine("--- PERCENTUAL DE PARTICIPAÇÃO ---\n");
            Console.WriteLine($"SP: {faturamentoSP.calcularPorcentagemParticipacao(faturamentoTotal):F2}%");
            Console.WriteLine($"RJ: {faturamentoRJ.calcularPorcentagemParticipacao(faturamentoTotal):F2}%");
            Console.WriteLine($"MG: {faturamentoMG.calcularPorcentagemParticipacao(faturamentoTotal):F2}%");
            Console.WriteLine($"ES: {faturamentoES.calcularPorcentagemParticipacao(faturamentoTotal):F2}%");
            Console.WriteLine($"Outros Estados: {faturamentoOutros.calcularPorcentagemParticipacao(faturamentoTotal):F2}%");
            Console.Write("\n----------------------------------");

        }

        public static void questaoCinco(string txt)
        {
            /*5) Escreva um programa que inverta os caracteres de um string.

                IMPORTANTE:
                a) Essa string pode ser informada através de qualquer entrada de sua preferência ou pode ser previamente definida no código;
                b) Evite usar funções prontas, como, por exemplo, reverse;*/
            int tamanhoString = txt.Length;
            for(int i = tamanhoString - 1; i >= 0; i--)
            {
                Console.Write(txt[i]);
            }
        }

        public class FaturamentoMensal
        {
            public string estado { get; set; }
            public decimal valor { get; set; }

            public FaturamentoMensal(string estado, decimal valor)
            {
                this.estado = estado;
                this.valor = valor;
            }

            public decimal calcularPorcentagemParticipacao(decimal total)
            {
                return (this.valor/total)*100;
            }
        }

        public class FaturamentoDiario
        {
            public int dia { get; set; }
            public decimal valor { get; set; }
        }
    }
}
