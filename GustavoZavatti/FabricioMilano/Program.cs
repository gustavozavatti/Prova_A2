using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapPost("/api/folha/cadastrar", ([FromBody] Funcionario funcioario, [FromServices] AppDataContext ctx) => {

    if(funcioario.Mes < 1 && funcioario.Mes > 12)
        return Results.BadRequest("Mês inválido!");

    if(funcioario.Ano < 2000)
       return Results.BadRequest("Ano inválido!");

    if (ctx.Funcionarios.FirstOrDefault(funcioariosCadastrados => funcioariosCadastrados.CPF == funcioario.CPF && funcioariosCadastrados.Ano == funcioario.Ano && funcioariosCadastrados.Mes == funcioario.Mes) != null)
        return Results.Conflict("Esse cadastro já existe");

    funcioario.salarioBruto = funcioario.horasTrabalhadas * funcioario.valorHora;

    if(funcioario.salarioBruto <= 1903.98){
        funcioario.impostoRenda = 0.0;
    } else if(funcioario.salarioBruto >= 1903.99 && funcioario.salarioBruto <= 2826.65){
        funcioario.impostoRenda = funcioario.salarioBruto * 0.075 - 142.80;
    } else if(funcioario.salarioBruto >= 2826.66  && funcioario.salarioBruto <= 3751.05){
        funcioario.impostoRenda = funcioario.salarioBruto * 0.150 - 354.80;
    } else if(funcioario.salarioBruto >= 3751.06  && funcioario.salarioBruto <= 4664.68){
        funcioario.impostoRenda = funcioario.salarioBruto * 0.225 - 636.13;
    } else if(funcioario.salarioBruto >= 4664.69){
        funcioario.impostoRenda = funcioario.salarioBruto * 0.275 - 869.36; 
    }

    if(funcioario.salarioBruto <= 1693.72 )
        funcioario.inss = funcioario.salarioBruto* 0.08;
    
    if(funcioario.salarioBruto >=1693.73   && funcioario.salarioBruto <=2822.90)
        funcioario.inss = funcioario.salarioBruto * 0.09;

    if(funcioario.salarioBruto >=2822.91  && funcioario.salarioBruto <=5645.80)
        funcioario.inss = funcioario.salarioBruto * 0.11;
    
    if(funcioario.salarioBruto >= 5645.81 )
        funcioario.inss = 621.03;

    funcioario.fgts = funcioario.salarioBruto* 0.08;

    funcioario.salarioLiquido = funcioario.salarioBruto - funcioario.impostoRenda - funcioario.inss;

    ctx.Funcionarios.Add(funcioario);
    ctx.SaveChanges();
    return Results.Created("", funcioario);     
});

app.MapGet("/api/folha/listar", ([FromServices] AppDataContext ctx) => {

    if (ctx.Funcionarios.Count() == 0)
    {
        return Results.NotFound("Não existem funcionários cadastrados");
    }
    return Results.Ok(ctx.Funcionarios.ToList());
});

app.MapGet("/api/folha/buscar/{cpf}/{mes}/{ano}", ([FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano, [FromServices] AppDataContext ctx) => {

    Funcionario? f = ctx.Funcionarios.FirstOrDefault(f => f.CPF == cpf && f.Mes == mes && f.Ano == ano);

    if (f != null)
    {
        return Results.Ok(f);
    }

    return Results.NotFound("Funcionário não encontrado");
});

app.MapDelete("/api/folha/remover/{cpf}/{mes}/{ano}", ([FromRoute] string cpf, [FromRoute] int mes, [FromRoute] int ano, [FromServices] AppDataContext ctx) => {
    Funcionario? f = ctx.Funcionarios.FirstOrDefault(f => f.CPF == cpf && f.Mes == mes && f.Ano == ano);

    if (f != null)
    {
        ctx.Funcionarios.Remove(f);
        ctx.SaveChanges();
        return Results.Ok("Funcionário deletado com sucesso!");
    }

    return Results.NotFound("Funcionário não encontrado");
});

app.MapGet("/api/folha/total-liquido", ([FromServices] AppDataContext ctx) => {

    if (ctx.Funcionarios.Count() == 0)
    {
        return Results.NotFound("Não existem funcionários cadastrados");
    }

    double salarioTotalLiquido = 0.0;
    var qualquer = ctx.Funcionarios.ToList();

     foreach (Funcionario i in qualquer)
    {
       salarioTotalLiquido += i.salarioLiquido;
    }

    return Results.Ok(salarioTotalLiquido);
});

app.Run();