using System.Collections.Generic;

namespace AutoCheck.ConsoleApp.Models
{
    /// <summary>
    /// RF03 - Veículo do tipo Carro. Herda de Veiculo e adiciona a
    /// propriedade QuantidadePortas, além de um checklist próprio.
    /// </summary>
    public class Carro : Veiculo
    {
        public int QuantidadePortas { get; set; }

        public Carro(string marca, string modelo, int ano, double quilometragem, int quantidadePortas)
            : base(marca, modelo, ano, quilometragem)
        {
            this.QuantidadePortas = quantidadePortas;
        }

        public override List<string> ObterChecklistObrigatorio()
        {
            List<string> checklist = base.ObterChecklistObrigatorio();
            checklist.Add("Estepe e Macaco");
            checklist.Add("Triângulo de Sinalização");
            checklist.Add("Ar Condicionado Funcional");
            return checklist;
        }

        public override string ObterTipo()
        {
            return "Carro";
        }

        public override string ObterAtributoEspecifico()
        {
            return this.QuantidadePortas + " Portas";
        }
    }
}
