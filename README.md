# Projeto de Migração de Bens Patrimoniais - IP de Dourados/MS

Este projeto realiza a migração de dados de bens patrimoniais do sistema antigo do Instituto de Previdência de Dourados para a nova plataforma _Patrimônio Cloud_ da Betha.

As informações foram extraídas de um arquivo CSV disponibilizado pelo Instituto e armazenadas em um banco de dados intermediário PostgreSQL. O desenvolvimento foi realizado em .NET para gerenciar e automatizar o processo de upload desses dados para o novo sistema, garantindo consistência e compatibilidade com os padrões da plataforma Betha.

## Tabela de Conteúdos

- [Visão Geral](#visão-geral)
- [Arquitetura do Projeto](#arquitetura-do-projeto)
- [Funcionalidades](#funcionalidades)
- [Requisitos](#requisitos)
- [Configuração](#configuração)
- [Uso](#uso)
- [Contribuição](#contribuição)
- [Licença](#licença)

## Visão Geral

Este projeto foi desenvolvido para o Instituto de Previdência de Dourados com o objetivo de facilitar a transição dos dados de bens patrimoniais para o novo sistema de gerenciamento de patrimônio, que oferece recursos mais modernos e melhor acessibilidade aos dados.

A migração envolve a extração dos dados do arquivo CSV, o armazenamento e processamento no banco de dados PostgreSQL intermediário, e o upload no _Patrimônio Cloud_.

## Arquitetura do Projeto

A aplicação é composta por uma estrutura modular, incluindo:

- **Modelo de Dados**: Estruturas que representam os bens patrimoniais no novo formato exigido pelo _Patrimônio Cloud_.
- **Leitura de CSV**: Classe para leitura e processamento do arquivo CSV disponibilizado pelo Instituto.
- **Banco Intermediário PostgreSQL**: Banco de dados para armazenamento temporário e manipulação dos dados.
- **Processamento de Dados**: Script em .NET para transformação e validação dos dados.
- **API de Upload**: Integração com a API do _Patrimônio Cloud_ para envio dos dados migrados.

## Funcionalidades

- **Leitura de Arquivo CSV**: Leitura dos dados de um arquivo CSV fornecido pelo sistema legado.
- **Armazenamento Intermediário**: Uso do PostgreSQL como banco de dados intermediário para processar e preparar os dados.
- **Transformação e Validação de Dados**: Conversão e verificação dos dados para compatibilidade com a plataforma Betha.
- **Upload para o Patrimônio Cloud**: Utilização de uma API para enviar os dados ao novo sistema.
- **Relatório de Logs**: Geração de logs detalhados para acompanhamento do status de migração, incluindo erros e registros de sucesso.

## Requisitos

- [.NET 5 ou superior](https://dotnet.microsoft.com/download)
- Banco de Dados PostgreSQL
- Arquivo CSV com os dados de bens patrimoniais fornecido pelo Instituto
- Credenciais de acesso à API do Patrimônio Cloud Betha

## Configuração

1. **Clone o Repositório**:

   ```bash
   git clone https://github.com/nathaliacosim/patrimonio-upload.git
   cd patrimonio-upload
   ```

2. **Configuração**: Configure o caminho do CSV, as credenciais do banco de dados PostgreSQL e o Token da Entidade Cloud na classe Program.cs:

   ```c#
   LerCsv(@"C:\\Users\\DevStaf02\\Área de Trabalho\\bens.csv");

   string host = "localhost";
   int port = 5432;
   string database = "IP_Dourados";
   string username = "postgres";
   string password = "root";

   string token = "Bearer token";
   ```

3. **Compilação do Projeto**:
   ```bash
   dotnet build
   ```

## Uso

1. **Executar a Migração**:

   ```bash
   dotnet run
   ```

   Esse comando executa o processo de leitura do CSV, armazenamento no PostgreSQL, transformação, validação e envio dos dados para o _Patrimônio Cloud_.

2. **Verificar Logs**: Após a execução, consulte os arquivos de log para revisar os detalhes da migração.

## Contribuição

Sinta-se à vontade para contribuir com melhorias e correções de bugs. Para contribuir:

1. Crie um _fork_ do repositório.
2. Crie uma _branch_ para sua feature (`git checkout -b feature/nova-feature`).
3. Realize um _commit_ das mudanças (`git commit -am 'Adiciona nova feature'`).
4. Envie para o repositório remoto (`git push origin feature/nova-feature`).
5. Crie um _pull request_.

## Licença

Este projeto é licenciado sob a Licença MIT.
