# CRUD básico de contatos

Esta API foi implementada para fins acadêmicos, como um breve resumo para ajudar àqueles que estão iniciando no assunto de RESTful API.

O intuito deste repositório é explicar conceitos básicos de SQLs, abordagem "Database First", ORM e operações de _CRUD_ (create, read, update, delete) de uma API REST de uma maneira simples e descomplicada.

# Tecnologias utilizadas

Para este exemplo, optei por usar uma [Minimal API](https://learn.microsoft.com/pt-br/aspnet/core/tutorials/min-web-api) em .NET 8 com a linguagem C#.
O banco de dados é [SQLite3](https://www.sqlite.org/) e utilizei o [Entity Framework](https://learn.microsoft.com/pt-br/ef/) como _ORM_ (Object Relational Mapper) para fazer as operações com o banco de dados.

# Links para instalações
- [SDK Dotnet 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
- [SQlite3](https://www.sqlite.org/download.html)

# Rodando a API

Para rodar a API basta apenas rodar o comando `dotnet run` na raiz do projeto.
Ex:
<img width="758" alt="image" src="https://github.com/user-attachments/assets/6e32e769-4ecd-4a85-8a1c-2bb8b3ce0143">

# Exemplo de payload

POST - `/contacts`
```json
{
  "name": "Jhon Doe",
  "email": "jhon.doe@email.com",
  "phone": "1234567891011"
}
```

# Boa práticas de APIs REST

1. Endpoints descritivos, no plural e kebab-case
  1. `/contacts/1`
  2. `/users`
  3. `/user-groups`
2. Usar rotas apropriadas para cada ação, POST, PUT, PATCH, GET, DELETE.
3. Receber requisições e devolver respostas em `json`
4. Devolver status code de acordo com o contexto (ex: 201 para created, 204 para delete, 200 para GET e PUT, 400 para falha de validação, etc.).