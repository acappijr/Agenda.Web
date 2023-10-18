# Agenda.Web

## 🔵 Instalação do projeto
Esse projeto requer uma máquina com Visual Studio (testedo com a versão 2022) instalado e Docker Desktop (testado com a versão 4).  
Deve-se clonar o projeto localmente, abrir com o Visual Studio, caso ainda não esteja, setar o proejto `docker-compose` como projeto de inicialização padrão e clicar no **▶️ Docker Compose**.  
Caso não se tenha o VS2022 pode-se instalar o projeto com Docker Engine e o pacote compose. Para isso deve-se acessar a pasta onde se encontra o arquivo `docker-compose.yml` e executar o comando `docker compose up -d`.

## 🔵 Requisitos cumpridos
✔️ Aplicação MVC de Agenda de Contatos.  
✔️ Consulta de Cep em serviço externo.  
✔️ Testes unitários.

## 🔵 Decisões arquiteturais
A solução foi baseada em arquitetura limpa porém como há poucos requisitos e baixa complexidade não foi feito o uso de um **domínio rico**.  
Foi utilizada a injeção de dependência para todos os serviços e repositórios usando apropriado tempo de vida.  
Foi usado o padrão domain repository, expondo apenas os métodos necessários.  
Foi feita simples validação usando data annotations e o mapeamento foi feito usando method factories dentro dos DTOs, não necessitando pacotes externos.
Foram utilizados os recursos de `.editorconfig` para padronizar o projeto para todos que desenvolvam nele, e o `Directory.Build.props` instala o Sonar para todos os projetos da solução automaticamente além de apontar **Warnings** como **Errors** durante a compilação, exigindo melhor código.

---
## 🔵 Features

### 🔸 Banco de dados
Foi escolhida a versão 2019 do MSSQL e a migração inicial é feita dentro do próprio compose. Um usuário de aplicação é setado e dado as permissões para que não se precise usar o usuário SA do banco de dados.

### 🔸 Consulta de Cep
A consulta de cep é feita em um serviço externo localizado https://apicep.com/. A chamada é feita pelo backend e tem um HttpClient tipado para o seu serviço `CepExternalService.cs` e é gerenciado pela factory do próprio container de injeção de dependência do .NET.  
A chamada desse serviço parte do front com um script Ajax que preenche os dados na tela para serem usados depois.

### 🔸 Cadastro
O cadastro foi feito pensando em fazer o mínimo de chamadas possíveis no banco e ainda ter a melhor performance. A **primary key** do Cep é do tipo Uniqueidentifier, podendo ser setada no backend e não necessitando salvar e depois resgatar o Id para usar como foreign key no contato.  
Porém a tabela de Cep conta com um índice do tipo cluster baseado no código do Cep, que torna a busca por Cep muito rápida.  
Ceps são apenas cadastrados e não removidos da base. Uma feature que pode ser implementada é caso a consulta ao serviço de Cep seja paga, pode-se buscar internamente antes de Consultar o serviço externo.

### 🔸 Exclusão
A exclusão é feita com a abertura de um modal e chamada Ajax.

### 🔸 Testes
Os testes se concentraram em testes unitários e foram feitos em cima do core da aplicação que é o `ContatoService`. Garantindo que são feitos apenas os cadastros necessários e grantindo a integridade das regras que mantém a foreign key de cep íntegra com o contato