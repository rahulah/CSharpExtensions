using static Proinfocus.CSharpExtensions;
// string name = "Rahul";

// name.When(a => a.StartsWith("R"), _ => "Your name starts with the letter 'R'".DumpLine())
//     .When(a => a.Contains("hoo"), _ => "Your name contains the letters 'hoo'".DumpLine())
//     .When(a => a.Length == 5, _ => "Yes, you have 5 letters word!".DumpLine());

// Loop(1, name.Length + 1, i => name[..i].DumpLine());

var employees = new List<Employee>();
Loop(0, 100, x => employees.Add(new Employee { EmployeeNo = x + 1 }));
employees.LoopOver(e => e.EmployeeNo.DumpLine());