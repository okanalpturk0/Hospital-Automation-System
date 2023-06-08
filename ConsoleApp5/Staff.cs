using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp5
{
    abstract class Staff
    {
        private string name, surname, job;
        private string pesel, password;
        public string getName { get { return name; } }
        public string getSurname { get { return surname; } }      
        public string getPesel { get { return pesel; } }  
        public string getPassword { get { return password; } }   
        public string getJob { get { return job; } } 
        public Staff(string name, string surname, string pesel, string password, string job)
        {
            this.name = name;
            this.surname = surname;
            this.pesel = pesel;
            this.password = password;
            this.job = job;
        }
        public void EnterencePanel(string nameSurname, Hospital obj)
        {

        tag1:

            Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + "                                                                                " + nameSurname + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + "\n" + "\n" +
                              "To see Doctor & Nurses List please press D2" + "\n" +
                              "\n" + "\n" +
                              "To see your Duty List, please press F6" + "\n" + "\n" +

                              "To Exit Application, please press Esc(Escape)");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + "\n" + "\n" + "                                                                                  LOGOUT(PRESS L)");
            Console.ForegroundColor = ConsoleColor.Yellow;
            ConsoleKey ck = Console.ReadKey(true).Key;
            //Show Doctors
            if (ck == ConsoleKey.D2)
            {
                showMembers(obj);
                Console.WriteLine("\n" + "If you wanna go to homepage, please press H!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.H) goto tag1;
            }
           
            //Show Duties
            if (ck == ConsoleKey.F6)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                bool isDuty = false;
                if(Program.loginOccupation=="Doctor")
                { 
                    foreach (DoctorDuty n in obj.getDoctorDuties())
                    {
                        if (n.getPesel.Equals(Program.loginPesel))
                        {
                            Console.WriteLine("Duty Date : " + n.getDay + "/" + n.getMonth + "/" + n.getYear);
                            isDuty = true;

                        }
                    }
                    if (isDuty == false) { Console.WriteLine("There is no Duty defined for you!"); }
                }

                else if(Program.loginOccupation=="Nurse")
                { 
                    foreach (DutyList n in obj.getNurseDuties())
                    {
                        if (n.getPesel.Equals(Program.loginPesel))
                        {
                            Console.WriteLine("Duty Date : " + n.getDay + "/" + n.getMonth + "/" + n.getYear);
                            isDuty = true;

                        }
                    }
                    if (isDuty == false) { Console.WriteLine("There is no Duty defined for you!"); }
                }
                

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n" + "If you wanna go to homepage, please press H!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.H) goto tag1;
            }
            //Logout....
            else if (ck == ConsoleKey.L)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("\n" + "\n" + "Are you sure? (Press Y)");
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("If you wanna go to homepage, please any button!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + "\n" + "Signing out..");
                    Thread.Sleep(1300);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    return;
                }
                else { goto tag1; }
            }
            //Exit App....
            else if (ck == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else
            {
                goto tag1;
            }
        }
        virtual public void showMembers(Hospital obj)
        {
            foreach (var member in obj.members)
            { 
                if (member.GetType() != typeof(Admin))
                {
                    Console.WriteLine("\n" + "\n" + "\n"
                      + "Name                : " + ((Staff)member).getName + "\n"
                      + "Surname             : " + ((Staff)member).getSurname + "\n"
                      + "Occupation          : " + ((Staff)member).getJob);
                    if (member.GetType() == typeof(Doctor))
                    {
                        Console.WriteLine("Speciality          : " + ((Doctor)member).getOccupation);
                    }
                }
            }
        }
    }
    class Doctor : Staff
    {
        string pwz;
        string occupation;
        public string getPwz { get { return pwz; } }
        public string getOccupation { get { return occupation; } }

        public Doctor(string name, string surname, string pesel, string password, string job, string pwz, string occupation) : base(name, surname, pesel, password, job)
        {
            this.pwz = pwz;
            this.occupation = occupation;
        }
    }
    class Nurse : Staff
    {
        public Nurse(string name, string surname, string pesel, string password, string job) : base(name, surname, pesel, password, job) { }

    }
    class Admin : Staff
    {
        public Admin(string name, string surname, string pesel, string password, string job) : base(name, surname, pesel, password, job) { }
       public override void showMembers(Hospital obj)
        {
            foreach (var member in obj.members)
            {       
                Console.WriteLine( "\n" + "\n" + "\n"
                + "Name                : " + ((Staff)member).getName + "\n"
                + "Surname             : " + ((Staff)member).getSurname + "\n"
                + "Pesel(Username)     : " + ((Staff)member).getPesel + "\n"
                + "Password            : " + ((Staff)member).getPassword + "\n"
                + "Occupation          : " + ((Staff)member).getJob

                );
                if (member.GetType() == typeof(Doctor))
                {
                    Console.WriteLine(
                     "PWZ                 : " + ((Doctor)member).getPwz + "\n"
                   + "Speciality          : " + ((Doctor)member).getOccupation + "\n");
                }
            }
        }
        public void EnterencePanel(string nameSurname, Hospital obj)
        {
        tag1:
            Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + "                                                                                " + nameSurname + "\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + "\n" +
                              "To add Admin, please press A                                                                " + "\n" +
                              "To add Doctor, please press D                                                               " + "\n" +
                              "To add Nurse, please press N ;                                                              " + "\n" + "\n" +
                              "To see staff List please press S                                                            " + "\n" + "\n" +
                              "To Add Doctor Duty please press F2 ; To see Duty List of Doctors please press F6            " + "\n" +
                              "To Add Nurse Duty please press F3 ;To see Duty List of Nurses please press F7               " + "\n" + "\n" +
                              "To Exit Application, please press Esc(Escape)                                    ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + "\n" + "\n" + "                                                                                  LOGOUT(PRESS L)");
            Console.ResetColor();
            ConsoleKey ck = Console.ReadKey(true).Key;

            //Add Admin
            if (ck == ConsoleKey.A)
            {
                string pesel;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Admin admin;

            tag2:
                try
                {
                    Console.WriteLine("\n" + "Please enter a name : ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Please enter a surname : ");
                    string surnaname = Console.ReadLine();
                TagP:
                    try
                    {
                        Console.WriteLine("Please enter a PESEL number : ");
                        pesel = Console.ReadLine();
                        long size = pesel.Length;
                        if (size != 11)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter an 11 digit value consisting of numbers" + "\n");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            goto TagP;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Character!" + "\n" + "Please enter an 11 digit value consisting of numbers" + "\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        goto TagP;
                    }

                    Console.WriteLine("Please enter a password : ");
                    string password = Console.ReadLine();
                    admin = new Admin(name, surnaname, pesel, password, "Admin");
                    obj.members.Add(admin);
                    obj.fileWrite();
                    Console.WriteLine("\n" + "The new Admin succesfully added!" + "\n" + "\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                };
                Console.WriteLine("If you wanna add more admin, please press M!");
                Console.WriteLine("If you wanna go to homepage, please press any button!" + "\n");

                if (Console.ReadKey(true).Key == ConsoleKey.M) goto tag2;
                else goto tag1;
            }
            //Add Doctor 
            else if (ck == ConsoleKey.D)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                string pesel;
                Doctor doctor;
            tag2:
                try
                {
                    Console.WriteLine("\n" + "Please enter a name : ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Please enter a surname : ");
                    string surnaname = Console.ReadLine();
                TagP:
                    try
                    {
                        Console.WriteLine("Please enter a PESEL number : ");
                        pesel = Console.ReadLine();
                        long size = pesel.Length;
                        if (size != 11)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter an 11 digit value consisting of numbers" + "\n");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            goto TagP;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Character!" + "\n" + "Please enter an 11 digit value consisting of numbers" + "\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        goto TagP;
                    }
                    Console.WriteLine("Please enter a password : ");
                    string password = Console.ReadLine();
                    Console.WriteLine("Please enter a pwz number : ");
                    string pwz = Console.ReadLine();
                    Console.WriteLine("Please enter a occupation : ");
                    string occupation = Console.ReadLine();
                    doctor = new Doctor(name, surnaname, pesel, password, "Doctor", pwz, occupation);
                    obj.Add(doctor);
                    obj.fileWrite();
                    Console.WriteLine("\n" + "The new Doctor succesfully added!" + "\n" + "\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                Console.WriteLine("If you wanna add more admin, please press M!");
                Console.WriteLine("If you wanna go to homepage, please press any button!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.M) goto tag2;
                else goto tag1;
            }
            //Add Nurse
            else if (ck == ConsoleKey.N)
            {
                string pesel;
                Nurse nurse;
            tag2:
                try
                {
                    Console.WriteLine("\n" + "Please enter a name : ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Please enter a surname : ");
                    string surnaname = Console.ReadLine();
                TagP:
                    try
                    {
                        Console.WriteLine("Please enter a PESEL number : ");
                        pesel = Console.ReadLine();
                        long size = pesel.Length;
                        if (size != 11)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter an 11 digit value consisting of numbers" + "\n");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            goto TagP;
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Character!" + "\n" + "Please enter an 11 digit value consisting of numbers" + "\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        goto TagP;
                    }
                    Console.WriteLine("Please enter a password : ");
                    string password = Console.ReadLine();
                    nurse = new Nurse(name, surnaname, pesel, password, "Nurse");
                    obj.Add(nurse);
                    obj.fileWrite();
                    Console.WriteLine("\n" + "The new Nurse succesfully added!" + "\n" + "\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                Console.WriteLine("If you wanna add more admin, please press M!");
                Console.WriteLine("If you wanna go to homepage, please press any button!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.M) goto tag2;
                else goto tag1;

            }
            //Show Members
            else if (ck == ConsoleKey.S)
            {
                showMembers(obj);
                Console.WriteLine("\n" + "If you wanna go to homepage, please press H!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.H) goto tag1;
            }
            //Show Nurse Duties
            else if (ck == ConsoleKey.F7)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n" + "The Duty List of NURSES' ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                obj.showDuties(1);
                Console.WriteLine("\n" + "If you wanna go to homepage, please press H!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.H) goto tag1;
            }
            //Show Doctor Duties
            else if (ck == ConsoleKey.F6)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n" + "The Duty List of DOCTORS'");
                Console.ForegroundColor = ConsoleColor.Yellow;
                obj.showDuties(2);
                Console.WriteLine("\n" + "If you wanna go to homepage, please press H!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.H) goto tag1;
            }           

            //Add Nurse Duty.........................
            else if (ck == ConsoleKey.F3)
            {   int fark;
                int day;
                bool isFark ;
                DutyList duty;
            tag2:
                isFark = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                try
                {
                    Console.WriteLine("\n" + "Please enter a day : ");
                    try
                    {
                        day = Convert.ToInt32(Console.ReadLine());

                        int days = System.DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                        
                        if (day<=days)
                        {
                            Console.WriteLine("\n" + "Please enter a staff's pesel number : ");
                            string pesel = Console.ReadLine();
                            foreach (var n in obj.members)
                            {
                                if (n.GetType() == typeof(Nurse))
                                {
                                    if (((Nurse)n).getPesel.Equals(pesel))
                                    {
                                        foreach (DutyList dl in obj.getNurseDuties())
                                        {
                                            if (dl.getPesel.Equals(pesel))
                                            {
                                                 fark = Math.Abs(dl.getDay - day);
                                                if (fark > 1) { }
                                                else { isFark = true ;}
                                            }                                         
                                        }
                                         if (isFark==false)
                                         {
                                              duty = new DutyList(day, Convert.ToInt32(DateTime.Now.Month), Convert.ToInt32(DateTime.Now.Year), pesel);
                                              obj.Add(duty);
                                              obj.dutyWrite();
                                              Console.WriteLine("\n" + "The new Nurse Duty succesfully added!" + "\n" + "\n");
                                          }
                                           else
                                           {
                                            Console.WriteLine("\n" + "An employee cannot be registered on the duty list for two consecutive days." + "\n" +
                                            "Please select a different day." + "\n" + "\n"); goto tag2;
                                           }
                                    }
                                }
                           
                            }
                        }
   
                        else 
                        {
                            Console.WriteLine("\n" + "Please enter day of number!"); goto tag2;
                        }
                    }
                    catch (InvalidCastException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                Console.WriteLine("If you wanna add more duty, please press M!");
                Console.WriteLine("If you wanna go to homepage, please press any button!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.M) goto tag2;
                else goto tag1;
            }
            
            //....doctor duty add........
            else if (ck == ConsoleKey.F2)
            {
                int day;
                bool isFark = false;
                bool isControl = false;
                string occupation = "";
                DoctorDuty duty;
            tag2:
                isControl = false;
                isFark = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                try
                {
                    Console.WriteLine("\n" + "Please enter a day : ");
                    
                    try 
                    {
                        day = Convert.ToInt32(Console.ReadLine());
                        int days = System.DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                        if (day <= days)
                        {
                            Console.WriteLine("\n" + "Please enter a staff's pesel number : ");
                            string pesel = Console.ReadLine();
                            foreach (var n in obj.members)
                            {
                                if (n.GetType() == typeof(Doctor) && ((Doctor)n).getPesel.Equals(pesel))
                                {
                                    occupation = ((Doctor)n).getOccupation;
                                    foreach (DoctorDuty dd in obj.getDoctorDuties())
                                    {
                                        if (dd.getPesel.Equals(pesel) )
                                        {   
                                            int fark = Math.Abs(dd.getDay - day);
                                            
                                            if(fark > 1) 
                                            {
                                            }
                                            else 
                                            {
                                                isFark = true ;
                                               
                                            }
                                            
                                            
                                        }
                                         if(dd.getOccupation.Equals(occupation))
                                         {
                                            int fark = Math.Abs(dd.getDay - day);

                                            if(fark==0)
                                            { isControl=true ; }
                                         }
                                        
                                    }
                                    if( isFark==false && isControl == true)
                                    {
                                         Console.WriteLine("\n" + "Two different doctors with the same specialty type cannot be listed on the same day!" + "\n" +
                                                  "Please select a different day."); 
                                        isControl = false ;
                                        goto tag2;
                                    }

                                   else if(isFark==false && isControl==false) 
                                    {
                                              duty = new DoctorDuty(day, Convert.ToInt32(DateTime.Now.Month), Convert.ToInt32(DateTime.Now.Year), pesel,occupation);
                                              obj.Add(duty);
                                              obj.dutyWrite();
                                              Console.WriteLine("\n" + "The new Doctor Duty succesfully added!" + "\n" + "\n");
                                    
                                    }
                                    else
                                           {
                                            Console.WriteLine("\n" + "An employee cannot be registered on the duty list for two consecutive days." + "\n" +
                                            "Please select a different day." + "\n" + "\n"); 
                                        isFark = false ; isControl = false ;
                                        goto tag2;
                                           }
                                   

                                }
                               
                            }


                        }
                        else
                        {
                            Console.WriteLine("\n" + "Please enter day of number!"); goto tag2;
                        }
                    }
                    catch (InvalidCastException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.Message);
                }
                Console.WriteLine("If you wanna add more duty, please press M!");
                Console.WriteLine("If you wanna go to homepage, please press any button!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.M) goto tag2;
                else goto tag1;

            }
            
            else if (ck == ConsoleKey.L)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("\n" + "\n" + "Are you sure? (Press Y)");
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine("If you wanna go to homepage, please any button!" + "\n");
                if (Console.ReadKey(true).Key == ConsoleKey.Y)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n" + "\n" + "Signing out..");
                    Thread.Sleep(1300);
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    return;
                }
                else { goto tag1; }
            }
            else if (ck == ConsoleKey.Escape)
            {
                Environment.Exit(0);
            }
            else
            {
                goto tag1;
            }
        }
    }
}

