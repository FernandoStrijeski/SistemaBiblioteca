# Gerenciamento de Biblioteca API
![version](https://img.shields.io/static/v1?label=version&message=1.0.0&color=blue)
![status](https://img.shields.io/badge/status-em%20avalia%C3%A7%C3%A3o-yellow)
![release-date](https://img.shields.io/badge/release%20date-04--2023-green)
 ![GitHub Org's stars](https://img.shields.io/github/stars/FernandoStrijeskiLinx?style=social)

Trata-se de uma aplicação Dotnet desenvolvida com o objetivo de gerenciar o armazenamento das informações referentes a livros e autores.


# 📄**Documentação da API**
Para um melhor entendimento das funcionalidades existentes na API, utilizamos a interface do swagger.<br>
A aplicação é dividida em 3 seções, definidas para uma melhor organização das ações disponíveis, sendo elas:
1) Autores
   <br>
   Responsável por gerenciar o cadastro dos autores.
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Autores</td>
   <td>Retorna a lista dos autores cadastrados</td>
   </tr>
   <tr>
   <td>POST</td>
   <td>/api/Autores</td>
   <td>Inclui um novo autor no cadastro</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Autores/PorNome/{nome}</td>
   <td>Retorna a lista contendo os autores cadastrados, filtrando pelo nome completo</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Autores/PorParteDoNome/{nome}</td>
   <td>Retorna a lista contendo os autores cadastrados, filtrando por parte do nome</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Autores/{id}</td>
   <td>Busca autor pela identificação (id)</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/Autores/{id}</td>
   <td>Atualiza autor pelo identificador (id)</td>
   </tr>
   <tr>
   <td>DELETE</td>
   <td>/api/Autores/{id}</td>
   <td>Remove autor pelo identificador (id)</td>
   </tr>
   </table>
       
2) Livros
   <br>
   Responsável por gerenciar o cadastro dos livros.
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Livros</td>
   <td>Retorna a lista contendo os livros cadastrados no sistema.</td>
   </tr>
   <tr>
   <td>POST</td>
   <td>/api/Livros</td>
   <td>Inclui um novo livro no sistema.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Livros/PorTitulo/{titulo}</td>
   <td>Retorna a lista contendo os livros cadastrados no sistema, filtrando pelo título.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Livros/PorParteDoTitulo/{titulo}</td>
   <td>Retorna a lista contendo os livros cadastrados no sistema, filtrando por parte do título.</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Livros/{id}</td>
   <td>Busca o cadastro de um determinado livro, a partir do identificador informado.</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/Livros/{id}</td>
   <td>Altera o cadastro de um livro, a partir do identificador fornecido.</td>
   </tr>
   <tr>
   <td>DELETE</td>
   <td>/api/Livros/{id}</td>
   <td>Remove o livro pelo identificador (id)</td>
   </tr>
   </table>
   
3) Usuários
   <br>
   Responsável por gerenciar o cadastro de acesso dos usuários ao sistema
   <table>
   <tr>
   <td>Método</td>
   <td>EndPoint</td>
   <td>Descrição</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Usuarios</td>
   <td>Exibe todos os usuários cadastrados no sistema</td>
   </tr>
   <tr>
   <td>POST</td>
   <td>/api/Usuarios</td>
   <td>Inclui um novo usuário no sistema</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Usuarios/PorNome/{nome}</td>
   <td>Busca um usuário a partir do nome informado</td>
   </tr>
   <tr>
   <td>GET</td>
   <td>/api/Usuarios/{id}</td>
   <td>Busca um usuário pelo identificador</td>
   </tr>
   <tr>
   <td>PUT</td>
   <td>/api/Usuarios/{id}</td>
   <td>Altera o cadastro de um usuário, a partir do identificador fornecido</td>
   </tr>
   <tr>
   <td>DELETE</td>
   <td>/api/Usuarios/{id}</td>
   <td>Remove o usuário pelo identificador (id)</td>
   </tr>
   </table>

# 🗂️**Acesso ao projeto**

Você pode [acessar o código fonte do projeto]([https://github.com/FernandoStrijeskiLinx/SistemaBiblioteca]) ou [baixá-lo](https://github.com/FernandoStrijeskiLinx/SistemaBiblioteca/archive/refs/heads/main.zip).

## Abrir e rodar o projeto

Após baixar o projeto, você pode abrir com o `Visual Studio 2022` ou com o `VS Code`.
<br>
As tecnologias utilizadas são:
* C#
* .Net 8.0
* SQL Server 2019

# ⚙️**Configurações**
Para execução dessa aplicação é necessário criar a base de dados, acessando os arquivos .sql dentro da pasta [https://github.com/FernandoStrijeskiLinx/SistemaBiblioteca/tree/main/0%20-%20Banco%20de%20Dados%20SQL].

### No `VS Code` pode ser necessário instalar o EF: dotnet tool install --global dotnet-ef

# ⚠️**Aviso**
No momento esta API ainda não utiliza o método de autenticação, não necessitando a utilização de tokens para acesso. 


# 📸**Preview**
<img src="https://github.com/FernandoStrijeskiLinx/SistemaBiblioteca/blob/main/1%20-%20Projeto%20API%20.NET/preview.png">

Nos vemos no próximo projeto! 👋✌️
