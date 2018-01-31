using System;

namespace FpElectionCalculator.Domain.Models
{
    // -----------------------------------------------------------------
    // Class written by Tomasz Lubinski, originally written in Java
    // http://www.algorytm.org/numery-identyfikacyjne/pesel/pesel-j.html
    // This class is slightly modified and ported to C#
    // -----------------------------------------------------------------
    // PS. http://www.bogus.ovh.org/generatory/all.html - is not working
    // -----------------------------------------------------------------
    public class Pesel
    {
        private byte[] peselByte = new byte[11];
        private string peselString;
        private bool valid = false;

        public Pesel(string pesel)
        {
            peselString = pesel;
            if (pesel.Length != 11)
            {
                valid = false;
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    this.peselByte[i] = byte.Parse(pesel[i].ToString());
                }

                if (CheckMonth() && CheckDay() && CheckSum())
                {
                    valid = true;
                }
                else
                {
                    valid = false;
                }
            }
        }

        public bool IsValid()
        {
            return valid;
        }

        public int GetBirthYear()
        {
            int year;
            int month;
            year = 10 * peselByte[0];
            year += peselByte[1];
            month = 10 * peselByte[2];
            month += peselByte[3];
            if (month > 80 && month < 93)
            {
                year += 1800;
            }
            else if (month > 0 && month < 13)
            {
                year += 1900;
            }
            else if (month > 20 && month < 33)
            {
                year += 2000;
            }
            else if (month > 40 && month < 53)
            {
                year += 2100;
            }
            else if (month > 60 && month < 73)
            {
                year += 2200;
            }
            return year;
        }

        public int GetBirthMonth()
        {
            int month;
            month = 10 * peselByte[2];
            month += peselByte[3];
            if (month > 80 && month < 93)
            {
                month -= 80;
            }
            else if (month > 20 && month < 33)
            {
                month -= 20;
            }
            else if (month > 40 && month < 53)
            {
                month -= 40;
            }
            else if (month > 60 && month < 73)
            {
                month -= 60;
            }
            return month;
        }


        public int GetBirthDay()
        {
            int day;
            day = 10 * peselByte[4];
            day += peselByte[5];
            return day;
        }

        public String GetSex()
        {
            if (valid)
            {
                if (peselByte[9] % 2 == 1)
                {
                    return "Male";
                }
                else
                {
                    return "Female";
                }
            }
            else
            {
                return "---";
            }
        }

        private bool CheckSum()
        {
            int sum = 1 * peselByte[0] +
            3 * peselByte[1] +
            7 * peselByte[2] +
            9 * peselByte[3] +
            1 * peselByte[4] +
            3 * peselByte[5] +
            7 * peselByte[6] +
            9 * peselByte[7] +
            1 * peselByte[8] +
            3 * peselByte[9];
            sum %= 10;
            sum = 10 - sum;
            sum %= 10;

            if (sum == peselByte[10])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckMonth()
        {
            int month = GetBirthMonth();
            int day = GetBirthDay();
            if (month > 0 && month < 13)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckDay()
        {
            int year = GetBirthYear();
            int month = GetBirthMonth();
            int day = GetBirthDay();
            if ((day > 0 && day < 32) &&
            (month == 1 || month == 3 || month == 5 ||
            month == 7 || month == 8 || month == 10 ||
            month == 12))
            {
                return true;
            }
            else if ((day > 0 && day < 31) &&
            (month == 4 || month == 6 || month == 9 ||
            month == 11))
            {
                return true;
            }
            else if ((day > 0 && day < 30 && LeapYear(year)) ||
            (day > 0 && day < 29 && !LeapYear(year)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool LeapYear(int year)
        {
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                return true;
            else
                return false;
        }

        public override string ToString()
        {
            return peselString;
        }
    }
}
