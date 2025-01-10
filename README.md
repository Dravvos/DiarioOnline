# Diário Online- ASP.NET Core API e Razor Pages
Este projeto é uma aplicação de Diário Online, desenvolvida utilizando **ASP.NET Core API** com **.NET 8** e **Razor Pages**. A aplicação permite que o usuário registre, visualize, edite e exclua suas entradas de diário de forma simples e segura. Ele utiliza o modelo arquitetural de API para comunicação entre o back-end e a interface de usuário.

## Tecnologias Utilzadas
* **ASP.NET Core API (.NET 8)**: Framework para desenvolvimento da API que lida com as operações de CRUD (Criar, Ler, Atualizar e Excluir) de entradas de diário.
* **Razor Pages**: Uma abordagem baseada em páginas dinâmicas para o front-end da aplicação, que permite interagir com o usuário de maneira intuitiva.
* **Entity Framework Core**: Utilizado para interação com o banco de dados, realizando mapeamento objeto-relacional.
* **SQL Server**: Banco de dados utilizado para armazenar as entradas do diário.
* **JWT (JSON Web Tokens)**: Para autenticação segura dos usuários.

## Funcionalidades
* **Autenticação de Usuário**: Cadastro, login e gerenciamento de sessão de usuários através de tokens JWT.
* **CRUD de Entradas de Diário**: O usuário pode criar, visualizar, editar e excluir suas entradas de diário.
* **Segurança**: Proteção contra acesso não autorizado às entradas de diário de outros usuários.

## Pré-requisitos
Antes de executar o projeto, certifique-se de que você tem os seguintes pré-requisitos instalados:

* **.NET 8 SDK**: Para executar e compilar o projeto.
* **SQL Server**.
* **Visual Studio** ou **Visual Studio Code**: Para desenvolvimento e depuração.

## Como Executar o Projeto

### 1. Clonar o repositório
```
git clone https://github.com/Dravvos/DiarioOnline.git
```

### 2. Configurar o Banco de Dados
Crie uma variável de ambiente com o nome DiarioConnection

### 3. Criar o Banco de Dados
```
dotnet ef migrations add DiarioDB
dotnet ef database update
```

### 4. Executar a Aplicação
```
dotnet run
```
