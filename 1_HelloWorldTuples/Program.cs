using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1_HelloWorldTuples
{
	class Program
	{
		static void Main(string[] args)
		{
			//BEFORE YOU START: C#7 Tuples require the package System.ValueTuple, so make sure you add that via package manager

			//Tuples in C# were always very useful for when you needed a data structure, but didn't need a class

			var person = new Tuple<string, string, int>("John", "Doe", 35);

			Console.WriteLine($"{person.Item1} {person.Item2} is {person.Item3} years old.");

			//Two of the main reasons people are reluctant to use Tuples are due to it's lengthy declaration syntax
			//and the lack of meaningful field names (Item1, Item2, etc.)

			//Luckily these issues are solved in C#7
			//Let's create the person tuple again

			var person2 = (FirstName: "John", LastName: "Doe", Age: 35);

			Console.WriteLine($"{person2.FirstName} {person2.LastName} is {person2.Age} years old.");

			//As you can see we can now use meaningful field names and get full intellisense support.
			//Also, the declaration of the Tuple was simplified from the previous "legacy" Tuple

			//One difference you may have noticed is that we are not specifying the types, instead we are leaving it
			//up to the compiler exactly as we do with the var keyword (e.g. var i = 0 is an int, based on the compiler)

			//If you need tighter control over the types, you can specify them in the declaration 
			(string FirstName, string LastName, int Age) person3 = ("John", "Doe", 35);

			//You'll notice that I specified the field names as well as the type. This is a constraint by the compiler.
			//For example, in the code below the compiler will warn you that your names on the right side are going to be ignored
			(string, string, int) person4 = (FirstName: "John", LastName: "Doe", Age: 35);

			//The code will compile, but the field names are ignored and by default it reverts to Item1, Item2, etc.
			var firstName = person4.Item1;

			//Finally, on the subject of declaration, if you specify the type, it will be enforced. The following code will not compile:
			(string, string, int) person5 = ("John", "Doe", 10.5M); //cannot convert decimal to int

			//In part 2 we'll look at the concept of deconstruction and how Tuples play a key role in it's use

		}
	}
}
