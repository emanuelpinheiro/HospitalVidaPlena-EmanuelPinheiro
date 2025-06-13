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
No sistema Hospital Vida Plena, um paciente pode possuir múltiplos prontuários e diversas internações.  Cada Prontuário é exclusivo para um único paciente e pode incluir diversos Atendimentos.  Cada Atendimento está relacionado a um Profissional e a um prontuário, com a possibilidade de incluir prescrições e exames vinculados.  Uma internação está relacionada tanto ao paciente quanto a um tipo específico de atendimento, podendo resultar em uma alta hospitalar.  Dessa forma, o modelo espelha o fluxo real de atendimento em hospitais, assegurando a rastreabilidade e a integridade dos dados clínicos.
---
