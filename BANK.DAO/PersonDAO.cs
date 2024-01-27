using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using BANK.Model;
using BANK.Model.Enums;

namespace BANK.DAO

{

    public class PersonDAO : IPersonDAO
    {
        private List<Person> People { get; set; } = new();
        private List<EmployeeCSVRow> employees { get; set; } = new();
        private List<ClientCSVRow> client { get; set; } = new();
        private const string CsvFilePath = "people.csv";
        private const string EmployeCsvFilePath = "employee.csv";
        private const string ClientCsvFilePath = "client.csv";

        public void save()
        {
            var People = new List<Person>
        {
            new Employee("ID1", "Ime1", "Prezime1", Gender.Male, new byte[] {0x01}, new byte[] {0x02}, true, DateTime.Now, null, "email1@example.com", "Adresa1", "123-456-7890",EmployeeType.CounterWorker),
            new Client("ID2", "Ime2", "Prezime2", Gender.Female, new byte[] {0x01}, new byte[] {0x02}, true, DateTime.Now, null, "email2@example.com", "Adresa2", "123-456-7890",new DateTime()),
            new Client("IDD2", "Ime3", "Prezime3", Gender.Female, new byte[] {0x01}, new byte[] {0x02}, true, DateTime.Today, null, "email3@example.com", "Adresa23", "123-456-7890",new DateTime())

        };
            string path = Path.Combine(Environment.CurrentDirectory, CsvFilePath);
            using (var writer = new StreamWriter(path))
            {
                // Write the CSV header
                writer.WriteLine("Id,Name,SurName,Gender,Password,Salt,isActive,CreateOn,DeleteOn,Email,Address,Phone");

                foreach (var person in People)
                {

                    var line = $"{person.Id},{person.Name},{person.SurName}," +
                        $"{person.Gender},{Convert.ToBase64String(person.Password)}," +
                        $"{Convert.ToBase64String(person.Salt)},{person.isActive}," +
                        $"{person.CreateOn.ToString("o", CultureInfo.InvariantCulture)}," +
                        $"{(person.DeleteOn.HasValue ? person.DeleteOn.Value.ToString("o", CultureInfo.InvariantCulture) : "")}," +
                        $"{person.Email},{person.Address},{person.Phone}";
                    writer.WriteLine(line);


                }

            }
            SaveClientCSV();
            SaveEmployeeCSV();

        }

        public void SaveEmployeeCSV()
        {
            //TODO:izbrisati ovu listu
            var employees = new List<EmployeeCSVRow>
        {
            new EmployeeCSVRow("ID1",EmployeeType.CounterWorker),

        };
            string path = Path.Combine(Environment.CurrentDirectory, EmployeCsvFilePath);
            using (var writer = new StreamWriter(path))
            {
                // Write the CSV header
                writer.WriteLine("UserId,EmployeeType");

                foreach (var person in employees)
                {

                    var line =
                        $"{person.userId},{person.employeeType.ToString()}";

                    writer.WriteLine(line);
                }
            }
        }

        public void SaveClientCSV()
        {   //TODO: obrisat listu kasnije 
            var client = new List<ClientCSVRow>
        {
            new ClientCSVRow("ID2", new DateTime())

        };
            string path = Path.Combine(Environment.CurrentDirectory, ClientCsvFilePath);
            using (var writer = new StreamWriter(path))
            {
                // Write the CSV header
                writer.WriteLine("UserId,DueDate");

                foreach (var person in client)
                {

                    var line =
                        $"{person.userId},{person.dueDate.ToString("o", CultureInfo.InvariantCulture)}";

                    writer.WriteLine(line);
                }
            }
        }



        public void loadEmployee()
        {
            string path = Path.Combine(Environment.CurrentDirectory, EmployeCsvFilePath);

            if (!File.Exists(path))
            {
                Console.WriteLine("Fajl nije pronađen.");
                return;
            }

            employees = new List<EmployeeCSVRow>();

            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');

                    if (values.Length == 2)
                    {
                        var employeeCSVRow = new EmployeeCSVRow(

                        values[0].Trim('"'),
                        Enum.Parse<EmployeeType>(values[1])

                        );

                        employees.Add(employeeCSVRow);
                    }
                }
            }

        }



        public void loadClient()
        {
            string path = Path.Combine(Environment.CurrentDirectory, ClientCsvFilePath);

            if (!File.Exists(path))
            {
                Console.WriteLine("Fajl nije pronađen.");
                return;
            }

            client = new List<ClientCSVRow>();

            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');

                    if (values.Length == 2)
                    {

                        var clientCSVRow = new ClientCSVRow(

                        values[0].Trim('"'),
                        DateTime.Parse(values[1], CultureInfo.InvariantCulture)

                        );

                        client.Add(clientCSVRow);
                    }
                }
            }


        }


        public void load()
        {
            loadEmployee();
            loadClient();
            string path = Path.Combine(Environment.CurrentDirectory, CsvFilePath);

            if (!File.Exists(path))
            {
                Console.WriteLine("Fajl nije pronađen.");
                return;
            }

            People = new List<Person>();

            using (var reader = new StreamReader(path))
            {
                reader.ReadLine();

                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');

                    if (values.Length == 12)
                    {
                        string userId = values[0];
                        var employee = employees.Find(c => c.userId == userId);
                        if (employee != null)
                        {



                            People.Add(new Employee(values[0],
                             values[1],
                             values[2],
                             (Gender)Enum.Parse(typeof(Gender), values[3]),
                             Convert.FromBase64String(values[4]),
                             Convert.FromBase64String(values[5]),
                             bool.Parse(values[6]),
                             DateTime.Parse(values[7], CultureInfo.InvariantCulture),
                             string.IsNullOrEmpty(values[8]) ? (DateTime?)null : DateTime.Parse(values[8], CultureInfo.InvariantCulture),
                             values[9],
                             values[10],
                             values[11],
                             employee.employeeType));
                        }
                        else
                        {
                            var client1 = client.Find(a => a.userId == userId);
                            People.Add(new Client(
                                values[0],
                                values[1],
                                values[2],
                                (Gender)Enum.Parse(typeof(Gender), values[3]),
                                Convert.FromBase64String(values[4]),
                                Convert.FromBase64String(values[5]),
                                bool.Parse(values[6]),
                                DateTime.Parse(values[7], CultureInfo.InvariantCulture),
                                string.IsNullOrEmpty(values[8]) ? (DateTime?)null : DateTime.Parse(values[8], CultureInfo.InvariantCulture),
                                values[9],
                                values[10],
                                values[11],
                                client1!.dueDate
                           ));
                        }

                    }

                }
            }

        }
    }
}




