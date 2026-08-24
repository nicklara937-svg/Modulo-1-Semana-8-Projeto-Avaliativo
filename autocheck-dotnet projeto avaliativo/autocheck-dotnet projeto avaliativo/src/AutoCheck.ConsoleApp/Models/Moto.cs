using System.Collections.Generic;

namespace AutoCheck.ConsoleApp.Models
{
    /// <summary>
    /// RF03 - Veículo do tipo Moto. Herda de Veiculo e adiciona a
    /// propriedade Cilindradas, além de um checklist próprio.
    /// </summary>
    public class Moto : Veiculo
    {
        public int Cilindradas { get; set; }

        public Moto(string marca, string modelo, int ano, double quilometragem, int cilindradas)
            : base(marca, modelo, ano, quilometragem)
        {
            this.Cilindradas = cilindradas;
        }

        public override List<string> ObterChecklistObrigatorio()
        {
            List<string> checklist = base.ObterChecklistObrigatorio();
            checklist.Add("Kit Transmissão/Corrente");
            checklist.Add("Manetes de Freio/Embreagem");
            checklist.Add("Pezinho Lateral");
            return checklist;
        }

        public override string ObterTipo()
        {
            return "Moto";
        }

        public override string ObterAtributoEspecifico()
        {
            return this.Cilindradas + " Cilindradas";
        }
    }
}
