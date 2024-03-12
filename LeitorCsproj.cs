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

            var manager = new XmlNamespaceManager(documentoXml.NameTable);
            manager.AddNamespace("msbuild", "http://schemas.microsoft.com/developer/msbuild/2003");

            var nodes = documentoXml.SelectNodes("//msbuild:Compile", manager);
            foreach (XmlNode node in nodes)
            {
                var includeAttribute = node.Attributes["Include"];
                if (includeAttribute != null)
                {
                    itensCompile.Add(includeAttribute.Value);
                }
            }

            return itensCompile;
        }
    }
}
