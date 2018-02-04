using System;

namespace FpElectionCalculator.Domain.Models
{
    // -------------------------------------------------------------------
    // This class is modified and ported from Java to C#
    // -------------------------------------------------------------------
    // Class originally written by Tomasz Lubinski
    // http://www.algorytm.org/numery-identyfikacyjne/pesel/pesel-j.html
    // -------------------------------------------------------------------
    public class Pesel
    {
        private readonly byte[] _peselByte = new byte[11];
        private readonly string _peselString;
        private readonly bool _valid;

        public Pesel(string pesel)
        {
            _peselString = pesel;
            if (pesel.Length != 11)
            {
                _valid = false;
            }
            else
            {
                for (int i = 0; i < 11; i++)
                {
                    _peselByte[i] = byte.Parse(pesel[i].ToString());
                }

                if (CheckMonth() && CheckDay() && CheckSum())
                {
                    _valid = true;
                }
                else
                {
                    _valid = false;
                }
            }
        }

        public bool IsValid()
        {
            return _valid;
        }

        public int GetBirthYear()
        {
            int year = 10 * _peselByte[0];
            year += _peselByte[1];
            int month = 10 * _peselByte[2];
            month += _peselByte[3];
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
            int month = 10 * _peselByte[2];
            month += _peselByte[3];
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
            int day = 10 * _peselByte[4];
            day += _peselByte[5];
            return day;
        }

        public DateTime GetBirthdayDate() => new DateTime(GetBirthYear(), GetBirthMonth(), GetBirthDay());

        public bool IsEighteen()
        {
            return GetBirthdayDate().AddYears(18) <= DateTime.Now;
        }

        public String GetSex()
        {
            if (_valid)
            {
                if (_peselByte[9] % 2 == 1)
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
            int sum = 1 * _peselByte[0] +
                      3 * _peselByte[1] +
                      7 * _peselByte[2] +
                      9 * _peselByte[3] +
                      1 * _peselByte[4] +
                      3 * _peselByte[5] +
                      7 * _peselByte[6] +
                      9 * _peselByte[7] +
                      1 * _peselByte[8] +
                      3 * _peselByte[9];
            sum %= 10;
            sum = 10 - sum;
            sum %= 10;

            if (sum == _peselByte[10])
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
            return _peselString;
        }
    }
}