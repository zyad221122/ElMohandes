namespace The_Engneering.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName{ get; set; } = string.Empty;
}
