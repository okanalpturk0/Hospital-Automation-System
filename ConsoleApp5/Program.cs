using ConsoleApp5;
using System.Collections;
using System.Drawing;
using System.Formats.Asn1;
using System.IO;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    static Hospital obj;
    static string nameSurname;
    public static string loginPesel;
    public static string loginOccupation;

    static void Main(string[] args)
    {
        obj = new Hospital();
        obj.readFile();
        obj.readDuties();
        long username;
        string pass;
    //.....LOGIN.....
    Tag:
        try
        {
            Console.ResetColor();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + "\n" + "Username : ");
            string text = Console.ReadLine();
            long size = text.Length;
            username = long.Parse(text);

            if (size != 11)
            {
                Console.WriteLine("Please enter an 11 digit value consisting of numbers" + "\n");
                Thread.Sleep(1300);
                goto Tag;
            }
        }
        catch
        {
            Console.WriteLine("Invalid Character!" + "\n" + "Please enter an 11 digit value consisting of numbers" + "\n");
            Thread.Sleep(1300);
            goto Tag;
        }
        try
        {
            Console.WriteLine("\n" + "Password : ");
            pass = Console.ReadLine();
        }
        catch
        {
            Console.WriteLine("No matching records found!" + "\n");
            Thread.Sleep(1000);
            goto Tag;
        }      
        foreach (Staff staf in obj.members)
        {
            if (staf.getPesel.Equals(username.ToString()) && staf.getPassword.Equals(pass.ToString()))
            {   
                loginOccupation = staf.getJob;
                loginPesel = staf.getPesel;
                if (staf.GetType() == typeof(Admin))
                {
                    nameSurname = " Admin " + staf.getName + " " + staf.getSurname;
                    ((Admin)staf).EnterencePanel(nameSurname, obj);
                    goto Tag;
                }
                else if (staf.GetType() == typeof(Nurse))
                {
                    nameSurname = " Nurse " + staf.getName + " " + staf.getSurname;
                    staf.EnterencePanel(nameSurname, obj);
                }
                else if (staf.GetType() == typeof(Doctor))
                {
                    nameSurname = ((Doctor)staf).getOccupation + " Doctor " + staf.getName + " " + staf.getSurname;
                    staf.EnterencePanel(nameSurname, obj);
                }
            }
        }     
            Console.WriteLine("\n" + "The username or password is incorrect!" + "\n" + "Please try again." + "\n");
            Thread.Sleep(1500);
            goto Tag;
    }
}