
namespace VerificadorDuplicidadeCodigosRetorno
{
    internal class LineAsterisk
    {
        public static string Content()
        {
            string result = string.Empty;
            for (int i = 0; i < Console.WindowWidth; i++)
                result += "*";

            return result;
        }
    }
}
