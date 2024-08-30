# GestaoColaboradoresBackend

Este é um projeto backend desenvolvido em ASP.NET Core para gerenciar colaboradores e seus registros de frequência. O sistema permite adicionar, modificar, listar e remover colaboradores, bem como gerar relatórios de frequência mensal. O backend foi projetado para funcionar com uma API RESTful e pode ser integrado com um frontend desenvolvido em Angular.

## Arquitetura Utilizada

O projeto segue uma **Arquitetura de Camadas** (Layered Architecture), que é uma das abordagens mais comuns para estruturar aplicações. Esta arquitetura facilita a separação de responsabilidades, manutenção e escalabilidade do sistema.

### Componentes Principais da Arquitetura:

1. **Camada de Apresentação (Presentation Layer):**
   - Contém os controladores (Controllers) que expõem os endpoints da API.
   - Recebe as solicitações HTTP e as encaminha para a camada de aplicação.
   - Utiliza o ASP.NET Core para definir os controladores e os endpoints RESTful.

2. **Camada de Aplicação (Application Layer):**
   - Contém a lógica de aplicação e coordena o fluxo entre as camadas de apresentação e de dados.
   - Inclui os serviços que lidam com operações de negócios específicas, como adicionar, atualizar ou remover colaboradores e registros de frequência.
   - Implementa a lógica de negócios necessária antes de interagir com a camada de dados.

3. **Camada de Dados (Data Layer):**
   - Responsável pelo acesso aos dados e pela comunicação com o banco de dados.
   - Utiliza o Entity Framework Core como ORM (Object-Relational Mapping) para realizar operações de banco de dados.
   - Inclui o contexto do banco de dados e as entidades (models) que representam as tabelas do banco de dados.

4. **Camada de Infraestrutura (Infrastructure Layer):**
   - Contém a configuração do ambiente, bibliotecas de terceiros e outras dependências.
   - Lida com aspectos de infraestrutura, como configurações de banco de dados e logging.
   - Inclui a configuração do Docker e outros scripts de deployment.

5. **Camada de Testes (Test Layer):**
   - Contém testes automatizados para garantir a qualidade e a funcionalidade do código.
   - Inclui testes de unidade para os serviços e controladores, utilizando frameworks de teste como xUnit.

### Vantagens da Arquitetura Utilizada:

- **Manutenção e Extensibilidade:** A separação de responsabilidades facilita a manutenção e a adição de novas funcionalidades ao sistema.
- **Testabilidade:** As camadas de aplicação e dados são bem definidas, o que facilita a criação de testes unitários.
- **Reutilização de Código:** As funcionalidades podem ser reutilizadas em diferentes partes da aplicação ou em outros projetos.
- **Isolamento de Camadas:** Mudanças em uma camada não afetam as outras camadas, desde que a interface da camada seja mantida.

## Funcionalidades

### Colaboradores:

- Listar todos os colaboradores.
- Buscar colaborador por ID.
- Adicionar um novo colaborador.
- Atualizar informações de um colaborador existente.
- Deletar um colaborador.
- Gerar relatórios personalizados de colaboradores por mês e ano.

### Registros de Frequência:

- Listar todos os registros de frequência.
- Buscar registros de frequência por ID.
- Criar novos registros de frequência.
- Filtrar registros de frequência por mês e ano.
- Exportar registros de frequência para um arquivo Excel.

## Tecnologias Utilizadas

- .NET 8.0
- Entity Framework Core 8.0.8 - ORM para acesso ao banco de dados SQL Server.
- EPPlus - Biblioteca para manipulação de arquivos Excel.
- Swashbuckle.AspNetCore - Para documentação de API utilizando Swagger.
- Docker - Para execução e deployment em containers Linux.
- DotNetEnv - Para ler variáveis de ambiente de um arquivo .env.

## Requisitos

- .NET SDK 8.0 ou superior
- SQL Server
- Docker (opcional, para execução em containers)

## Como Executar o Projeto

### Clonar o Repositório:

```bash
git clone https://github.com/seuusuario/GestaoColaboradoresBackend.git
cd gestao-colaboradores-backend/GestaoColaboradoresBackend
```

### Configurar o Banco de Dados:

Atualize a string de conexão no arquivo `appsettings.json` com as informações do seu banco de dados SQL Server ou coloque as variáveis de ambiente em um arquivo `.env`.

**Arquivo .env:**

