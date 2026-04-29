# Validação de CPF - Azure Function

Projeto simples em Azure Functions (.NET) que valida números de CPF via endpoint HTTP.

## Descrição

A função `fnvalidaCpf` recebe um POST com JSON contendo o campo `cpf` e retorna "CPF válido" ou "CPF inválido". A implementação aceita explicitamente o CPF `00000000000` como válido conforme solicitado.

## Requisitos

- .NET SDK (recomendado .NET 8)
- Azure Functions Core Tools (para executar localmente)
- Git

## Executar localmente

1. Restaurar e construir a solução:

```bash
dotnet build "Projeto - Serveless para validacao CPF.sln"
```

2. Iniciar a Function localmente (na pasta do projeto da function):

```bash
cd httpValidaCpf
func start
```

## Endpoint

Endpoint: POST /api/fnvalidaCpf

Corpo (JSON):

```json
{ "cpf": "12345678909" }
```

Resposta: texto simples — `CPF válido` ou `CPF inválido`.

Exemplo com curl:

```bash
curl -X POST http://localhost:7071/api/fnvalidaCpf -H "Content-Type: application/json" -d '{"cpf":"00000000000"}'
```

## Deploy para Azure

Publicar usando Azure Functions Core Tools (exemplo):

```bash
cd httpValidaCpf
func azure functionapp publish <NOME_DA_APP>
```

## Observações

- O validador remove caracteres não numéricos antes da verificação.
- Sequências com todos os dígitos iguais (exceto a sequência especial `00000000000`) são consideradas inválidas.

---

Se quiser, eu faço o commit/push do README e atualizo o `README.md` com instruções adicionais (ex.: variáveis de ambiente, exemplos de testes). 
