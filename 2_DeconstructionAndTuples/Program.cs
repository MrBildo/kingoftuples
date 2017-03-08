using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_DeconstructionAndTuples
{
	class Program
	{
		static void Main(string[] args)
		{
			//In part 1 we looked at the new way Tuples are declared and used in C#7. In part 2 we'll
			//look at the concept of deconstruction and how Tuples provide the support for it's functionality

			//Previously we saw how Tuples are declared using the new syntax seen below:
			var person = (FirstName: "Jane", LastName: "Doe", Age: 33);

			//or by specifying the type:
			(string FirstName, string LastName, int Age) person2 = ("Jane", "Doe", 33);

			//What if you wanted to return a Tuple from a method? The syntax is very similar
			//NOTE: I'm taking advantage of local functions here for illustrative purposes

			(string FirstName, string LastName, int Age) GetPerson()
			{
				return ("Jane", "Doe", 33);
			}

			//This method now returns a strongly-typed Tuple with meaningful field names
			var person3 = GetPerson();
			var firstName = person3.FirstName;

			//As with Tuple declarations there are several valid variations
			//For example you can rename the fields return from a method by simply overriding them in your declaration
			(string FName, string LName, int age) GetPerson2()
			{
				return ("Jane", "Doe", 33);
			}

			(string FirstName, string LastName, int Age) person4 = GetPerson2();
			var lastName = person4.LastName;

			//So what if we didn't care about everything in a Tuple and wanted to pluck out the relevant bits?
			//This is where deconstruction comes into play. You can "deconstruct" a Tuple and grab only the bits you want
			//First, here's what it looks like and then I'll explain the value of this feature

			(double ticks, DateTime lastBoot, OperatingSystem currentOS) GetSystemInformation()
			{
				//this is just theoretical code
				return (67521789523.52D, DateTime.Now, new OperatingSystem(PlatformID.Win32NT, Version.Parse("10.0.0.0")));
			}

			var (i1, i2, currentOperatingSystem) = GetSystemInformation();

			var servicePack = currentOperatingSystem.ServicePack;

			//What is happening is that our GetSystemInformation method is returning a whole bunch of data, however
			//we are only interested in the current operating system. By specifying var(i1, i2, currentOperatingSystem) we 
			//are deconstructing the Tuple returned from the method and only giving the OperatingSystem field semantical meaning.

			//It's important to recognize that this is a bit of a paradigm shift from how data structures have been handled in C#
			//in the past. If you are familiar with how Tuples work in F#, this is similar. The general C#/OOP concept
			//applied in the above example would likely have us create a class, SystemInformation, with properties for Ticks, LastBoot and
			//CurrentOperatingSystem. Given the above example, this class would be nothing more than a hollow data structure, who's only
			//purpose is to store those 3 fields. And if we were only interested in the current operating system, we would be forced to 
			//lug around the container object throughout the rest of our code. 

			//By using Tuples and deconstruction, we are acknowledging that the method supplies a set of useful data: var(i1, i2, currentOperatingSystem),
			//however, beyond the initial method call we can ignore the rest of the data and focus solely on the bit(s) we are interested in.

			//Another feature of deconstruction is that you can design your classes to support deconstruction.
			//Note the Person class defined below.
			//Example usage:
			var myPerson = new Person() { FirstName = "Jane", LastName = "Doe", Age = 33 };

			var(fName, lName, age) = myPerson;

			//Finally, it's possible to "retro-fit" deconstruction on an existing class by using an extension method
			//Note extension method below
			var (platform, version, servicePck) = currentOperatingSystem;
		}
	}

	public class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int Age { get; set; }

		public void Deconstruct(out string firstName, out string lastName, out int age)
		{
			firstName = FirstName;
			lastName = LastName;
			age = Age;
		}
	}

	public static class Extensions
	{
		public static void Deconstruct (this OperatingSystem os,  out string platform, out string version, out string servicePack)
		{
			platform = os.Platform.ToString();
			version = os.VersionString;
			servicePack = os.ServicePack;
		}
	}
}
