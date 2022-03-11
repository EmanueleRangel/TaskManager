# TaskManager REST API 

## Sobre o projeto

Web API de gerenciador de tarefas 

Projeto baseado no tutorial de João Paulo S. Araújo de como criar Web API usando .NET, C# e Visual Studio.      
Além de fazer conexão com **Banco de Dados NoSQL (MongoDB)**, implementei também a conexão **Banco de Dados Relacional (SQLServer, ORM: Dapper)**.   
A utilização de ambos é possível pois o código da aplicação está baseada nos princípios do **SOLID**, nesse caso, o Princípio da Inversão de Dependência.    

Também implementei dois diferentes métodos de segurança:

- JWT
- ApiKey Authentication

Na branch "nova-arquitetura-controller" encapsulei a implementação dos endpoints nas classes de Services para que o Princípio de Responsabilidade Única do SOLID fosse respeitado.
Nela, também a implementação dos testes unitários.

## Meus próximos passos:
- Revisar código para melhorias relacionadas a boas práticas;
- Implementar Health Checks;
- 
## Tecnologias utilizadas:

- C#
- .NET 6.0
- VisualStudio
- MongoDB
- SQLServer
- ORM Dapper

Link do tutorial: https://www.youtube.com/watch?v=9vbT-nF_JM8&t=5914s.
