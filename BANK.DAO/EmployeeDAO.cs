using System;
using BANK.Model;
using BANK.Model.Enums;

namespace BANK.DAO
{
	public class EmployeeDAO : IEmployeeDAO
	{


		List<Employee> employees { get; set; } = new();
		private const string EmployeePath = "employee.csv";


		public void save() {


			var employees = new List<Employee>
			{
				new Employee("ID4", "Amir", "Begic", Gender.Male, new byte[] {0x01}, new byte[] {0x02}, true, DateTime.Now, null, "amir@example.com", "Adresa1", "223-456-7890",EmployeeType.ITSpecialists),
				new Employee("ID5", "Sara", "Hadzic", Gender.Female, new byte[] {0x01}, new byte[] {0x02}, true, DateTime.Now, null, "saraexample.com", "Adresa2", "333-456-7890",EmployeeType.Manager),
				new Employee("ID6", "Damir", "Heco", Gender.Male, new byte[] {0x01}, new byte[] {0x02}, true, DateTime.Now, null, "damir1@example.com", "Adresa1", "444-456-7890",EmployeeType.Administrator),
				new Employee("ID7", "Sabina", "Karahodzic", Gender.Female, new byte[] {0x01}, new byte[] {0x02}, true, DateTime.Now, null, "sabina@example.com", "Adresa44", "123-666-7890",EmployeeType.Director),
				new Employee("ID8", "Mirza", "Sinanovic", Gender.Male, new byte[] {0x01}, new byte[] {0x02}, true, DateTime.Now, null, "mirza@example.com", "Adresa55", "444-456-7890",EmployeeType.CounterWorker),

			};


			string path = Path.Combine(Environment.CurrentDirectory, EmployeePath);
			Console.WriteLine($"{path}");


			using (var writer = new StreamWriter(path)) {

				writer.WriteLine("Id,Name,Surname,Gender,Password,Salt,IsActive,CreateOn,DeleteOn,EmaiL,Address,Phone,EmployeeType");

				foreach (var employee in employees) {

					writer.WriteLine($"\"{employee.Id}\",{employee.Name},{employee.SurName},{employee.Gender},{employee.Password},{employee.Salt},{employee.isActive},{employee.CreateOn},{employee.DeleteOn},{employee.Email},{employee.Address},{employee.Phone},{employee.EmployeeType}");
				}

			}


		}



		
	}
}




















			
    


