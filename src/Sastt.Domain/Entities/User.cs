using Sastt.Domain.Enums;

namespace Sastt.Domain.Entities;

public class User : Base
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public RoleType Role { get; set; } = RoleType.Viewer;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}
