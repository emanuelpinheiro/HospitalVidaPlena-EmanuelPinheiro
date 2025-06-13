# Hospital Vida Plena

Sistema de gestão hospitalar desenvolvido em ASP.NET Core (.NET 8) com Razor Pages e Entity Framework Core.

## Instruções de Execução

1. **Pré-requisitos**
   - .NET 8 SDK instalado
   - SQL Server (ou LocalDB) disponível

2. **Configuração do Banco de Dados**
   - Verifique a string de conexão no arquivo `appsettings.json`:
     ```json
     "ConnectionStrings": {
       "HospisimContext": "Server=(localdb)\\mssqllocaldb;Database=HospisimDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```
   - Ajuste conforme necessário para seu ambiente.

3. **Aplicando as Migrations**
   - No terminal, execute:
     ```sh
     dotnet ef database update
     ```
   - Isso criará e populá o banco de dados com dados iniciais.

4. **Executando a Aplicação**
   - No terminal, execute:
     ```sh
     dotnet run
     ```
   - Acesse a aplicação em `https://localhost:5001` ou `http://localhost:5000`.

---

## Relacionamentos das Entidades

O sistema possui as seguintes entidades principais e relacionamentos:

- **Paciente**
  - Possui vários **Prontuários**
  - Possui várias **Internações**

- **Prontuário**
  - Pertence a um **Paciente**
  - Possui vários **Atendimentos**

- **Atendimento**
  - Pertence a um **Prontuário**
  - Pertence a um **Profissional**
  - Pode ter uma **Internação**
  - Possui várias **Prescrições**
  - Possui vários **Exames**

- **Profissional**
  - Pertence a uma **Especialidade**
  - Possui vários **Atendimentos**
  - Possui várias **Prescrições**

- **Prescrição**
  - Pertence a um **Atendimento**
  - Pertence a um **Profissional**

- **Exame**
  - Pertence a um **Atendimento**

- **Internação**
  - Pertence a um **Paciente**
  - Pertence a um **Atendimento**
  - Pode ter uma **Alta Hospitalar**

- **Alta Hospitalar**
  - Pertence a uma **Internação**

- **Especialidade**
  - Possui vários **Profissionais**

### Texto Explicativo do Diagrama
No sistema Hospital Vida Plena, um Paciente pode ter vários Prontuários e várias Internações. Cada Prontuário pertence a um único paciente e pode conter vários Atendimentos. Cada Atendimento está vinculado a um Profissional e a um prontuário, podendo ter prescrições e exames associados. Uma Internação está ligada tanto ao paciente quanto a um atendimento específico, e pode gerar uma Alta Hospitalar. Assim, o modelo reflete o fluxo real de atendimento hospitalar, garantindo rastreabilidade e integridade das informações clínicas.
---

## Observações

- O sistema já inclui dados iniciais para facilitar testes.
- As validações de campos obrigatórios e relacionamentos são feitas via DataAnnotations e Fluent API.
- Para dúvidas ou sugestões, abra uma issue no repositório.
