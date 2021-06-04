# Desafio programação para vaga desenvolvedor .Net _bycoders - Resolução

Para a resolução do desafio, utilizei as seguintes tecnologias:
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server Express LocalDB
- xunit e Moq para os testes automatizados

Versão do Framework: 5.0  
Versão do Visual Studio: Enterprise 2019.

# Instruções para setup inicial da aplicação

1. Abrir a solution CNABParser.sln com o Visual Studio 2019. A versão Community pode ser utilizada.
2. Abrir o arquivo appsettings.json do projeto CNABParser.MVC e adaptar a string de conexão "DefaultConnection" para apontar para um servidor SQL válido. Pode ser tanto um localDB quanto a versão full.
3. O nome do banco de dados ("TesteBycoders") deve ser mantido. Como utilizei a abordagem "Code First", o Entity Framework ficará responsável por criar e popular o DB e as tabelas envolvidas no projeto.
4. Selecionar o projeto CNABParser.MVC como default project.
5. Rodar a aplicação.