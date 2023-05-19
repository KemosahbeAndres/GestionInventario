namespace GestionInventario.Controlador
{
    class BarCode
    {
        private static string country = "780";
        private static string manufact = "99876";
        public static string generate13(int count)
        {
            string numero = "1";
            if(count > 0)
            {
                numero = (count + 1).ToString();
            }
            string value = numero.PadLeft(5-numero.Length, '0');
            return $"{country}{manufact}{value}";
        }
    }
}
