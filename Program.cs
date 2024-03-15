
namespace VerificadorDuplicidadeCodigosRetorno
{
    class Program
    {
        static void Main()
        {
            Console.Title = "VerificadorDuplicidadeCodigosRetorno";

            do
            {
                RegistraLog.Registrar(LineAsterisk.Content(), RegistraLog.TipoDestino.Ambos);
                RegistraLog.Registrar("Iniciando aplicação", RegistraLog.TipoDestino.Ambos);
                RegistraLog.Registrar("Analise de arquivos .csproj e .dpr", RegistraLog.TipoDestino.Ambos);
                RegistraLog.Registrar(LineAsterisk.Content(), RegistraLog.TipoDestino.Ambos);
                RegistraLog.Registrar("Projetos pré cadastrados", RegistraLog.TipoDestino.Console);
                RegistraLog.Registrar("1 - " + "C:\\Projetos\\aspx\\Servicos\\branches\\02.01\\Servicos\\Servicos.csproj", RegistraLog.TipoDestino.Console);
                RegistraLog.Registrar("2 - " + "C:\\Projetos\\aspx\\ParceleDebitos\\branches\\01.00\\WebService\\WebService.csproj", RegistraLog.TipoDestino.Console);
                RegistraLog.Registrar("3 - " + "C:\\Projetos\\delphi\\Cartao\\branches\\02.56\\Cartao.dpr", RegistraLog.TipoDestino.Console);

                RegistraLog.Registrar("Entre com o caminho do arquivo (ou 'q' para sair):", RegistraLog.TipoDestino.Console);

                var conteudoDigitado = Console.ReadLine();

                if (string.Equals(conteudoDigitado, "q", StringComparison.OrdinalIgnoreCase))
                    return;

                if (string.Equals(conteudoDigitado, "1", StringComparison.OrdinalIgnoreCase))
                    conteudoDigitado = "C:\\Projetos\\aspx\\Servicos\\branches\\02.01\\Servicos\\Servicos.csproj";
                else if (string.Equals(conteudoDigitado, "2", StringComparison.OrdinalIgnoreCase))
                    conteudoDigitado = "C:\\Projetos\\aspx\\ParceleDebitos\\branches\\01.00\\WebService\\WebService.csproj";
                else if (string.Equals(conteudoDigitado, "3", StringComparison.OrdinalIgnoreCase))
                    conteudoDigitado = "C:\\Projetos\\delphi\\Cartao\\branches\\02.56\\Cartao.dpr";

                if (!File.Exists(conteudoDigitado))
                {
                    RegistraLog.Registrar($"Arquivo não encontrado: {conteudoDigitado}.", RegistraLog.TipoDestino.Ambos);
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
                            var codigosErro = LeituraArquivo.BuscarCodigosErroNoCS(arquivo);

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
                    else if (Path.GetExtension(conteudoDigitado) == ".dpr")
                    {
                        // Procurando todos os arquivos .pas que existem no diretorio e subdiretorio do .dpr
                        var buscarArquivosPAS = ArquivosPAS.BuscarArquivosPAS(Path.GetDirectoryName(conteudoDigitado));

                        // Efetuar a analise em cada arquivo .pas
                        foreach (var arquivo in buscarArquivosPAS)
                        {
                            // Procurando todos os códigos de retorno interno que existem no arquivo .cs
                            var codigosErro = LeituraArquivo.BuscarCodigosErroNoPAS(arquivo);

                            //se existir algum código de erro no arquivo, adicionar em uma lista com os códigos encontrado e em outra lista as definições encontradas
                            if (codigosErro.Count > 0)
                            {
                                arquivosDeCodigoFonte.Add(new Arquivos { Caminho = arquivo, CodigosErro = codigosErro });
                                
                                //no pas não temos a definição do texto de resposta pois o testo é baseado no codigo do erro e não no codigo do erro interno
                                //então vou adicionar como se tivesse encontrado a definição
                                arquivosDeDefinicao.Add(new Arquivos { Caminho = arquivo, CodigosErro = codigosErro });
                            }
                        }
                    }
                    else
                    {
                        RegistraLog.Registrar($"O arquivo {conteudoDigitado} não é um arquivo conhecido.", RegistraLog.TipoDestino.Ambos);
                        continue;
                    }

                    int qtdCodigoDeErroDuplicados = 0;
                    int qtdCodigoDeErroSemDefiniccao = 0;
                    // Checando foram encontrados codigos de retorno interno
                    if (arquivosDeCodigoFonte.Count > 0)
                    {
                        RegistraLog.Registrar(LineAsterisk.Content(), RegistraLog.TipoDestino.Ambos);
                        RegistraLog.Registrar("Arquivos a serem analisados:", RegistraLog.TipoDestino.Ambos);

                        var todosOsCodigosErro = new List<string>();
                        foreach (var arquivo in arquivosDeCodigoFonte)
                        {
                            bool encontrouAlgoNoArquivo = false;

                            foreach (var codigo in arquivo.CodigosErro)
                            {
                                if (todosOsCodigosErro.Contains(codigo))
                                {
                                    if (!encontrouAlgoNoArquivo)
                                    {
                                        RegistraLog.Registrar("", RegistraLog.TipoDestino.Ambos);
                                        RegistraLog.Registrar(arquivo.Caminho, RegistraLog.TipoDestino.Ambos);
                                        encontrouAlgoNoArquivo = true;
                                    }
                                    RegistraLog.Registrar($"Duplicado: {codigo}", RegistraLog.TipoDestino.Ambos);
                                    qtdCodigoDeErroDuplicados++;
                                }
                                else
                                {
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
                                            RegistraLog.Registrar("", RegistraLog.TipoDestino.Ambos);
                                            RegistraLog.Registrar(arquivo.Caminho, RegistraLog.TipoDestino.Ambos);
                                            encontrouAlgoNoArquivo = true;
                                        }
                                        RegistraLog.Registrar($"Código de erro sem definição: {codigo}", RegistraLog.TipoDestino.Ambos);
                                        qtdCodigoDeErroSemDefiniccao++;
                                    }
                                }
                                else
                                {
                                    RegistraLog.Registrar("Nenhum arquivo de definição encontrado.", RegistraLog.TipoDestino.Ambos);
                                }
                            }
                        }

                        RegistraLog.Registrar("--RESUMO--", RegistraLog.TipoDestino.Ambos);
                        RegistraLog.Registrar($"Quantidade de codigo de erro duplicado: {qtdCodigoDeErroDuplicados}", RegistraLog.TipoDestino.Ambos);
                        RegistraLog.Registrar($"Quantidade de codigo de erro sem definição: {qtdCodigoDeErroSemDefiniccao}", RegistraLog.TipoDestino.Ambos);
                    }
                    else
                    {
                        RegistraLog.Registrar("Nenhum arquivo com códigos de erro encontrado.", RegistraLog.TipoDestino.Ambos);
                    }

                }

            } while (true);

        }

    }
}
