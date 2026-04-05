using System.Diagnostics.Metrics;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Security.Cryptography;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;

namespace Healthcare_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] patientNames = new string[100];
            string[] patientIDs = new string[100];
            string[] diagnoses = new string[100];
            bool[] admitted = new bool[100];       // true = currently admitted
            string[] assignedDoctors = new string[100];
            string[] departments = new string[100];     // e.g. "Cardiology", "Orthopedics"
            int[] visitCount = new int[100];        // how many times admitted
            double[] billingAmount = new double[100];     // total fees owed
            int lastPatientIndex = -1;

            string[] lastVisitDate = new string[100];
            string[] lastDischargeDate = new string[100];
            int[] daysInHospital = new int[100];
            string[] bloodType = new string[100];


            //Data storage

            //Patient 1

            lastPatientIndex++;

            patientNames[lastPatientIndex] = "Ali Hassan";
            patientIDs [lastPatientIndex] = "P001";
            diagnoses [lastPatientIndex] = "Flu";
            admitted [lastPatientIndex] = false;
            assignedDoctors [lastPatientIndex] = "";
            departments [lastPatientIndex] = "General";
            visitCount [lastPatientIndex] = 2;
            billingAmount [lastPatientIndex] = 0;
            lastVisitDate[lastPatientIndex] = "2025-01-10";
            lastDischargeDate[lastPatientIndex] = "2025-01-15";
            daysInHospital[lastPatientIndex] = 12;
            bloodType[lastPatientIndex] = "A+";

            //Patient 2

            lastPatientIndex++;

            patientNames[lastPatientIndex] = "Sara Ahmed";
            patientIDs[lastPatientIndex] = "P002";
            diagnoses[lastPatientIndex] = "Fracture";
            admitted[lastPatientIndex] = true;
            assignedDoctors[lastPatientIndex] = "Dr. Noor";
            departments[lastPatientIndex] = "Orthopedics";
            visitCount[lastPatientIndex] = 4;
            billingAmount[lastPatientIndex] = 0;
            lastVisitDate[lastPatientIndex] = "";
            lastDischargeDate[lastPatientIndex] = "2025-01-15";
            daysInHospital[lastPatientIndex] = 8;
            bloodType[lastPatientIndex] = "O-";

            //Patient 3

            lastPatientIndex++;

            patientNames[lastPatientIndex] = "Omar Khalid";
            patientIDs[lastPatientIndex] = "P003";
            diagnoses[lastPatientIndex] = "Diabetes";
            admitted[lastPatientIndex] = false;
            assignedDoctors[lastPatientIndex] = "";
            departments[lastPatientIndex] = "Cardiology";
            visitCount[lastPatientIndex] = 1;
            billingAmount[lastPatientIndex] = 0;
            lastVisitDate[lastPatientIndex] = "2024-12-20";
            lastDischargeDate[lastPatientIndex] = "2024-12-28";
            daysInHospital[lastPatientIndex] = 5;
            bloodType[lastPatientIndex] = "B+";

            lastPatientIndex++;


            bool exit = false;
            while (exit == false)
            {
                Console.WriteLine("===== Healthcare Management System =====");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("1. Register New Patient.");
                Console.WriteLine("2. Admit Patient.");
                Console.WriteLine("3. Discharge Patient.");
                Console.WriteLine("4. Search Patient.");
                Console.WriteLine("5. List All dmitted Patients.");
                Console.WriteLine("6. Transfer Patient to Another Doctor.");
                Console.WriteLine("7. View Most Visited Patients.");
                Console.WriteLine("8. Search Patients by Department.");
                Console.WriteLine("9. Billing Report.");
                Console.WriteLine("10. Exit.");

                Console.Write("Choose option: ");


                int option = 0;
                try
                {
                    option = int.Parse(Console.ReadLine());
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Invalid input. Please choose a number from 1 to 10.");
                }

                switch (option)
                {

                    case 1:
                        //Register New Patient

                        Console.WriteLine("Enter patient name: ");
                        patientNames[lastPatientIndex] = Console.ReadLine();

                        Console.WriteLine("Enter the diagnose: ");
                        diagnoses[lastPatientIndex] = Console.ReadLine();

                        Console.WriteLine("Enter the department: ");
                        departments[lastPatientIndex] = Console.ReadLine();

                        Console.WriteLine("Enter the blood type: ");
                        bloodType[lastPatientIndex] = Console.ReadLine();

                        patientIDs[lastPatientIndex] = "P" + (lastPatientIndex + 1).ToString("D3");

                        admitted[lastPatientIndex] = false;
                        assignedDoctors[lastPatientIndex] = "";
                        visitCount[lastPatientIndex] = 0;
                        billingAmount[lastPatientIndex] = 0;
                        lastDischargeDate[lastPatientIndex] = "";
                        lastVisitDate[lastPatientIndex] = DateTime.Now.ToString("yyyy-MM-dd");
                        daysInHospital[lastPatientIndex] = 0;
                        Console.WriteLine("patient added Successfully, with patient ID: " + patientIDs[lastPatientIndex]);

                        lastPatientIndex++;

                        break;
                    case 2:
                        //Admit Patient
                        Console.WriteLine("Enter patient ID or patient name: ");
                        string Input = Console.ReadLine();

                        bool Found = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (Input == patientIDs[i] || Input == patientNames[i])
                            {
                                Found = true; // patient found

                                if (admitted[i] == true) // patient already admit
                                {
                                    Console.WriteLine("Patient is already admitted under " + assignedDoctors[i]);
                                    break;
                                }

                                //new admit patient

                                Console.WriteLine("Enter doctor name: ");
                                string doc = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(doc)) // Empty value is not allowed
                                {
                                    Console.WriteLine("Doctor name cannot be empty.");
                                    break;
                                }

                                assignedDoctors[i] = doc;
                                visitCount[i]++;
                                lastVisitDate[i] = DateTime.Now;
                                admitted[i] = true;

                                if (visitCount[i] > 1)
                                {
                                    Console.WriteLine("Patient admitted successfully and assigned to " + assignedDoctors[i]);
                                    Console.WriteLine("This patient has been admitted " + visitCount[i] + " times.");
                                }

                                else
                                {
                                    Console.WriteLine("This is the frist visit.");
                                }
                                
                                break;

                            }

                        }

                        if (Found == false)
                        {
                            Console.WriteLine("patient not found");
                        }

                        break;

                    case 3:
                        //Discharge Patient
                        Console.WriteLine("Enter patient ID or patient name: ");
                        string input = Console.ReadLine();

                        bool patientFound = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (input == patientIDs[i] || input == patientNames[i])
                            {
                                patientFound = true; // patient found

                                if (admitted[i] == false)
                                {
                                    Console.WriteLine("This patient is not currently admitted.");
                                    break;
                                }

                                double visitCharge = 0; // fee for this Discharge

                                Console.WriteLine("Was there a consultation fee? (yes/no)");
                                string fee = Console.ReadLine();

                                if (fee == "yes")
                                {
                                    Console.WriteLine("Enter consultation fee amount: ");
                                    double amount = 0;
                                    try
                                    {
                                        amount = double.Parse(Console.ReadLine());

                                        if (amount > 0)
                                        {
                                            billingAmount[i] += amount;
                                            visitCharge += amount;
                                        }
                                        else
                                        {
                                            Console.WriteLine("fee amount must be posititve");
                                        }
                                    }

                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                                    }
                                }


                                Console.WriteLine("Any medication charges? (yes/no)");
                                string medication = Console.ReadLine();

                                if (medication == "yes")
                                {
                                    Console.WriteLine("Enter medication charges: ");
                                    double price = 0;
                                    try
                                    {
                                        price = double.Parse(Console.ReadLine());

                                        if (price > 0)
                                        {
                                            billingAmount[i] += price;
                                            visitCharge += price;
                                        }
                                        else
                                        {
                                            Console.WriteLine("fee price must be posititve");
                                        }
                                    }

                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Invalid price. Please enter a valid number.");
                                    }

                                }

                                if (billingAmount[i] > 0)
                                {

                                    Console.WriteLine("Total charges added this visit: " + billingAmount[i] + " OMR");
                                }


                                else
                                {
                                    Console.WriteLine("No charges recorded");
                                }

                                admitted[i] = false;
                                assignedDoctors[i] = "";
                                Console.WriteLine("Patient discharged successfully!");
                            }
                        }


                        if (patientFound == false)
                        {
                            Console.WriteLine("patient not found");
                        }

                        break;

                    case 4:
                        //Search Patient
                        Console.WriteLine("Enter patient ID or patient name: ");
                        string patientInput = Console.ReadLine();

                        bool PatientFound = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (patientInput == patientIDs[i] || patientInput == patientNames[i])
                            {
                                PatientFound = true; // patient found

                                Console.WriteLine("Patient name: " + patientNames[i] + ", Patient ID: " + patientIDs[i] + ", Diagnosis: " + diagnoses[i] + ", Department: " + departments[i] + ", Admission status: " + admitted[i] + ", Visit count: " + visitCount[i] + ", total billing amount: " + billingAmount[i]);

                                if (admitted[i] == true)
                                {
                                    Console.WriteLine("Assigned doctor: " + assignedDoctors[i]);
                                }

                                string visitDateDisplay;
                                if (lastVisitDate[i] == DateTime.MinValue)
                                {
                                    visitDateDisplay = "No admission recorded";
                                }
                                else
                                {
                                    visitDateDisplay = lastVisitDate[i].ToString ("yyyy-MM-dd");
                                }
                                Console.WriteLine("Last Visit Date: " + visitDateDisplay);
                                break;
                            }
                        }

                        if (PatientFound == false)
                        {
                            Console.WriteLine("patient not found");
                        }

                        break;
                    case 5:
                        //List All Admitted Patients
                        Console.WriteLine("Admitted Patients: ");

                        int Count = 0;
                        bool HasAdmitted = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (admitted[i] == true)
                            {   
                                HasAdmitted = true;
                                Count++;
                                Console.WriteLine("The  total admitted count is: " + Count);
                                Console.WriteLine("Patient name: " + patientNames[i] + ", Patient ID: " + patientIDs[i] + ", Diagnosis: " + diagnoses[i] + ", Department: " + departments[i] + ", Admission status: " + admitted[i] + ", Visit count: " + visitCount[i] + ", total billing amount: " + billingAmount[i] + ", Assigned doctor: " + assignedDoctors[i]);
                            }
                        }

                        if (HasAdmitted == false)
                        {
                            Console.WriteLine("No patient admitted.");
                        }

                        break;

                    case 6:
                        //Transfer Patient to Another Doctor
                        Console.WriteLine("Enter current doctor name: ");
                        string currentDoctor = Console.ReadLine();

                        Console.WriteLine("Enter new doctor name: ");
                        string newDoctor = Console.ReadLine();

                        bool doctorFound = false;
                        bool admittedPatientFound = false; 

                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (assignedDoctors[i] == currentDoctor) // find current Doctor 
                            {
                                doctorFound = true;

                                if (admitted[i] == true)
                                {
                                    admittedPatientFound = true;
                                    if (newDoctor != currentDoctor)
                                    {
                                        assignedDoctors[i] = newDoctor;
                                        Console.WriteLine("Patient name: " + patientNames[i] + " has been transferred to " + newDoctor);
                                       
                                    }

                                    else
                                    {
                                        Console.WriteLine("Doctor names must be different!");
                                        break;
                                    }

                                }

                            }
                        }

                        if (doctorFound == false)
                        {
                            Console.WriteLine("Doctor not found!.");
                        }

                        else if (!admittedPatientFound)
                        {
                            Console.WriteLine("No admitted patient found under this doctor");
                        }

                            break;

                    case 7:
                        //View Most Visited Patients
                        Console.WriteLine("The most visited patients is: ");

                        for (int visit = 100; visit >= 0; visit--) //عداد تنازلي من الاكثر للاقل
                        {
                            for (int i = 0; i <= lastPatientIndex; i++)
                            {
                                if (visitCount[i] == visit) //ترتيب المرضى على حسب عدد مرات الزيارة
                                {
                                    Console.WriteLine("Patient name: " + patientNames[i] + ", Patient ID: " + patientIDs[i] + ", Diagnosis: " + diagnoses[i] + ", Department: " + departments[i] + ", Visit count: " + visitCount[i]);
                                }
                            }
                        }

                        break;

                    case 8:
                        //Search Patients by Department
                        Console.WriteLine("Enter department name: ");
                        string dept = Console.ReadLine();

                        bool patFound = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (departments[i] != null && departments[i].ToLower().Contains(dept.ToLower()))
                            {
                                patFound = true;

                                string AdmissionStatus = admitted[i] ? "Admitted" : "Not Admitted";
                                Console.WriteLine("Patient name: " + patientNames[i] + ", Patient ID: " + patientIDs[i] + ", Diagnosis: " + diagnoses[i] + ", Status: " + AdmissionStatus);
                            }
                        }

                        if (!patFound)
                        {
                            Console.WriteLine("No patients found in this department");
                        }

                        break;

                    case 9:
                        //Billing Report
                        Console.WriteLine("===== Sub Menu =====");
                        Console.WriteLine("1. System billing amount.");
                        Console.WriteLine("2. Individual patient billing.");
                        Console.WriteLine("Enter your choice: ");

                        int choice = 0;
                        bool choiceFound = false;

                        while (!choiceFound)
                        {
                            try
                            {
                                choice = int.Parse(Console.ReadLine());
                                if (choice == 1 || choice == 2)
                                {
                                    choiceFound = true; // خروج من اللوب
                                }
                                else
                                {
                                    Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                                }
                            }

                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                Console.WriteLine("Invalid input.Please enter 1 or 2.");
                            }
                        }
                            // بعد ما يدخل خيار صحيح فقط

                            if (choice == 1)
                            {
                                double TotalAmount = 0;
                                for (int i = 0; i <= lastPatientIndex; i++)
                                {
                                    TotalAmount += billingAmount[i];
                                }

                                Console.WriteLine("Total amount = " + TotalAmount + " OMR");
                            }

                            else if (choice == 2)
                            {
                                Console.WriteLine("Enter patient ID or patient name: ");
                                string inputPatient = Console.ReadLine();

                                bool found = false;
                                for (int i = 0; i <= lastPatientIndex; i++)
                                {
                                    if (inputPatient == patientIDs[i] || inputPatient == patientNames[i])
                                    {
                                        found = true;
                                    if (billingAmount[i] > 0)
                                    {
                                        Console.WriteLine("billing amount = " + billingAmount[i]);
                                        break; // stop after found patient
                                    }

                                    else
                                    {
                                        Console.WriteLine("'No billing records!");
                                    }
                                       
                                    }

                                }

                                if (!choiceFound)
                                {
                                    Console.WriteLine("No billing records found for this patient.");
                                }
                            }
                        
                
                        break;

                case 10:
                        //Exit
                 
                        Console.WriteLine("Are you sure you want to exit? (yes/no): ");
                        string confirmExit = Console.ReadLine();

                        if (confirmExit == "yes")
                        {
                            exit = true;
                            Console.WriteLine("Exiting system...");
                            Console.WriteLine("Thank you for using the Healthcare Management System!");
                            
                        }

                        else
                        {
                            Console.WriteLine("Returning to menu...");
                        }

                        break;
                default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                
                }

                Console.WriteLine("Press any key to continue....");
                Console.ReadKey();
                Console.Clear();

            }

        }
    }
}
