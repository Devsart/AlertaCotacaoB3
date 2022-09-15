# B3Broker :chart:

### Envio de alertas de cotação por e-mail

A B3Broker visa monitorar os preços de ações da B3, emitindo alertas em valores alvo.

Requisitos:

  - .Net 5

Após clonar o repositório, altere o arquivo de configuração ```appSettings.json``` com as configurações de SMTP e lista de e-mails desejada.
Em seguida, build a aplicação com o comando:

```
dotnet build StockQuoteAlert.sln
```

Para começar a executar busque pela pasta ```/bin/Debug/net5.0/``` e acesse-a pela linha de comando. Execute o ```.exe``` então, da seguinte maneira:

```
StockQuoteAlert.exe <Ticker> <Limite_Superior> <Limite_Inferior>

Exemplo:
StockQuoteAlert.exe PETR4 24,49 24,38
```
