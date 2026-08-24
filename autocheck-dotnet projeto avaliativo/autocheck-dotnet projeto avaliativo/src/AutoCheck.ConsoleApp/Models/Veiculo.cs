using System.Collections.Generic;

namespace AutoCheck.ConsoleApp.Models
{
    /// <summary>
    /// RF02 - Classe base abstrata de todo veículo vistoriado.
    /// Concentra os dados cadastrais comuns e a lista de itens inspecionados.
    /// Não pode ser instanciada diretamente: cada vistoria é sempre feita a
    /// partir de uma subclasse concreta (Carro, Moto ou Caminhao).
    /// </summary>
    public abstract class Veiculo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Quilometragem { get; set; }
        public List<ItemVistoria> VistoriaRealizada { get; set; }

        public Veiculo(string marca, string modelo, int ano, double quilometragem)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Quilometragem = quilometragem;
            this.VistoriaRealizada = new List<ItemVistoria>();
        }

        /// <summary>
        /// Adiciona um item já avaliado (nome + status) à vistoria do veículo.
        /// </summary>
        public void AdicionarItemVistoriado(string nome, string status)
        {
            ItemVistoria item = new ItemVistoria(nome, status);
            this.VistoriaRealizada.Add(item);
        }

        /// <summary>
        /// Checklist genérico, comum a qualquer tipo de veículo.
        /// Cada subclasse sobrescreve este método (polimorfismo) para
        /// acrescentar os itens específicos da sua categoria.
        /// </summary>
        public virtual List<string> ObterChecklistObrigatorio()
        {
            List<string> checklist = new List<string>();
            checklist.Add("Nível de Óleo do Motor");
            checklist.Add("Bateria e Sistema Elétrico");
            checklist.Add("Documentação Regularizada");
            return checklist;
        }

        /// <summary>
        /// Nome do tipo de veículo (usado apenas para exibição no relatório).
        /// </summary>
        public abstract string ObterTipo();

        /// <summary>
        /// Texto com o(s) atributo(s) específico(s) da subclasse
        /// (ex.: "4 Portas", "125 Cilindradas"), usado apenas no relatório.
        /// </summary>
        public abstract string ObterAtributoEspecifico();
    }
}
