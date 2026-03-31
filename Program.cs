using System.Globalization;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Security.Cryptography;
using System.Transactions;
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
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {

                    case 1:
                        //Register New Patient

                        lastPatientIndex++;

                        Console.WriteLine("Enter patient name: ");
                        patientNames[lastPatientIndex] = Console.ReadLine();

                        Console.WriteLine("Enter the diagnose: ");
                        diagnoses[lastPatientIndex] = Console.ReadLine();

                        Console.WriteLine("Enter the department: ");
                        departments[lastPatientIndex] = Console.ReadLine();

                        patientIDs[lastPatientIndex] = "P00" + lastPatientIndex;

                        admitted[lastPatientIndex] = false;
                        assignedDoctors[lastPatientIndex] = "";
                        visitCount[lastPatientIndex] = 0;
                        billingAmount[lastPatientIndex] = 0;

                        Console.WriteLine("patient added Successfully, with patient ID: " + patientIDs[lastPatientIndex]);
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

                                assignedDoctors[i] = doc;
                                visitCount[i]++;
                                admitted[i] = true;
                                Console.WriteLine("Patient admitted successfully and assigned to " + assignedDoctors[i]);
                                Console.WriteLine("This patient has been admitted " + visitCount[i] + " times.");



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

                                double visitCharge = 0; // free for this Discharge

                                Console.WriteLine("Was there a consultation fee? (yes/no)");
                                string free = Console.ReadLine();

                                if (free == "yes")
                                {
                                    Console.WriteLine("Enter consultation fee amount: ");
                                    double amount = double.Parse(Console.ReadLine());

                                    billingAmount[i] += amount;
                                    visitCharge += amount;
                                }

                               
                                Console.WriteLine("Any medication charges? (yes/no)");
                                string medication = Console.ReadLine();

                                if (medication == "yes")
                                {
                                    Console.WriteLine("Enter medication charges: ");
                                    double price = double.Parse(Console.ReadLine());

                                    billingAmount[i] += price;
                                    visitCharge += price;

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

                                Console.WriteLine("Patient name: " + patientNames[i] + ", Patient ID: " + patientIDs[i] + ", Diagnosis: " + diagnoses[i] + ", Department: " + departments[i] + ", Admission status: " + admitted[i] + ", Visit count: " + visitCount[i] + ", total billing amount: " + billingAmount[i] );

                                if (admitted[i] == true) 
                                {
                                    Console.WriteLine("Assigned doctor: " + assignedDoctors[i]);
                                }
                                break ;
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

                        bool HasAdmitted = false;
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (admitted[i] == true)
                            {
                                HasAdmitted = true;
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
                        
                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (assignedDoctors[i] == currentDoctor) // find current Doctor 
                            {
                                doctorFound = true;

                                if (admitted[i] == true)
                                {
                                    assignedDoctors[i] = newDoctor;
                                    Console.WriteLine("Patient name: " + patientNames[i] + " has been transferred to " + newDoctor);
                                }

                                else
                                {
                                    Console.WriteLine("No admitted patient found under this doctor");
                                }
                            }
                        }

                        if (doctorFound == false)
                        {
                            Console.WriteLine("Doctor not found!.");
                        }

                        break;

                case 7:
                        //View Most Visited Patients
                        Console.WriteLine("The most visited patients is: ");

                        for (int visit = 100; visit >= 0; visit--) //عداد تنازلي من الاكثر للاقل
                        {
                            for(int i = 0; i <= lastPatientIndex; i++)
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
                            if (dept.ToLower() == departments[i].ToLower())
                            {
                                patFound = true;
                                Console.WriteLine("Patient in this department: ");
                                Console.WriteLine("==================================");
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
                        
                        int choice = int.Parse (Console.ReadLine());

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
                                    Console.WriteLine("billing amount = " + billingAmount[i]);
                                }

                            }

                            Console.WriteLine("No billing records found for this patient.");

                        }

                        else
                        {
                            Console.WriteLine("Invalid choice");
                        }
                        break;

                case 10:
                        //Exit
                        exit = true;
                        Console.WriteLine("Exiting system...");
                        Console.WriteLine("Thank you for using the Healthcare Management System!");
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
