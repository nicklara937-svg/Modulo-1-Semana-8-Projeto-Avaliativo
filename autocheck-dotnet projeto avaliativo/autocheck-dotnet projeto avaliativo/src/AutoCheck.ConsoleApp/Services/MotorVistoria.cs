using System;
using System.Collections.Generic;
using System.Globalization;
using AutoCheck.ConsoleApp.Models;

namespace AutoCheck.ConsoleApp.Services
{
    /// <summary>
    /// Motor de processamento da vistoria: concentra o cálculo de
    /// pontuação, a classificação do veículo e a geração do relatório
    /// exibido no terminal. Toda a varredura de listas é feita com
    /// laços tradicionais (for / foreach), sem uso de LINQ (RF09).
    /// </summary>
    public class MotorVistoria
    {
        /// <summary>
        /// RF04 - Soma a pontuação de todos os itens vistoriados do veículo.
        /// </summary>
        public int CalcularPontuacaoObtida(Veiculo veiculo)
        {
            int pontuacaoObtida = 0;

            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                pontuacaoObtida += item.ObterPontuacao();
            }

            return pontuacaoObtida;
        }

        /// <summary>
        /// Pontuação máxima possível: total de itens x 10 pontos.
        /// </summary>
        public int CalcularPontuacaoMaxima(Veiculo veiculo)
        {
            return veiculo.VistoriaRealizada.Count * 10;
        }

        /// <summary>
        /// RF05 - Calcula o percentual de aprovação do veículo.
        /// Faz o cast para double antes da divisão para não truncar o
        /// resultado em uma divisão de inteiros.
        /// </summary>
        public double CalcularPercentual(Veiculo veiculo)
        {
            int pontuacaoObtida = CalcularPontuacaoObtida(veiculo);
            int pontuacaoMaxima = CalcularPontuacaoMaxima(veiculo);

            if (pontuacaoMaxima == 0)
            {
                return 0;
            }

            double percentual = ((double)pontuacaoObtida / pontuacaoMaxima) * 100;
            return percentual;
        }

        /// <summary>
        /// RF06 - Classifica o veículo de acordo com o percentual atingido.
        /// </summary>
        public string ClassificarVeiculo(double percentual)
        {
            string classificacao;

            if (percentual >= 90)
            {
                classificacao = "Aprovado com Excelência";
            }
            else if (percentual >= 60)
            {
                classificacao = "Aprovado com Apontamentos";
            }
            else
            {
                classificacao = "Reprovado na Vistoria";
            }

            return classificacao;
        }

        /// <summary>
        /// RF07 - Retorna os itens críticos (status "Ruim").
        /// </summary>
        public List<ItemVistoria> ObterItensCriticos(Veiculo veiculo)
        {
            List<ItemVistoria> criticos = new List<ItemVistoria>();

            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                if (item.Status == "Ruim")
                {
                    criticos.Add(item);
                }
            }

