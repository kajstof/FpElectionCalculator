using System;
using System.Collections.Generic;
using System.Text;

namespace FpElectionCalculator.Domain
{
    // -----------------------------------------------------------------
    // Class written by Tomasz Lubinski and ported from Java to C#
    // http://www.algorytm.org/numery-identyfikacyjne/pesel/pesel-j.html
    // -----------------------------------------------------------------
    // PS. http://www.bogus.ovh.org/generatory/all.html - is not working
    // -----------------------------------------------------------------
    public class PeselValidator
    {
        private byte[] pesel = new byte[11];
        private bool valid = false;

        public PeselValidator(string pesel)
        {
            if (pesel.Length != 11)
            {
                valid = false;
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    this.pesel[i] = byte.Parse(pesel[i].ToString());
                }

                if (checkMonth() && checkDay() && checkSum())
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

        public int getBirthYear()
        {
            int year;
            int month;
            year = 10 * pesel[0];
            year += pesel[1];
            month = 10 * pesel[2];
            month += pesel[3];
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

        public int getBirthMonth()
        {
            int month;
            month = 10 * pesel[2];
            month += pesel[3];
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


        public int getBirthDay()
        {
            int day;
            day = 10 * pesel[4];
            day += pesel[5];
            return day;
        }

        public String getSex()
        {
            if (valid)
            {
                if (pesel[9] % 2 == 1)
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

        private bool checkSum()
        {
            int sum = 1 * pesel[0] +
            3 * pesel[1] +
            7 * pesel[2] +
            9 * pesel[3] +
            1 * pesel[4] +
            3 * pesel[5] +
            7 * pesel[6] +
            9 * pesel[7] +
            1 * pesel[8] +
            3 * pesel[9];
            sum %= 10;
            sum = 10 - sum;
            sum %= 10;

            if (sum == pesel[10])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkMonth()
        {
            int month = getBirthMonth();
            int day = getBirthDay();
            if (month > 0 && month < 13)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool checkDay()
        {
            int year = getBirthYear();
            int month = getBirthMonth();
            int day = getBirthDay();
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
            else if ((day > 0 && day < 30 && leapYear(year)) ||
            (day > 0 && day < 29 && !leapYear(year)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool leapYear(int year)
        {
            if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                return true;
            else
                return false;
        }
    }
}
