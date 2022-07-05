using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeronnummerTest
{
    public class PersNr
    {
        public List<int> Personnummer { get; set; }
        public string Input { get; set; }
        public int ControlSum { get; set; }


        public void RunProgram()
        {
            InputPhase();
            Personnummer = new List<int>();
            if (!CheckInput())
            {
                Console.WriteLine("Ange 9st siffror");
                return;
            }
            if (!CheckValidPersNr())
            {
                Console.WriteLine("Fel Månad/Datum");
                return;
            }
            try
            {
                CalcControlSum();
            }
            catch (Exception)
            {
                throw new Exception("Något gick fel vid beräkningen av kontrollsumman");
            }
            CompletePersonalNumber();

        }

        private void CompletePersonalNumber()
        {
            Personnummer.Add(ControlSum);
            string outputString = string.Join("", Personnummer.ToArray());
            Console.WriteLine($"Kontrollsumman är: {ControlSum} och kompletta  personnummer är: {outputString}");
        }

        private void CalcControlSum()
        {
            int multiplier = 2;
            int sumOfDigits = 0;
            foreach (var item in Personnummer)
            {
                if (item * multiplier > 9)
                {
                    sumOfDigits += (item * multiplier) / 10;
                    sumOfDigits += (item * multiplier) % 10;
                }
                else
                {
                    sumOfDigits += item * multiplier;
                }

                if (multiplier == 2)
                {
                    multiplier = 1;
                }
                else
                {
                    multiplier = 2;
                }
            }
            ControlSum = (10 - (sumOfDigits % 10)) % 10;
        }

        private bool CheckValidPersNr()
        {
            // Check Month
            int month = (Personnummer[2] * 10) + Personnummer[3];
            if (month < 1 || month > 12)
            {
                Console.WriteLine($"Månad kan inte vara: {month}");
                return false;
            }

            //Check Date
            int date = (Personnummer[4] * 10) + Personnummer[5];
            if (date < 1 || date > 31)
            {
                Console.WriteLine($"Dagen kan inte vara: {date}");
                return false;
            }

            return true;
        }

        private bool CheckInput()
        {
            //Check input has correct length
            if (Input.Length < 9 || Input.Length > 9)
            {
                return false;
            }

            //Check input only contains digits
            for (int i = 0; i < Input.Length; i++)
            {
                int e;
                char a = Input[i];
                if (int.TryParse(a.ToString(), out e))
                {
                    Personnummer.Add(e);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public void InputPhase()
        {
            // Prompts user for input
            Console.WriteLine("Ange ett personnummer med de första 9 siffrorna");
            Input = Console.ReadLine();
        }
    }
}
