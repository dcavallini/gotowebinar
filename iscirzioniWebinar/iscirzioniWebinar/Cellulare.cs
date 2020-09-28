using System;
namespace iscirzioniWebinar
{
    public class Cellulare
    {
        public static bool CheckLunghezza(string numero)
        {
            if(numero.Length == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public static bool CheckNumeri(string numero)
        {
            bool check = false;

            foreach(char c in numero)
            {
                if(c.Equals('0') || c.Equals('1') || c.Equals('2') || c.Equals('2') || c.Equals('3') || c.Equals('4') || c.Equals('5') ||
                    c.Equals('6') || c.Equals('7') || c.Equals('8') || c.Equals('9'))
                {
                    check = true;
                }
                else
                {
                    return false;
                }
            }

            return check;

        }
    }
}
