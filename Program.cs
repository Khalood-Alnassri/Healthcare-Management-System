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

            patientNames[lastPatientIndex] = "Ali Hassan";
            patientIDs [lastPatientIndex] = "P001";
            diagnoses [lastPatientIndex] = "Flu";
            admitted [lastPatientIndex] = false;
            assignedDoctors [lastPatientIndex] = " ";
            departments [lastPatientIndex] = "General";
            visitCount [lastPatientIndex] = 2;
            billingAmount [lastPatientIndex] = 0;
            lastPatientIndex++;

            //Patient 2

            patientNames[lastPatientIndex] = "Sara Ahmed";
            patientIDs[lastPatientIndex] = "P002";
            diagnoses[lastPatientIndex] = "Fracture";
            admitted[lastPatientIndex] = true;
            assignedDoctors[lastPatientIndex] = "Dr. Noor";
            departments[lastPatientIndex] = "Orthopedics";
            visitCount[lastPatientIndex] = 4;
            billingAmount[lastPatientIndex] = 0;
            lastPatientIndex++;

            //Patient 3

            patientNames[lastPatientIndex] = "Omar Khalid";
            patientIDs[lastPatientIndex] = "P003";
            diagnoses[lastPatientIndex] = "Diabetes";
            admitted[lastPatientIndex] = false;
            assignedDoctors[lastPatientIndex] = " ";
            departments[lastPatientIndex] = "Cardiology";
            visitCount[lastPatientIndex] = 1;
            billingAmount[lastPatientIndex] = 0;
            lastPatientIndex++;

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
                
                
                
                
                
                }

            }

        }
    }
}
