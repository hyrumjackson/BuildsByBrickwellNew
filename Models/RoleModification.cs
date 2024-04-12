using System.ComponentModel.DataAnnotations;

public class RoleModification
{
    [Required]
    public string RoleName { get; set; }

    public string RoleId { get; set; }

    public string[]? AddIds { get; set; }

    public string[]? DeleteIds { get; set; }
}