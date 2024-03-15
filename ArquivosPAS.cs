namespace VerificadorDuplicidadeCodigosRetorno
{
    public static class ArquivosPAS
    {
        public static List<string> BuscarArquivosPAS(string diretorio)
        {
            // Lista para armazenar os nomes dos arquivos
            var arquivosCS = new List<string>();

            // Obter os arquivos do diretório
            var arquivos = Directory.GetFiles(diretorio, "*.pas", SearchOption.AllDirectories);

            // Adicionar os nomes dos arquivos à lista
            foreach (var arquivo in arquivos)
                arquivosCS.Add(arquivo);

            // Retornar a lista de arquivos
            return arquivosCS;
        }
    }
}