using System;
using System.Collections.Generic;
using AutoCheck.ConsoleApp.Models;
using AutoCheck.ConsoleApp.Services;

namespace AutoCheck.ConsoleApp
{
    /// <summary>
    /// AutoCheck.ConsoleApp - Motor de Vistoria Veicular.
    /// Ponto de entrada da aplicação: exibe o menu principal (RF11) e
    /// orquestra a coleta de dados, o processamento (MotorVistoria) e a
    /// exibição dos relatórios.
    /// </summary>
    internal class Program
    {
        private static readonly List<Veiculo> vistorias = new List<Veiculo>();
        private static readonly MotorVistoria motor = new MotorVistoria();

        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool continuarExecutando = true;

            ExibirCabecalho();

            while (continuarExecutando)
            {
                ExibirMenuPrincipal();
                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        RealizarNovaVistoria();
                        break;
                    case "2":
                        ExibirRelatorioDasVistorias();
                        break;
                    case "0":
                        continuarExecutando = false;
                        Console.WriteLine();
                        Console.WriteLine("===================================================================");
                        Console.WriteLine("                FIM DO PROCESSAMENTO DE VISTORIAS                 ");
                        Console.WriteLine("===================================================================");
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Opção inválida. Por favor, escolha 1, 2 ou 0.");
                        break;
                }
            }
        }

        private static void ExibirCabecalho()
        {
            Console.WriteLine("===================================================================");
            Console.WriteLine("                   AUTOCHECK .NET - MOTOR DE VISTORIA             ");
            Console.WriteLine("===================================================================");
        }

        private static void ExibirMenuPrincipal()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------- MENU PRINCIPAL ------------------------");
            Console.WriteLine("1 - Realizar Nova Vistoria");
            Console.WriteLine("2 - Exibir Relatório das Vistorias");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
        }

        /// <summary>
        /// Opção 1 do menu: coleta o tipo de veículo, os dados cadastrais,
        /// percorre o checklist específico da subclasse e salva o veículo
        /// na lista central de vistorias.
        /// </summary>
        private static void RealizarNovaVistoria()
        {
            Console.WriteLine();
            Console.WriteLine("---------------------- NOVA VISTORIA -------------------------");
            Console.WriteLine("Selecione o tipo de veículo:");
            Console.WriteLine("1 - Carro");
            Console.WriteLine("2 - Moto");
            Console.WriteLine("3 - Caminhão");
            Console.Write("Opção: ");
            string tipoVeiculo = Console.ReadLine();

            string marca = LerTexto("Marca: ");
            string modelo = LerTexto("Modelo: ");
            int ano = LerInteiro("Ano: ");
            double quilometragem = LerDouble("Quilometragem (km): ");

            Veiculo veiculo;

            if (tipoVeiculo == "1")
            {
                int quantidadePortas = LerInteiro("Quantidade de Portas: ");
                veiculo = new Carro(marca, modelo, ano, quilometragem, quantidadePortas);
            }
            else if (tipoVeiculo == "2")
            {
                int cilindradas = LerInteiro("Cilindradas: ");
                veiculo = new Moto(marca, modelo, ano, quilometragem, cilindradas);
            }
            else if (tipoVeiculo == "3")
            {
                int quantidadeEixos = LerInteiro("Quantidade de Eixos: ");
                double capacidadeCarga = LerDouble("Capacidade de Carga (toneladas): ");
                veiculo = new Caminhao(marca, modelo, ano, quilometragem, quantidadeEixos, capacidadeCarga);
            }
            else
            {
                Console.WriteLine("Tipo de veículo inválido. Vistoria cancelada.");
                return;
            }

            List<string> checklist = veiculo.ObterChecklistObrigatorio();

            Console.WriteLine();
            Console.WriteLine("Checklist de Inspeção (" + checklist.Count + " itens) - informe o status de cada item.");
            Console.WriteLine("Valores aceitos: Bom, Regular ou Ruim.");

            foreach (string nomeItem in checklist)
            {
                string status = LerStatus(nomeItem);
                veiculo.AdicionarItemVistoriado(nomeItem, status);
            }

            vistorias.Add(veiculo);

            Console.WriteLine();
            Console.WriteLine("Vistoria cadastrada com sucesso! Utilize a opção 2 do menu para ver o relatório.");
        }

        /// <summary>
        /// Opção 2 do menu: percorre a lista central de vistorias e exibe o
        /// relatório detalhado de cada veículo avaliado.
        /// </summary>
        private static void ExibirRelatorioDasVistorias()
        {
            if (vistorias.Count == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Nenhuma vistoria realizada até o momento.");
                return;
            }

            for (int i = 0; i < vistorias.Count; i++)
            {
                motor.ExibirRelatorio(vistorias[i], i + 1, vistorias.Count);
            }
        }

        private static string LerTexto(string mensagem)
        {
            string valor = string.Empty;
            bool valido = false;

            while (!valido)
            {
                Console.Write(mensagem);
                valor = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(valor))
                {
                    Console.WriteLine("Valor não pode ser vazio. Tente novamente.");
                }
                else
                {
                    valido = true;
                }
            }

            return valor;
        }

        private static int LerInteiro(string mensagem)
        {
            int valor = 0;
            bool valido = false;

            while (!valido)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out valor))
                {
                    valido = true;
                }
                else
                {
                    Console.WriteLine("Valor inválido. Informe um número inteiro.");
                }
            }

            return valor;
        }

        private static double LerDouble(string mensagem)
        {
            double valor = 0;
            bool valido = false;

            while (!valido)
            {
                Console.Write(mensagem);
                string entrada = Console.ReadLine();
                entrada = entrada != null ? entrada.Replace(",", ".") : entrada;

                if (double.TryParse(entrada, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out valor))
                {
                    valido = true;
                }
                else
                {
                    Console.WriteLine("Valor inválido. Informe um número (use ponto ou vírgula para decimais).");
                }
            }

            return valor;
        }

        private static string LerStatus(string nomeItem)
        {
            string status = string.Empty;
            bool valido = false;

            while (!valido)
            {
                Console.Write("  - " + nomeItem + " [Bom/Regular/Ruim]: ");
                string entrada = Console.ReadLine();

                if (entrada != null)
                {
                    entrada = entrada.Trim();
                }

                if (entrada == "Bom" || entrada == "Regular" || entrada == "Ruim")
                {
                    status = entrada;
                    valido = true;
                }
                else if (entrada != null && entrada.ToLower() == "bom")
                {
                    status = "Bom";
                    valido = true;
                }
                else if (entrada != null && entrada.ToLower() == "regular")
                {
                    status = "Regular";
                    valido = true;
                }
                else if (entrada != null && entrada.ToLower() == "ruim")
                {
                    status = "Ruim";
                    valido = true;
                }
                else
                {
                    Console.WriteLine("    Status inválido. Digite Bom, Regular ou Ruim.");
                }
            }

            return status;
        }
    }
}
