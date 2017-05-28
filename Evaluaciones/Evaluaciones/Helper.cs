using System;
using System.Net;
using System.Net.Mail;
using System.Text;
namespace Evaluaciones
{
	public static class Helper
	{
		public static void SendMail(string to, string subject, string body)
		{
			MailMessage message = new MailMessage();
			message.From = new MailAddress("applicationserver@iEvaluaciones.cl");
			message.To.Add(to);
			message.Subject = subject;
			message.Body = body;
			message.IsBodyHtml = false;
			message.Priority = MailPriority.Normal;

			SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
			smtp.EnableSsl = true;
			smtp.Credentials = new NetworkCredential("applicationserver@iEvaluaciones.cl", "insignia", "");
			smtp.Send(message);
		}

        #region Escritura de números en palabras

        private readonly static string[] _numbersInWords =
             {
            "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve",
 
            "diez", "once", "doce", "trece", "catorce", "quince", "dieciseis", "diecisiete", "dieciocho", "diecinueve",
 
            "veinte", "veintiuno", "veintidos", "veintitres", "veinticuatro", "veinticinco", "veintiseis", "veintisiete", "veintiocho", "veintinueve",
 
            "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa",
 
            "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos"
             };

        private const string _centWord = "centavo(s)";

        private const string _oneMillionWord = "un millon";

        private const string _millionsWord = "millones";

        private const string _oneThousandWord = "mil";

        private const string _thousandWord = "mil";

        private const string _hundredWord = "cien";

        private const string _oneHundredWord = "ciento un";

        private const string _zeroWithWord = "cero con";

        private const string _withWord = "con";

        private const string _andWord = "y";

        /// <summary>
        /// Convierte números a palabras
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string NumerosAPalabras(double number)
        {
            if (number > 999999999)
                return "Numero demasiado grande";

            StringBuilder Words;

            string FormattedNumberString;

            int decimalSeparatorLocation;

            int millionsPart;

            int thousandsPart;

            int hundredsPart;

            int decimalsPart;

            int hundreds;

            int tens;

            int units;

            int ActualNumber = 0;

            Words = new StringBuilder();

            FormattedNumberString = number.ToString("000000000.00");

            char DecimalSeparator = Convert.ToChar((Convert.ToString(1.1)).Trim().Substring(1, 1));

            decimalSeparatorLocation = FormattedNumberString.IndexOf(DecimalSeparator);

            millionsPart = Convert.ToInt32(FormattedNumberString.Substring(0, 3));

            thousandsPart = Convert.ToInt32(FormattedNumberString.Substring(3, 3));

            hundredsPart = Convert.ToInt32(FormattedNumberString.Substring(6, 3));

            decimalsPart = Convert.ToInt32(FormattedNumberString.Substring(decimalSeparatorLocation + 1, 2));

            for (int NumberPart = 1; NumberPart <= 4; NumberPart++)
            {
                switch (NumberPart)
                {
                    case 1:
                        {
                            ActualNumber = millionsPart;

                            if (millionsPart == 1)
                            {

                                Words.Append(_oneMillionWord);
                                Words.Append(' ');
                                continue;
                            }

                            break;
                        }

                    case 2:
                        {
                            ActualNumber = thousandsPart;

                            if (millionsPart != 1 && millionsPart != 0)
                            {
                                Words.Append(_millionsWord);
                                Words.Append(' ');
                            }

                            if (thousandsPart == 1)
                            {
                                Words.Append(_oneThousandWord);
                                Words.Append(' ');

                                continue;
                            }

                            break;
                        }
                    case 3:
                        {
                            ActualNumber = hundredsPart;

                            if (thousandsPart != 1 && thousandsPart != 0)
                            {
                                Words.Append(_thousandWord);
                                Words.Append(' ');
                            }

                            break;
                        }
                    case 4:
                        {
                            ActualNumber = decimalsPart;

                            if (decimalsPart != 0)
                            {
                                if (millionsPart == 0 && thousandsPart == 0 && hundredsPart == 0)
                                {
                                    Words.Append(_zeroWithWord);
                                    Words.Append(' ');
                                }
                                else
                                {
                                    Words.Append(_withWord);
                                    Words.Append(' ');
                                }
                            }

                            break;
                        }
                }

                hundreds = (int)(ActualNumber / 100);
                tens = (int)(ActualNumber - hundreds * 100) / 10;
                units = (int)(ActualNumber - (hundreds * 100 + tens * 10));

                if (ActualNumber == 0) continue;

                if (ActualNumber == 100)
                {
                    Words.Append(_hundredWord);
                    Words.Append(' ');
                    continue;
                }
                else
                {
                    if (ActualNumber == 101 && NumberPart != 3)
                    {
                        Words.Append(_oneHundredWord);
                        Words.Append(' ');
                        continue;
                    }
                    else
                    {
                        if (ActualNumber > 100)
                        {
                            Words.Append(_numbersInWords[hundreds + 35]);
                            Words.Append(' ');
                        }
                    }
                }

                if (tens < 3 && tens != 0)
                {
                    Words.Append(_numbersInWords[tens * 10 + units - 1]);
                    Words.Append(' ');
                }
                else
                {
                    if (tens > 2)
                    {
                        Words.Append(_numbersInWords[tens + 26]);
                        Words.Append(' ');

                        if (units == 0)
                            continue;

                        Words.Append(_andWord);
                        Words.Append(' ');
                        Words.Append(_numbersInWords[units - 1]);
                        Words.Append(' ');
                    }

                    else
                    {
                        if (tens == 0 && units != 0)
                        {
                            Words.Append(_numbersInWords[units - 1]);
                            Words.Append(' ');
                        }
                    }
                }
            } // end for

            if (decimalsPart != 0)
                Words.Append(_centWord);

            // Resolve particular problems here.
            Words.Replace("uno mil", "un mil");

            return Words.ToString().Trim();
        }

        #endregion

        public static bool ValidaRun(int runCuerpo, char runDigito)
        {
            return Evaluaciones.Helper.GetDigito(runCuerpo).ToString().ToLower().Equals(runDigito.ToString().ToLower());
        }

        private static char GetDigito(int cuerpo)
        {
            int contador = 0;
            int multiplo = 2;

            while (cuerpo != 0)
            {
                contador += (cuerpo % 10) * multiplo;

                cuerpo /= 10;

                multiplo++;

                if (multiplo == 8) multiplo = 2;
            }

            contador = 11 - (contador % 11);

            if (contador == 10) return 'K';
            else if (contador == 11) return '0';
            else return char.Parse(contador.ToString());
        }
	}
}
