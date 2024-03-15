
namespace VerificadorDuplicidadeCodigosRetorno
{
    internal class RegistraLog
    {
        public enum TipoDestino
        {
            Console = 1,
            Arquivo = 2,
            Ambos = 3
        }
        public static void Registrar(string strMensagem, TipoDestino destino)
        {
            if ((destino == TipoDestino.Console) || (destino == TipoDestino.Ambos))
                Console.WriteLine(strMensagem);

            if ((destino == TipoDestino.Arquivo) || (destino == TipoDestino.Ambos))
            {
                string caminhoArquivo;
                try
                {
                    caminhoArquivo = Path.Combine(AppContext.BaseDirectory, "log");
                    if (!Directory.Exists(caminhoArquivo))
                        Directory.CreateDirectory(caminhoArquivo);
                }
                catch
                {
                    Console.WriteLine("Falha validar diretorio de log");
                    return;
                }

                try
                {
                    caminhoArquivo = Path.Combine(caminhoArquivo, "ArquivoLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");

                    if (!File.Exists(caminhoArquivo))
                    {
                        FileStream arquivo = File.Create(caminhoArquivo);
                        arquivo.Close();
                    }
                }
                catch
                {
                    Console.WriteLine("Falha ao criar arquivo de log");
                    return;
                }

                try
                {
                    using (StreamWriter w = File.AppendText(caminhoArquivo))
                    {
                        w.WriteLine($"{DateTime.Now.ToString("HH:mm:ss")} => {strMensagem}");
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Falha ao gravar no arquivo de log");
                    Console.WriteLine(ex);

                    return;
                }
            }

        }
    }
}
