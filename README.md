
# Transação API

Um projeto Web API em .NET para gerenciamento de transações financeiras, com validações de data e valor, logs estruturados e testes unitários.
Esse projeto é um desafio do **Itaú** para vaga de Desenvolvedor Junior.


## 📋 Funcionalidades

✔ Adicionar transações com validações

✔ Listar transações recentes (com filtro por tempo)

✔ Limpar todas as transações

✔ Validações automáticas (data futura, valor negativo)

✔ Logs detalhados (Informação e Erros)


## 🚀 Como Configurar e Executar

Pré-requisitos
- .NET 8 SDK

- IDE (Visual Studio 2022)

#### Passo a Passo
1 - Clone o repositório
```bash
git clone https://github.com/seu-usuario/transaction-api.git
cd transaction-api
```

2 - Restaure as dependências
```bash
dotnet restore
```

3 - Execute os testes
```bash
dotnet test
```

4 - Inicie a API
```bash
dotnet run --project TransactionAPI
```

## **📌 Endpoints**

| Método HTTP | Rota                     | Descrição                                  |
|-------------|--------------------------|--------------------------------------------|
| `POST`      | `/AddTransaction`          | Adiciona uma nova transação                |
| `DELETE`       | `/DeleteTransactions`          | Deleta todas as transações                  |
| `GET`       | `/GetStatistics` | Lista transações dos últimos `n` segundos  |

### **Exemplo de Request (POST /AddTransaction)**
```json
{
    "TransactionValue": 150.99,
    "TrasactionDate": "2024-05-20T10:00:00Z"
}
```

## **🧪 Testes**
Testes unitários incluídos para:

✅ Validação de transações

✅ Comportamento do serviço

✅ Verificação de logs

Execute com:
```bash
dotnet test
```

## **📝 Logs**
A API registra:

- Informações para ações bem-sucedidas

- Erros para validações falhadas

Exemplo de saída:
```bash
info: Adicionando transação...
fail: Data inválida: maior que a data atual
```

## **📄 Licença**
MIT License.
