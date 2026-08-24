using System;

namespace AutoCheck.ConsoleApp.Models
{
    /// <summary>
    /// RF01 - Representa um item avaliado durante a vistoria de um veículo.
    /// Cada item possui um nome (ex: "Nível de Óleo do Motor") e um status,
    /// que só pode assumir os valores "Bom", "Regular" ou "Ruim".
    /// </summary>
    public class ItemVistoria
    {
        private string status = "Regular";

        public string Nome { get; set; }

        public string Status
        {
            get { return status; }
            set
            {
                if (value != "Bom" && value != "Regular" && value != "Ruim")
                {
                    throw new ArgumentException("O status do item deve ser 'Bom', 'Regular' ou 'Ruim'.");
                }

                status = value;
            }
        }

        public ItemVistoria(string nome, string status)
        {
            this.Nome = nome;
            this.Status = status;
        }

        /// <summary>
        /// RF04 - Converte o status textual do item em pontuação numérica.
        /// Bom = 10 pontos | Regular = 5 pontos | Ruim = 0 pontos.
        /// </summary>
        public int ObterPontuacao()
        {
            int pontos;

            switch (Status)
            {
                case "Bom":
                    pontos = 10;
                    break;
                case "Regular":
                    pontos = 5;
                    break;
                case "Ruim":
                    pontos = 0;
                    break;
                default:
                    pontos = 0;
                    break;
            }

            return pontos;
        }
    }
}
