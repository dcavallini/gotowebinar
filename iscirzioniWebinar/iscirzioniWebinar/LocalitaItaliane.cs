using System;
using System.Collections.Generic;
using System.IO;

namespace iscirzioniWebinar
{
    public class LocalitaItaliane
    {
        private List<string> provincie = new List<string>();
        private List<string> regioni = new List<string>();

        public LocalitaItaliane()
        {
            try
            {
                StreamReader sr = new StreamReader("regioniProvincie.csv");

                while (!sr.EndOfStream)
                {
                    string riga = sr.ReadLine();
                    provincie.Add(riga.Split(';')[0]);
                }

                sr.Close();

                StreamReader reader = new StreamReader("regioni.csv");

                while (!reader.EndOfStream)
                {
                    string riga = reader.ReadLine();
                    regioni.Add(riga);

                }

                reader.Close();

            }
            catch (Exception ex)
            {

            }

            provincie.Sort();

            provincie.Insert(0, "");
        }

        public List<String> GetProvincie()
        {
            return provincie;
        }

        public List<String> GetRegioni()
        {
            return regioni;
        }

    }
}
