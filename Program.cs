using System.Globalization;
using System.Transactions;

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
            assignedDoctors [lastPatientIndex] = " ";
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
            assignedDoctors[lastPatientIndex] = " ";
            departments[lastPatientIndex] = "Cardiology";
            visitCount[lastPatientIndex] = 1;
            billingAmount[lastPatientIndex] = 0;

            bool exit = false;
            while (true)
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
                        patientNames[lastPatientIndex + 1] = Console.ReadLine();

                        Console.WriteLine("Enter patient ID: ");
                        patientIDs[lastPatientIndex + 1] = Console.ReadLine();

                        Console.WriteLine("Enter the diagnose: ");
                        diagnoses[lastPatientIndex + 1] = Console.ReadLine();

                        Console.WriteLine("Enter the department: ");
                        departments[lastPatientIndex + 1] = Console.ReadLine();

                        admitted[lastPatientIndex + 1] = true;
                        assignedDoctors[lastPatientIndex + 1] = " ";
                        visitCount[lastPatientIndex + 1] = 0;
                        billingAmount[lastPatientIndex + 1] = 0;

                        Console.WriteLine("patient added.");
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


                                Console.WriteLine("Was there a consultation fee? (yes/no)");
                                string free = Console.ReadLine();

                                if (free == "yes")
                                {
                                    Console.WriteLine("Enter consultation fee amount: ");
                                    double amount = double.Parse(Console.ReadLine());

                                    billingAmount[i] += amount;
                                }

                                Console.WriteLine("Any medication charges? (yes/no)");
                                string medication = Console.ReadLine();

                                if (medication == "yes")
                                {
                                    Console.WriteLine("Enter medication charges: ");
                                    double price = double.Parse(Console.ReadLine());

                                    billingAmount[i] += price;

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
                                assignedDoctors[i] = " ";
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

                        for (int i = 0; i <= lastPatientIndex; i++)
                        {
                            if (admitted[i] == true)
                            {
                                Console.WriteLine("Patient name: " + patientNames[i] + ", Patient ID: " + patientIDs[i] + ", Diagnosis: " + diagnoses[i] + ", Department: " + departments[i] + ", Admission status: " + admitted[i] + ", Visit count: " + visitCount[i] + ", total billing amount: " + billingAmount[i] + ", Assigned doctor: " + assignedDoctors[i]);
                            }
                        }

                        break;

                case 6:
                        break;

                case 7:
                        break;

                case 8:
                        break;

                case 9:
                        break;

                case 10:
                        break;
                
                
                
                }

            }

        }
    }
}
