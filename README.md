# AutoCheck.NET — Motor de Vistoria Veicular

Mini-Projeto Avaliativo — Módulo 01, Semana 08 — Desenvolvedor Back-End [.NET] T1

## 1. O que o sistema faz

O **AutoCheck.NET** é uma aplicação de console em C# que simula o motor de
processamento de vistorias técnicas usado por uma rede de concessionárias.
Ela permite cadastrar a inspeção de três tipos de veículo — **Carro**,
**Moto** e **Caminhão** — cada um com seu próprio checklist obrigatório de
itens (ex.: estepe e triângulo no carro, tacógrafo e freios a ar no
caminhão). Para cada item o técnico informa o status (`Bom`, `Regular` ou
`Ruim`), e o sistema:

- calcula a pontuação obtida e o percentual de aprovação do veículo;
- classifica o veículo em **Aprovado com Excelência**, **Aprovado com
  Apontamentos** ou **Reprovado na Vistoria**;
- separa os itens críticos (Ruim) dos itens de atenção (Regular);
- gera uma recomendação de serviços que a oficina deve executar.

## 2. Como executar

Pré-requisitos: [.NET SDK 8.0](https://dotnet.microsoft.com/download) instalado.

```bash
# 1. Clonar o repositório
git clone <https://github.com/nicklara937-svg/Modulo-1-Semana-8-Projeto-Avaliativo.git>
cd autocheck-dotnet projeto avaliativo

# 2. Entrar na pasta do projeto de console
cd src/AutoCheck.ConsoleApp

# 3. Restaurar dependências e rodar
dotnet restore
dotnet run
```

Ao iniciar, o programa exibe o menu principal:

```
1 - Realizar Nova Vistoria
2 - Exibir Relatório das Vistorias
0 - Sair
```

- **Opção 1**: escolha o tipo de veículo, informe os dados cadastrais
  (marca, modelo, ano, quilometragem e o atributo específico do tipo) e,
  em seguida, responda `Bom`, `Regular` ou `Ruim` para cada item do
  checklist que aparecer na tela.
- **Opção 2**: exibe o relatório completo de todas as vistorias já
  cadastradas na sessão atual (ou avisa que nenhuma vistoria foi feita).
- **Opção 0**: encerra a aplicação.

## 3. Estrutura do projeto

```
autocheck-dotnet/
├── src/
│   └── AutoCheck.ConsoleApp/
│       ├── Program.cs                 # Menu principal e coleta de dados (RF11)
│       ├── Models/
│       │   ├── ItemVistoria.cs        # RF01
│       │   ├── Veiculo.cs             # RF02 - classe base abstrata
│       │   ├── Carro.cs               # RF03
│       │   ├── Moto.cs                # RF03
│       │   └── Caminhao.cs            # RF03
│       ├── Services/
│       │   └── MotorVistoria.cs       # RF04 a RF10 - regras de negócio
│       └── AutoCheck.ConsoleApp.csproj
└── README.md
```

## 4. Regras de negócio

**Pontuação por item (RF04):** `Bom` = 10 pontos, `Regular` = 5 pontos,
`Ruim` = 0 pontos.

**Percentual de aprovação (RF05):**

```
Percentual (%) = (PontuaçãoObtida / PontuaçãoMáximaPossível) × 100
```

onde `PontuaçãoMáximaPossível = TotalDeItens × 10`. O cálculo é feito com
cast explícito para `double` antes da divisão, para não truncar o
resultado como aconteceria em uma divisão inteira em C#.

**Classificação final (RF06):**

| Percentual | Classificação | Ação |
|---|---|---|
| 90% a 100% | Aprovado com Excelência | Liberado para compra/revenda imediata |
| 60% a 89% | Aprovado com Apontamentos | Exige desconto para reparos da oficina |
| 0% a 59% | Reprovado na Vistoria | Veículo recusado pela concessionária |

**Recomendação de serviços (RF07/RF08):** todo item marcado como `Ruim`
entra na lista de **itens críticos** (ação imediata) e todo item marcado
como `Regular` entra na lista de **itens de atenção** (revisão
preventiva). Para cada item nessas listas o sistema sugere o serviço de
oficina correspondente (ex.: "Triângulo de Sinalização" → repor
equipamento obrigatório).

## 5. Conceitos do Módulo 01 aplicados e onde

| Conceito | Onde foi aplicado |
|---|---|
| Tipos primitivos (`string`, `int`, `double`, `bool`) | Propriedades de `ItemVistoria` e `Veiculo`, variáveis de controle do menu |
| Listas (`List<T>`) | `Veiculo.VistoriaRealizada`, lista central `vistorias` em `Program.cs` |
| Laços tradicionais (`for` / `foreach`) | Toda a varredura de itens em `MotorVistoria` (soma de pontos, filtragem de críticos/atenção) — sem uso de LINQ, conforme RF09 |
| Condicionais (`if`/`else`, `switch`) | Mapeamento de status → pontos, faixas de classificação, opções do menu |
| Classes e Objetos | `ItemVistoria`, `Veiculo`, `Carro`, `Moto`, `Caminhao`, `MotorVistoria` |
| Construtores explícitos com `this` | Construtores de `Veiculo` e das subclasses |
| Encapsulamento | Propriedades com `{ get; set; }`, validação de `Status` em `ItemVistoria` |
| Herança (`:`) | `Carro`, `Moto` e `Caminhao` herdam de `Veiculo` |
| Polimorfismo (`virtual`/`override`) | `ObterChecklistObrigatorio()` sobrescrito em cada subclasse |
| Git/GitHub | Histórico de commits descritivos ao longo do desenvolvimento |

## 6. Arquitetura cliente-servidor

Este projeto é uma aplicação de console local, então não existe uma
separação física entre cliente e servidor. Ainda assim, a ideia da
arquitetura cliente-servidor aparece na **separação de responsabilidades**
do código: `Program.cs` funciona como a camada de interação com o usuário
("cliente" da lógica de negócio, responsável só por ler entradas e exibir
saídas), enquanto `MotorVistoria` (em `Services/`) concentra as regras de
negócio e os cálculos ("servidor" da lógica), sendo consultado pelo
`Program.cs` sempre que um resultado precisa ser calculado ou exibido.
Essa separação facilita, no futuro, expor a mesma lógica de
`MotorVistoria` por trás de uma API web sem precisar reescrever as regras.

## 7. Uso de Inteligência Artificial

Ferramentas de IA foram utilizadas como apoio ao estudo e à estruturação
inicial do código (organização de classes, exemplos de sintaxe). Todo o
código foi lido, compreendido e testado manualmente antes da entrega.

