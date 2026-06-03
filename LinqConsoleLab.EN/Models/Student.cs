namespace LinqConsoleLab.EN.Models;
public sealed class Student
{
    public int Id { get; set; }
    public string IndexNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}
