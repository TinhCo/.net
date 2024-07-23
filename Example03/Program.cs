namespace Example03
{
    class Program
    {
        static void Main(string[] args)
        {
            EmployeeContext context = new EmployeeContext();
            Employee e = new Employee();
            e.FirstName = "Tu";
            e.LastName = "Nguyen";
            e.EmailId = "info@nguyentu.com";
            context.Add(e);
            context.SaveChanges();
            e = new Employee();
            e.FirstName = "Phat";
            e.LastName = "Huyen";
            e.EmailId = "info@huynhphat.com";
            context.Add(e);
            context.SaveChanges();
            Console.WriteLine("-----Loading all data-----");
            List<Employee> list = context.Employees.ToList();
            foreach (Employee item in list)
            {
                Console.WriteLine(item.FirstName);
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.EmailId);
            }
            Console.WriteLine("-----Loading a single entity-----");
            Employee single = context.Employees.Single(b => b.Id == 11);
            Console.WriteLine(single.FirstName);
            Console.WriteLine(single.LastName);
            Console.WriteLine(single.EmailId);
            Console.WriteLine("-----Loading with Filtering-----");
            List<Employee> filters = context.Employees.Where(b => b.FirstName.Contains("Tu")).ToList();
            foreach (Employee item in filters)
            {
                Console.WriteLine(item.FirstName);
                Console.WriteLine(item.LastName);
                Console.WriteLine(item.EmailId);
            }
        }
    }
}