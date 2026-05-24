namespace Rira.Domain.Entities;

public class User
{
    public int Id { get; private set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NationalCode { get; set; }
    public DateTime BirthDate { get; set; }

}

