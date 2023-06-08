using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp5
{
     class DutyList
    {
        int day,month,year;
        string pesel;
        public int getDay
        {
            get
            {
                return day;
            }
        }
        public int getMonth
        {
            get
            {
                return month;
            }
        }
        public int getYear
        {
            get
            {
                return year;
            }
        }
        public string getPesel
        {
            get
            {
                return pesel;
            }
        }
        public DutyList(int day, int month, int year,string pesel)
        {
            this.day = day;
            this.month = month;
            this.year = year;
            this.pesel = pesel;
        }
    }
    class DoctorDuty : DutyList
    {
        string occupation;
        public string getOccupation { get { return occupation; } }

        public DoctorDuty(int day, int month, int year, string pesel,string occupation) : base(day, month, year, pesel)
        {
            this.occupation = occupation;
        }
    }
}
