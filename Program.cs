
namespace VerificadorDuplicidadeCodigosRetorno
{
    class Program
    {
        static void Main()
        {
            Console.Title = "VerificadorDuplicidadeCodigosRetorno";

            do
            {
                Console.WriteLine(LineAsterisk.Content());
                Console.WriteLine("Analise de arquivos .csproj");
                Console.WriteLine(LineAsterisk.Content());
                Console.WriteLine("Projetos pré cadastrados");
                Console.WriteLine("1 - " + "C:\\Projetos\\aspx\\Servicos\\branches\\02.01\\Servicos\\Servicos.csproj");
                Console.WriteLine("2 - " + "C:\\Projetos\\aspx\\ParceleDebitos\\branches\\01.00\\WebService\\WebService.csproj");

                Console.Write("Entre com o caminho do arquivo (ou 'q' para sair):");

                var conteudoDigitado = Console.ReadLine();

                if (string.Equals(conteudoDigitado, "q", StringComparison.OrdinalIgnoreCase))
                    return;

                if (string.Equals(conteudoDigitado, "1", StringComparison.OrdinalIgnoreCase))
                    conteudoDigitado = "C:\\Projetos\\aspx\\Servicos\\branches\\02.01\\Servicos\\Servicos.csproj";
                if (string.Equals(conteudoDigitado, "2", StringComparison.OrdinalIgnoreCase))
                    conteudoDigitado = "C:\\Projetos\\aspx\\ParceleDebitos\\branches\\01.00\\WebService\\WebService.csproj";

                if (!File.Exists(conteudoDigitado))
                {
                    Console.WriteLine($"Arquivo não encontrado: {conteudoDigitado}.");
                    continue;
                }

                if (File.Exists(conteudoDigitado))
                {
                    var arquivosDeCodigoFonte = new List<Arquivos>();
                    var arquivosDeDefinicao = new List<Arquivos>();
                    var arquivosDeDefinicaoAntigo = new List<Arquivos>();

                    if (Path.GetExtension(conteudoDigitado) == ".csproj")
                    {
                        // Procurando todos os arquivos .cs que existem no diretorio e subdiretorio do .csproj
                        var buscarArquivosCS = ArquivosCS.BuscarArquivosCS(Path.GetDirectoryName(conteudoDigitado));

                        // Efetuar a analise em cada arquivo .cs
                        foreach (var arquivo in buscarArquivosCS)
                        {
                            // Procurando todos os códigos de retorno interno que existem no arquivo .cs
                            var codigosErro = LeituraArquivo.BuscarCodigosErro(arquivo);

                            //se existir algum código de erro no arquivo, adicionar em uma lista com os códigos encontrado e em outra lista as definições encontradas
                            if (codigosErro.Count > 0)
                            {
                                if (arquivo.Contains("RetornoInterno.cs"))
                                    arquivosDeDefinicao.Add(new Arquivos { Caminho = arquivo, CodigosErro = codigosErro });
                                else if (arquivo.Contains("AppFuncoes.cs"))
                                    arquivosDeDefinicaoAntigo.Add(new Arquivos { Caminho = arquivo, CodigosErro = codigosErro });
                                else
                                    arquivosDeCodigoFonte.Add(new Arquivos { Caminho = arquivo, CodigosErro = codigosErro });
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"O arquivo {conteudoDigitado} não é um arquivo conhecido.");
                        continue;
                    }

                    // Checando foram encontrados codigos de retorno interno
                    if (arquivosDeCodigoFonte.Count > 0)
                    {
                        Console.WriteLine(LineAsterisk.Content());
                        Console.WriteLine("Arquivos a serem analisados:");

                        var todosOsCodigosErro = new List<string>();
                        foreach (var arquivo in arquivosDeCodigoFonte)
                        {
                            bool encontrouAlgoNoArquivo = false;
                            //Console.WriteLine(arquivo.Caminho);
                            foreach (var codigo in arquivo.CodigosErro)
                            {
                                if (todosOsCodigosErro.Contains(codigo))
                                {
                                    if (!encontrouAlgoNoArquivo)
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine(arquivo.Caminho);
                                        encontrouAlgoNoArquivo = true;
                                    }
                                    Console.WriteLine($"Duplicado: {codigo}");
                                }
                                else
                                {
                                    //Console.WriteLine(codigo);
                                    todosOsCodigosErro.Add(codigo);
                                }

                                // Verificar se o código de erro está nos arquivos de definição
                                if (arquivosDeDefinicao.Count > 0)
                                {
                                    bool encontrouDefinicao = false;
                                    foreach (var arquivoDefinicao in arquivosDeDefinicao)
                                    {
                                        if (arquivoDefinicao.CodigosErro.Contains(codigo))
                                        {
                                            encontrouDefinicao = true;
                                        }
                                    }

                                    if (!encontrouDefinicao)
                                    {
                                        if (!encontrouAlgoNoArquivo)
                                        {
                                            Console.WriteLine("");
                                            Console.WriteLine(arquivo.Caminho);
                                            encontrouAlgoNoArquivo = true;
                                        }
                                        Console.WriteLine($"Código de erro sem definição: {codigo}");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Nenhum arquivo de definição encontrado.");
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nenhum arquivo com códigos de erro encontrado.");
                    }

                }

            } while (true);

        }

    }
}
