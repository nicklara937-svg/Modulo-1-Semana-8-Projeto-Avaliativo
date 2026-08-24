using System.Collections.Generic;
using System.Globalization;

namespace AutoCheck.ConsoleApp.Models
{
    /// <summary>
    /// RF03 - Veículo do tipo Caminhao. Herda de Veiculo e adiciona as
    /// propriedades QuantidadeEixos e CapacidadeCargaToneladas, além de
    /// um checklist próprio, mais pesado.
    /// </summary>
    public class Caminhao : Veiculo
    {
        public int QuantidadeEixos { get; set; }
        public double CapacidadeCargaToneladas { get; set; }

        public Caminhao(string marca, string modelo, int ano, double quilometragem, int quantidadeEixos, double capacidadeCargaToneladas)
            : base(marca, modelo, ano, quilometragem)
        {
            this.QuantidadeEixos = quantidadeEixos;
            this.CapacidadeCargaToneladas = capacidadeCargaToneladas;
        }

        public override List<string> ObterChecklistObrigatorio()
        {
            List<string> checklist = base.ObterChecklistObrigatorio();
            checklist.Add("Tacógrafo");
            checklist.Add("Sistema de Freios a Ar");
            checklist.Add("Trava e Lona da Caçamba");
            return checklist;
        }

        public override string ObterTipo()
        {
            return "Caminhão";
        }

        public override string ObterAtributoEspecifico()
        {
            string carga = this.CapacidadeCargaToneladas.ToString("0.0", CultureInfo.GetCultureInfo("pt-BR"));
            return this.QuantidadeEixos + " Eixos | Cap. Carga: " + carga + " Toneladas";
        }
    }
}
