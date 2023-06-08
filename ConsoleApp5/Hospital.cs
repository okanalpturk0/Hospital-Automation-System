using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp5
{
    internal class Hospital
    {
        private ArrayList memberss;
        public ArrayList members { get { return memberss; } }
        List<DutyList> ListDuty;
        List<DoctorDuty> ListDoctorDuty;
        public Hospital()
        {
            memberss = new ArrayList();
            ListDuty = new List<DutyList>();
            ListDoctorDuty = new List<DoctorDuty>();
        }
        public void Add(Staff member)
        {
            members.Add(member);
        }       
        public void Add(DutyList duty)
        {
            ListDuty.Add(duty);
        }
        public void Add(DoctorDuty duty)
        {
            ListDoctorDuty.Add(duty);
        }
        //NurseDuties...
        public List<DutyList> getNurseDuties()
        {
            return ListDuty;
        }
        public List<DoctorDuty> getDoctorDuties()
        {
            return ListDoctorDuty;
        }
        public bool IsEmpty()
        {
            return members.Count == 0;
        }
        public void dutyWrite()
        {
             try
             {
                StreamWriter sw = new StreamWriter("nurseDuty.txt");

                foreach (DutyList duty in this.getNurseDuties())
                {
                    sw.WriteLine(
                                  duty.getDay + ";"
                                + duty.getMonth + ";"
                                + duty.getYear + ";"
                                + duty.getPesel
                                );
                

                }
                sw.Close();
             }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
             try
             {
            StreamWriter sw = new StreamWriter("doctorDuty.txt");

            foreach (DoctorDuty  duty in this.getDoctorDuties())
            {
                sw.WriteLine(
                              duty.getDay + ";"
                            + duty.getMonth + ";"
                            + duty.getYear + ";"
                            + duty.getPesel+ ";"
                            + duty.getOccupation
                            );

            }
            sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        //Write to File.....
        public void fileWrite()
        {
            if (!File.Exists("admin.txt"))
            {
                File.Create("admin.txt");
            }
            try
            {
                StreamWriter sw = new StreamWriter("staff.txt");

                foreach (var member in this.members)
                {

                    if (member.GetType() == typeof(Doctor))
                    {
                        Doctor dr = (Doctor)member;
                        sw.WriteLine(
                                 dr.getName + ";"
                                + dr.getSurname + ";"
                                + dr.getPesel + ";"
                                + dr.getPassword + ";"
                                + dr.getJob + ";"
                                + dr.getPwz + ";"
                                + dr.getOccupation
                                );
                    }

                    
                    else
                    {
                        Staff staff = (Staff)member;
                        sw.WriteLine(
                             staff.getName + ";"
                           + staff.getSurname + ";"
                           + staff.getPesel + ";"
                           + staff.getPassword + ";"
                           + staff.getJob
                           );
                    }
                }
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }        
        //Read from File.....
        public void readFile()
        {
            string line;
            try
            {
                StreamReader sr = new StreamReader("staff.txt");
                line = sr.ReadLine();
                while (line != null && line != "")
                {
                    string[] dizi2 = line.Split(';');
                    if (dizi2[4] == "Admin")
                    {
                        members.Add(new Admin(dizi2[0], dizi2[1], dizi2[2], dizi2[3], dizi2[4]));
                        line = sr.ReadLine();
                    }
                    else if (dizi2[4] == "Nurse")
                    {
                        members.Add(new Nurse(dizi2[0], dizi2[1], dizi2[2], dizi2[3], dizi2[4]));
                        line = sr.ReadLine();
                    }
                    else if (dizi2[4] == "Doctor")
                    {
                        members.Add(new Doctor(dizi2[0], dizi2[1], dizi2[2], dizi2[3], dizi2[4], dizi2[5], dizi2[6]));
                        line = sr.ReadLine();
                    }
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
        //Read Duties...
        public void readDuties()
        {
            string line;
            try
            {               
                StreamReader sr = new StreamReader("nurseDuty.txt");
                line = sr.ReadLine();
                while (line != null && line != "")
                {
                    string[] dizi = line.Split(';');
                    this.Add(new DutyList(Convert.ToInt32(dizi[0]), Convert.ToInt32(dizi[1]), Convert.ToInt32(dizi[2]), dizi[3]));
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            //............DOCTOR DUTIES............
            try
            {
                StreamReader sr = new StreamReader("doctorDuty.txt");
                line = sr.ReadLine();
                while (line != null && line != "")
                {
                    string[] dizi = line.Split(';');
                    this.Add(new DoctorDuty(Convert.ToInt32(dizi[0]), Convert.ToInt32(dizi[1]), Convert.ToInt32(dizi[2]), dizi[3], dizi[4]));
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            
        }
        //Show Duties...
        public void showDuties(int mod)
        {
            if (mod == 1)
            {
                if (this.getNurseDuties().Count != 0)
                {

                    foreach (DutyList duty in this.getNurseDuties())
                    {

                        Console.WriteLine("\n" + "\n" + "\n" +
                                    "Pesel Number : " + duty.getPesel + "        Duty Date : " + duty.getDay + "/" + duty.getMonth + "/" + duty.getYear);
                    }
                }
                else { Console.WriteLine("\n" + "There is not any Nurse on the List!"); }
            }
            else
            {
                if (this.getDoctorDuties().Count != 0)
                {

                    foreach (DoctorDuty duty in this.getDoctorDuties())
                    {
                        Console.WriteLine("\n" + "\n" + "\n" +
                        "Pesel Number : " + duty.getPesel + "        Duty Date : " + duty.getDay + "/" + duty.getMonth + "/" + duty.getYear + "  Occupation : " + duty.getOccupation);
                    }
                }
                else { Console.WriteLine("\n" + "There is not any Doctor on the List!"); }
            }

        }
        //Write members to Console.....
    }
}
