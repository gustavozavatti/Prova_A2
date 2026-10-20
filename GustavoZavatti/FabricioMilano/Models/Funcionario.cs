public class Funcionario
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? Nome {get; set; }
    public string? CPF { get; set; }
    public int Mes {get; set; }
    public int Ano {get; set; }
    public int horasTrabalhadas {get; set; }
    public double valorHora {get; set; }
    public double salarioBruto {get; set;}
    public double impostoRenda {get; set; }
    public double inss {get; set;}
    public double fgts {get; set;}
    public double salarioLiquido {get; set;}

}