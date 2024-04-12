using Microsoft.AspNetCore.Identity;

public class RoleEdit
{
    public IdentityRole Role { get; set; }
    public IEnumerable<IdentityUser> Members { get; set; }
    public IEnumerable<IdentityUser> NonMembers { get; set; }
}