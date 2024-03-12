using System.Text.RegularExpressions;

namespace VerificadorDuplicidadeCodigosRetorno
{
    public static class LeituraArquivo
    {
        public static List<string> BuscarCodigosErro(string caminhoArquivo)
        {
            // Lista para armazenar os códigos de erro encontrados
            var codigosErro = new List<string>();

            // Verificar se o arquivo existe
            if (!File.Exists(caminhoArquivo))
            {
                return codigosErro;
            }

            // Abrir o arquivo para leitura
            using (StreamReader sr = new StreamReader(caminhoArquivo))
            {
                // Ler o conteúdo do arquivo linha por linha
                while (!sr.EndOfStream)
                {
                    string linha = sr.ReadLine();

                    string[] linhaSplit = linha.Split('\"');
                    foreach (var item in linhaSplit)
                    {
                        // Verificar se a linha contém traço, isso separa a identificação da função e a identificação do erro
                        // Verificar se a linha tem 9 caracteres, pois é formado de dois blocos de 4 numeros separados pos um traço
                        // Verificar se a linha contém oito numeros, pois é a quantidade de numeros que compõe o código de erro
                        if (item.Contains("-") && (item.Length == 9) && (SomenteNumeros(item).Length == 8))
                        {
                            string codigoEncontrado = item;
                            codigosErro.Add(codigoEncontrado);
                        }
                    }
                }
            }

            // Retornar a lista de códigos de erro
            return codigosErro;
        }

        public static string SomenteNumeros(string str)
        {
            string numeros = "";
            foreach (char c in str)
                if (char.IsDigit(c))
                    numeros += c;
            return numeros;
        }
    }

}
