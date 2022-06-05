using HowdyChallenge;
using System.Text.Json;

// See https://aka.ms/new-console-template for more information

string fileName = "data.json";
string jsonString = File.ReadAllText(fileName);
List<Employee> ?data = JsonSerializer.Deserialize<List<Employee>>(jsonString);
if (data == null)
{ 
    Console.WriteLine("Unexpected!");
    Environment.Exit(1);
}
else
{
    try
    {
        Evaluate(data);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        Environment.Exit(1);
    }
}

void Evaluate(List<Employee> data)
{
    List<EmployeeGroup> groups = new List<EmployeeGroup>();
    data.ForEach(e => {
        EmployeeGroup? grp = groups.Find(grp => grp.Id == e.groupId);
        if (grp != null)
        {
            grp.Employees.Add(e);
        }
        else
        {
            grp = new EmployeeGroup();
            grp.Id = e.groupId;
            grp.Employees.Add(e);
            groups.Add(grp);
        }
    });
    groups.OrderByDescending(g => g.EvaluateGroup()).ToList();
    Console.WriteLine($"Highest score has group: {groups[0].Id}. Group value: {groups[0].GroupEvaluation}");
}