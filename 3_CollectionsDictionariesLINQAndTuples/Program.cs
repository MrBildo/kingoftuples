using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace _3_CollectionsDictionariesLINQAndTuples
{
	class Program
	{
		static void Main(string[] args)
		{
			//Arrays, Lists and Dictionaries of Tuples behave like any other data type

			var employees = new(string FirstName, string LastName, int YearsOfService)[]
			{
				("John", "Doe", 10),
				("Jane", "Doe", 11),
				("Fred", "Fuchs", 2)
			};

			foreach(var employee in employees)
			{
				var name = employee.FirstName + " " + employee.LastName;
			}

			//The same is true of generics

			var pets = new List<(string Name, string Type, int Age)>
			{
				("Moosh", "Cat", 7),
				("Rover", "Dog", 8),
				("Puffy", "Chinchilla", 5)
			};

			//Because you can create semantically named strongly-typed data structures on the fly you can represent
			//complex data sets without the need for a supporting classes.
			//This makes Tuples an ideal solution for abstracting JSON or XML data

			var company = GetCompanyInfo();

			//now we have a strongly typed object hierarchy we can manipulate with LINQ

			var hr = company.Departments.Where(d => d.Name == "HR");

			var itEmployees = company.Departments.Where(d => d.Name == "IT").SelectMany(d => d.Employees).OrderByDescending(e => e.Level);

			//Dictionaries are pretty much the same story
			var directors = new Dictionary<string, (string Name, string Title, int Level)>
			{
				{ "HR", ("Jane Doe", "Director", 10) },
				{ "IT", ("Fred Fuchs", "Director", 10) },
				{ "Legal", ("Sue Meeh", "Director", 10) }
			};

		}

		//This syntax make seems a bit busy and you may be asking yourself why we are using Tuples here
		//First, for illustrative purposes
		//Second, we'll get into it more in another example project
		private static (string Company, 
			List<(string Name, 
				List<(string Name, string Title, int Level)>Employees)> Departments) GetCompanyInfo()
		{
			dynamic json = JsonConvert.DeserializeObject(File.ReadAllText("company.json"));

			(string Company, List<(string Name, List<(string Name, string Title, int Level)> Employees)> Departments)
				company = (json.companyName, new List<(string Name, List<(string Name, string Title, int Level)>)>());

			foreach(var jsonDepartment in json.departments)
			{
				(string Name, List<(string Name, string Title, int Level)> Employees) department = (jsonDepartment.name, new List<(string Name, string Title, int Level)>());

				company.Departments.Add(department);

				foreach(var employee in jsonDepartment.employees)
				{
					department.Employees.Add((employee.name, employee.title, employee.level));
				}
			}

			return company;
		}
	}
}
