using System.Threading.Tasks;
using Out_of_Office.Domain.Entities;
using Out_of_Office.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
        {
            return null;
        }

        return user;
    }

    public async Task RegisterAsync(string username, string password, int employeeId, string position)
    {
        var user = new User
        {
            Username = username,
            PasswordHash = _passwordHasher.HashPassword(null, password),
            EmployeeId = employeeId,
            Role = GetRoleByPosition(position)
        };

        await _userRepository.AddAsync(user);
    }
    private string GetRoleByPosition(string position)
    {
        return position switch
        {
            "HR Manager" => "HRManager",
            "Project Manager" => "ProjectManager",
            "Administrator" => "Administrator",
            _ => "Employee"
        };
    }
}