using System.Xml;

namespace VerificadorDuplicidadeCodigosRetorno
{
    public class LeitorCsproj
    {
        public List<string> LerItensCompile(string caminhoCsproj)
        {
            // Lista para armazenar os itens "Compile Include"
            var itensCompile = new List<string>();

            // Carregar o documento XML
            var documentoXml = new XmlDocument();
            documentoXml.Load(caminhoCsproj);

            // Obter os nós "Compile"
            var nosCompile = documentoXml.SelectNodes("Compile");

            // Extrair o atributo "Include" de cada nó "Compile"
            foreach (XmlNode noCompile in nosCompile)
            {
                string itemCompile = noCompile.Attributes["Include"].Value;
                itensCompile.Add(itemCompile);
            }

            return itensCompile;
        }
    }
}
