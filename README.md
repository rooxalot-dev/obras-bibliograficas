# Obras Bibliográficas

Aplicação de teste baseada no seguinte repositório:
    [Repositório Base do Projeto](https://github.com/guideti/obras-bibliograficas/blob/master/TESTE_DOTNET.md)

A aplicação SPA foi inicialmente criada em separado e posteriormente integrada ao projeto. Para analise dos passos de desenvolvimento, verificar o seguinte repositório:
    [SPA Front](https://github.com/rooxalot-dev/obras-bibliograficas-front)

## Tecnologias utilizadas na solução
* .Net Core: v2.2
* Angular: v10
* Armazenamento de dados: Postgres
* ORM: Entity Framework Core

## Como executar
* Utilizar um container Postgres para que seja realizada a integração com a aplicação:
    - Durante o desenvolvimento, utilizei a imagem `postgres` da seguinte forma: `docker run --name teste-postgres -e POSTGRES_PASSWORD=teste@123 -p 5432:5432 -d postgres` 

* Dentro do diretório `Api`, no arquivo `appsettings.json`, alterar a connection string para acessar a base de dados com as mesmas variáveis que forem configuradas na inicialização do container do Postgres.

* Dentro do diretório `Api/ClientApp` instalar as dependências utilizando o comando `npm install`.

* Novamente dentro do diretório `Api` executar o comando `dotnet run`. A aplicação executará na seguinte url: `http://localhost:5000/`. A aplicação foi configurada para executar a API e a SPA simultaneamente. Também é possível inicializar a SPA separadamente acessando o diretório `ClientApp` e executando o comando `npm start` para que a aplicação execute na url `http://localhost:4200/`