```
DB_SERVER={endereço do servidor}
DB_DATABASE={nome do banco de dados}
DB_USER={nome do usuário}
DB_PASSWORD={senha do usuário}
```

### Aplicar Migrações e Atualizar o Banco de Dados:

Utilize o comando abaixo para aplicar migrações e atualizar o banco de dados:

```bash
dotnet ef database update
```

### Executar o Projeto:

Execute o projeto utilizando o comando:

```bash
dotnet run
```

A API estará disponível em http://localhost:8080 por padrão.

### Acessar a Documentação da API:

Acesse http://localhost:8080/swagger no seu navegador para visualizar a documentação interativa da API gerada pelo Swagger.

## Endpoints Principais

### Colaboradores

- `GET /api/Collaborators` - Lista todos os colaboradores.
- `GET /api/Collaborators/{id}` - Retorna o colaborador pelo ID.
- `POST /api/Collaborators` - Cria um novo colaborador.
- `PUT /api/Collaborators/{id}` - Atualiza um colaborador existente.
- `DELETE /api/Collaborators/{id}` - Deleta um colaborador pelo ID.
- `GET /api/Collaborators/report` - Gera um relatório de colaboradores baseado no mês e ano.

### Frequências

- `GET /api/Attendances` - Lista todos os registros de frequência.
- `GET /api/Attendances/{id}` - Retorna um registro de frequência pelo ID.
- `POST /api/Attendances` - Cria um novo registro de frequência.
- `GET /api/Attendances/filter` - Filtra registros de frequência por mês e ano.
- `GET /api/Attendances/export` - Exporta registros de frequência para um arquivo Excel.

## Docker

Para executar o projeto utilizando Docker, certifique-se de ter o Docker instalado e siga os passos abaixo:

### Construir a Imagem Docker:

```bash
docker build -t GestaoColaboradoresBackend .
```

### Executar o Container:

```bash
docker run -d -p 8080:80 GestaoColaboradoresBackend
```

A API estará disponível em http://localhost:8080.

## Testes Automatizados

Este projeto inclui testes de unidade para garantir a qualidade e a funcionalidade do código. Os testes de unidade foram escritos usando [xUnit](https://xunit.net/), um framework de teste popular para .NET.

### Funcionalidades Testadas

Os testes de unidade cobrem as seguintes funcionalidades do backend:

- **Colaboradores**
  - Listar todos os colaboradores.
  - Buscar colaborador por ID.
  - Adicionar um novo colaborador.
  - Atualizar um colaborador existente.
  - Deletar um colaborador.
  - Gerar relatórios de colaboradores por mês e ano.
  
- **Registros de Frequência**
  - Listar todos os registros de frequência.
  - Buscar registros de frequência por ID.
  - Criar novos registros de frequência.
  - Filtrar registros de frequência por mês e ano.
  - Exportar registros de frequência para um arquivo Excel.

### Configuração do Ambiente de Testes

Para configurar o ambiente de testes, siga as instruções abaixo:

1. **Instale as Dependências de Teste:**

   Certifique-se de que o projeto de testes tem todas as dependências necessárias. O projeto de testes deve referenciar o projeto principal `GestaoColaboradoresBackend` e bibliotecas como `xUnit` e `Moq`.

2. **Banco de Dados em Memória:**

   Os testes utilizam um banco de dados em memória para garantir que os testes sejam isolados e não afetem o banco de dados de produção. O banco de dados em memória é configurado usando `Microsoft.EntityFrameworkCore.InMemory`.

### Executando os Testes

Para executar os testes de unidade, siga os passos abaixo:

1. **Navegue até o Diretório do Projeto de Testes:**

   ```bash
   cd GestaoColaboradoresBackend.Tests
   ```

2. **Execute os Testes Usando o .NET CLI:**

   ```bash
   dotnet test
   ```

   Isso executará todos os testes no projeto e fornecerá um resumo dos resultados dos testes.

3. **Ou Execute os Testes Usando o Visual Studio:**

   - Abra a solução do projeto no Visual Studio.
   - Navegue até o **Test Explorer**.
   - Clique em **Run All** para executar todos os testes.

### Observações Finais

Sinta-se à vontade para contribuir para este projeto. Para fazer isso, você pode:

Abrir uma issue para relatar bugs ou sugerir melhorias.
Enviar um pull request com novas funcionalidades ou correções.
