using System.Globalization;
using Udemy_LINQ.Entities;

static void Print<T>(string message, IEnumerable<T> collection)
{
    Console.WriteLine(message);
    foreach(T obj in collection)
    {
        Console.WriteLine(obj);
    }
    Console.WriteLine();

}




Console.Write("Enter file name(with extension): ");
string fileName = Console.ReadLine();


var info = new DirectoryInfo(Environment.CurrentDirectory);
var path = info.Parent.Parent.Parent.Parent.FullName + Path.DirectorySeparatorChar;


try
{
    Console.Write("Enter salary: ");
    double value = double.Parse(Console.ReadLine());
    string[] line;
    List<Employee> employees = new List<Employee>();
    using (StreamReader sr = File.OpenText(path + fileName))
    {
        while (!sr.EndOfStream)
        {
            line = sr.ReadLine().Split(';');
            employees.Add(new Employee { Name = line[0], Email = line[1], Salary = double.Parse(line[2],CultureInfo.InvariantCulture) });
           
        }
        Console.WriteLine();
        var biggerThanValue =
             from employee in employees
             where employee.Salary > value
             orderby employee.Name
             select employee.Email;
        Print($"Email of people whose salary is more than {value.ToString("F2", CultureInfo.InvariantCulture)}: ",
            biggerThanValue);


        var sumOfM = employees.Where(p => p.Name.ToUpper()[0] == 'M').Sum(p => p.Salary);

        Console.WriteLine("Sum of salary of people whose name starts with 'M': " + sumOfM.ToString("F2", CultureInfo.InvariantCulture));



    }

}
catch (IOException e)
{
    Console.WriteLine(e.Message);
}