            return criticos;
        }

        /// <summary>
        /// RF07 - Retorna os itens de atenção (status "Regular").
        /// </summary>
        public List<ItemVistoria> ObterItensAtencao(Veiculo veiculo)
        {
            List<ItemVistoria> atencao = new List<ItemVistoria>();

            for (int i = 0; i < veiculo.VistoriaRealizada.Count; i++)
            {
                ItemVistoria item = veiculo.VistoriaRealizada[i];

                if (item.Status == "Regular")
                {
                    atencao.Add(item);
                }
            }

            return atencao;
        }

        /// <summary>
        /// RF08 - Sugere o serviço prioritário que a oficina deve executar
        /// para um item específico, com base no nome do item.
        /// </summary>
        public string ObterRecomendacaoServico(string nomeItem)
        {
            string recomendacao;

            switch (nomeItem)
            {
                case "Nível de Óleo do Motor":
                    recomendacao = "Realizar troca de óleo e filtro do motor.";
                    break;
                case "Bateria e Sistema Elétrico":
                    recomendacao = "Testar a bateria e revisar todo o sistema elétrico.";
                    break;
                case "Documentação Regularizada":
                    recomendacao = "Regularizar a documentação do veículo junto ao órgão de trânsito.";
                    break;
                case "Estepe e Macaco":
                    recomendacao = "Calibrar o pneu reserva e verificar o funcionamento do macaco.";
                    break;
                case "Triângulo de Sinalização":
                    recomendacao = "Repor equipamento obrigatório ausente ou danificado.";
                    break;
                case "Ar Condicionado Funcional":
                    recomendacao = "Realizar higienização e checagem do gás refrigerante.";
                    break;
                case "Kit Transmissão/Corrente":
                    recomendacao = "Substituir o kit de relação (corrente, coroa e pinhão).";
                    break;
                case "Manetes de Freio/Embreagem":
                    recomendacao = "Ajustar ou substituir as manetes de freio e embreagem.";
                    break;
                case "Pezinho Lateral":
                    recomendacao = "Reparar ou substituir o pezinho lateral (cavalete).";
                    break;
                case "Tacógrafo":
                    recomendacao = "Realizar a calibração/aferição do tacógrafo.";
                    break;
                case "Sistema de Freios a Ar":
                    recomendacao = "Executar manutenção completa do sistema de freios a ar.";
                    break;
                case "Trava e Lona da Caçamba":
                    recomendacao = "Reparar a trava e substituir a lona da caçamba.";
                    break;
                default:
                    recomendacao = "Realizar inspeção detalhada e manutenção preventiva no item.";
                    break;
            }

            return recomendacao;
        }

        /// <summary>
        /// RF10 - Imprime, de forma formatada, o relatório completo de uma
        /// vistoria já processada (dados do veículo, itens, pontuação,
        /// percentual, classificação e recomendações da oficina).
        /// </summary>
        public void ExibirRelatorio(Veiculo veiculo, int numeroAtual, int totalVistorias)
        {
            CultureInfo ptBr = CultureInfo.GetCultureInfo("pt-BR");

            int pontuacaoObtida = CalcularPontuacaoObtida(veiculo);
            int pontuacaoMaxima = CalcularPontuacaoMaxima(veiculo);
            double percentual = CalcularPercentual(veiculo);
            string classificacao = ClassificarVeiculo(percentual);
            List<ItemVistoria> criticos = ObterItensCriticos(veiculo);
            List<ItemVistoria> atencao = ObterItensAtencao(veiculo);

            Console.WriteLine();
            Console.WriteLine("[" + numeroAtual + "/" + totalVistorias + "] PROCESSANDO VISTORIA");
            Console.WriteLine(new string('-', 67));

            Console.WriteLine("> DADOS DO VEÍCULO:");
            Console.WriteLine("  - Tipo: " + veiculo.ObterTipo());
            Console.WriteLine("  - Modelo: " + veiculo.Marca + " " + veiculo.Modelo);
            Console.WriteLine("  - Ano: " + veiculo.Ano + " | Quilometragem: " + veiculo.Quilometragem.ToString("N0", ptBr) + " km");
            Console.WriteLine("  - Atributo Específico: " + veiculo.ObterAtributoEspecifico());
            Console.WriteLine();

            Console.WriteLine("> AVALIAÇÃO DOS ITENS INSPECIONADOS (" + veiculo.VistoriaRealizada.Count + " ITENS):");
            foreach (ItemVistoria item in veiculo.VistoriaRealizada)
            {
                string marcador;
                if (item.Status == "Bom")
                {
                    marcador = "[OK]";
                }
                else if (item.Status == "Regular")
                {
                    marcador = "[ ! ]";
                }
                else
                {
                    marcador = "[ X ]";
                }

                Console.WriteLine("  " + marcador + " " + item.Nome.PadRight(35, '-') + " Status: " + item.Status + " (" + item.ObterPontuacao() + " pts)");
            }
            Console.WriteLine();

            Console.WriteLine("> RESUMO DA PONTUAÇÃO:");
            Console.WriteLine("  - Pontuação Atingida: " + pontuacaoObtida + " de " + pontuacaoMaxima + " pontos possíveis");
            Console.WriteLine("  - Percentual de Aprovação: " + percentual.ToString("0.0", ptBr) + "%");
            Console.WriteLine("  - Classificação Final: [ " + classificacao.ToUpper() + " ]");
            Console.WriteLine();

            Console.WriteLine("> RELATÓRIO DE MANUTENÇÃO E RECOMENDAÇÕES DA OFICINA:");

            if (criticos.Count == 0 && atencao.Count == 0)
            {
                Console.WriteLine("  🟢 Nenhuma pendência mecânica identificada. Veículo liberado para operação!");
            }
            else
            {
                if (criticos.Count > 0)
                {
                    Console.WriteLine("  🔴 ITENS CRÍTICOS / REPROVADOS (AÇÃO IMEDIATA):");
                    foreach (ItemVistoria item in criticos)
                    {
                        Console.WriteLine("     - " + item.Nome + ": " + ObterRecomendacaoServico(item.Nome));
                    }
                    Console.WriteLine();
                }

                if (atencao.Count > 0)
                {
                    Console.WriteLine("  🟡 ITENS DE ATENÇÃO (REVISÃO PREVENTIVA):");
                    foreach (ItemVistoria item in atencao)
                    {
                        Console.WriteLine("     - " + item.Nome + ": " + ObterRecomendacaoServico(item.Nome));
                    }
                }
            }

            Console.WriteLine(new string('-', 67));
        }
    }
}
