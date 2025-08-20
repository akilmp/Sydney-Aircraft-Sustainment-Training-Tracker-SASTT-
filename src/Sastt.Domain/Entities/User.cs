using Sastt.Domain.Enums;

namespace Sastt.Domain.Entities;

public class User : Base
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public RoleType Role { get; set; } = RoleType.Viewer;
}
