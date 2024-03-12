# VerificadorDuplicidadeCodigosRetorno


## VerificadorDuplicidadeCodigosRetorno

**Descrição:**

A aplicação "Verificador de Duplicidade em Códigos de Retorno" tem como objetivo :
	- Identificar códigos de retorno duplicados em um conjunto de código fonte.
	- Identificar códigos de retorno que estão sem definição. 

**Funcionalidades:**

* Busca por arquivos em um diretório especificado.
* Separa os códigos duplicados dos códigos de definição.
* Identifica códigos de retorno duplicados.
* Lista os códigos de retorno sem definição.

**Utilização:**

1. Clone o repositório: `git clone https://github.com/lgcastellucci/VerificadorDuplicidadeCodigosRetorno.git`


**Exemplo:**

```
VerificadorDuplicidadeCodigosRetorno.exe
```

**Saída:**

```
Caminho do arquivo:
- codigo_duplicado
- codigo_sem_definição

```

**Observações:**

O modelo de analise esta baseado no modelo de organização de código fonte da empresa onde trabalho, porém, o código pode ser facilmente adaptado para outros modelos de organização.

Abaixo demonstro um trecho em C# de como é feita a definição de um código de retorno:
```
  retorno.CodigoRetorno = 18;
  retorno.CodigoRetornoInterno = "0049-0003";
  retorno.MensagemRetorno = RetornoInterno.getMessage(CodCliente, retorno.CodigoRetornoInterno, true);
  return retorno;
```

**Melhorias:**

* Adicionar a opção de especificar arquivos específicos a serem verificados.
* Adicionar a opção de ignorar pastas específicas.
* Adicionar a opção de gerar um relatório em HTML.

**Contribuições:**

* Sinta-se à vontade para contribuir para o projeto!
* Fork o repositório e faça suas alterações.
* Crie um pull request para que suas alterações sejam revisadas.

**Agradecimentos:**

* Agradeço a todos que contribuíram para este projeto!
