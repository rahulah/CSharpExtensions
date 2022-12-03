using static Proinfocus.CSharpExtensions;

// var rahul = new Employee
// {
//     EmployeeNo = 1,
//     Name = "Rahul Hadgal",
//     Email = "rahul@proinfocus.com",
//     Salary = 1,
//     BirthDate = new DateTime(1979, 9, 10)
// };

// var validation = EmployeeValidator.Validate(rahul);
// if (!validation.Result) validation.Errors?.LoopOver(i => i.DumpLine());

public sealed class Employee
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int EmployeeNo { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public decimal Salary { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsRetired { get; set; }
}

public sealed class EmployeeValidator
{
    const string EMPLOYEE_NO = "Employee No expected between 1 and 1000.";
    const string EMPLOYEE_NAME = "Name expected between 3 and 30 characters.";
    const string EMPLOYEE_EMAIL = "A valid Email is required.";
    const string EMPLOYEE_SALARY = "Salary expected between 1 and 100.";
    const string EMPLOYEE_BIRTHDATE = "BirthDate is invalid!";
    public static (bool Result, Dictionary<string, string>? Errors) Validate(Employee model)
    {
        Dictionary<string, string>? errors = new();

        model.EmployeeNo.When(n => !n.Range(1, 1000), _ => errors.Log(nameof(model.Name), EMPLOYEE_NO));
        model.Name.When(n => !n.Range(3, 30), _ => errors.Log(nameof(model.Name), EMPLOYEE_NAME));
        model.Email.When(n => !n.IsValid() || !n.IsEmail(), _ => errors.Log(nameof(model.Email), EMPLOYEE_EMAIL));
        model.Salary.When(n => !n.Range(1, 100), _ => errors.Log(nameof(model.Salary), EMPLOYEE_SALARY));
        model.BirthDate.When(n => {
                int age = (int)(DateTime.Now.Subtract(n).TotalDays / 365);
                return (age < 18 || age > 60);
            }, _ => errors.Log(nameof(model.BirthDate), EMPLOYEE_BIRTHDATE));

        return (errors.Count == 0, errors);
    }
}