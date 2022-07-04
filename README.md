# Desafio Luizalabs
------------------------------------------------------------------------------------------------------------------------------------------------------------------------

Este projeto foi desenvolvido para o desafio da Luizalabs. Com o objetivo de desenvolver uma API para o gerenciamento de clientes e seus produtos favoritos.
O projeto foi desenvolvido utilizando .Net 6, com o conceito de DDD (Domain Driven Design).

## 🚀 Começando

Para a execução do projeto existem duas alternativas utilizando o # Dockers ou realizando a instalação dos itens necessários para a execução:

Para realizar a execução via dockers, é necessário realizar a execução dos comandos abaixo?

```
docker-compose build

docker-compose up
```

Após a execução é possível acessar o swagger da aplicação a partir da URL http://localhost:8000/swagger/index.html

A segunda maneira necessitas das seguintes ferramentas instaladas:


🔧 Instalação

* [PostgreSQL](https://www.postgresql.org/download/) - Banco de dados utilizado
* [SDK e Runtime do .Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) - O SDK é usado para criar bibliotecas e aplicativos, o runtime é usado para executar um aplicativo
* [Visual Studio 2022](https://visualstudio.microsoft.com/pt-br/) - IDE

Após a instalação executar dentro do Console de gerenciador de pacotes os seguintes comandos, necessários para a criação do Banco:

```
Add-Migration
update-database
```

### 🔒 Segurança

Após finalizar as instalações é possível realizar login na aplicação, para ter acesso a todos os recursos da API. <br>
A aplicação utiliza de autenticação com token JWT, após realizar o login é gerado um token, com ele é possível realizar autorização pelo próprio swagger clicando em "Authorize" e inserir no campo value o token gerado no seguinte formato: **Bearer + [TOKEN]**:

![image](https://user-images.githubusercontent.com/37752682/177080189-e40d0313-811c-4e65-9f29-5f9bd16a1eab.png)

**Credenciais de acesso:**

```
Usuario: Admin
```

```
Senha: testeLuizaLabs
```

### ⚙️ Executando os testes

A execução dos testes pode ser feita de duas maneiras, pelo próprio Visual Studio ou executando o comando abaixo dentro da pasta do projeto:

```
dotnet test
```

### 🗂️ Estrutura dos dados

Vale ressaltar que foi utilizada a seguinte nomenclatura para as tabelas do banco de dados:

|Nome            |Objetivo                                             |
|----------------|-----------------------------------------------------|
|Customer        |Armazena dados cadastrais de clientes Z              |
|CustomerProduct |Responsável pelo armazenamento da lista de favoritos |

Com isso a aplicação está pronta para ser executada!

## ✒️ Autor

Rafael Oliveira
* [Linkedin](https://www.linkedin.com/in/rafael-oliveira-44b701160) - Perfil do Linkedin

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------

### Tenham um bom dia! ❤️
