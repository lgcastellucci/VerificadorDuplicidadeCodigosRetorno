

using System.Xml;

namespace VerificadorDuplicidadeCodigosRetorno
{
    class Program
    {
        static void Main()
        {
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            Console.Title = "VerificadorDuplicidadeCodigosRetorno";

            do
            {
                Console.Write(LineAsterisk());
                Console.WriteLine("Analise de arquivos .csproj");
                Console.Write(LineAsterisk());
                Console.Write("Entre com o caminho do arquivo (ou 'q' para sair):");

                var val = Console.ReadLine();

                if (string.Equals(val, "q", StringComparison.OrdinalIgnoreCase))
                    return;

                if (!File.Exists(val))
                {
                    Console.WriteLine($"Arquivo não encontrado: {val}.");
                    continue;
                }

                val = "C:\\Projetos\\aspx\\Servicos\\branches\\02.01\\Servicos\\Servicos.csproj";
                if (File.Exists(val))
                {
                    if (Path.GetExtension(val) == ".csproj")
                    {
                        var leitorCsproj = new LeitorCsproj();
                        var itensCompile = leitorCsproj.LerItensCompile(val);

                        foreach (var item in itensCompile)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"O arquivo {val} não é um arquivo conhecido.");
                        continue;
                    }

                }

            } while (true);

        }
        public static string LineAsterisk()
        {
            string result = string.Empty;
            for (int i = 0; i < Console.WindowWidth; i++)
                result += "*";

            return result;
        }



    }
}