
namespace GestionInventario.Controlador
{
    class BarCode
    {
        private static string country = "780";
        private static string manufact = "99876";
        public static string generate13(int count)
        {
            return $"{country}{manufact}{count+1}";
        }
    }
}
