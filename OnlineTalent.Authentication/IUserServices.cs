
namespace OnlineTalent.Authentication
{
    public interface IUserServices
    {
        int Authenticate(string userName, string password);
    }
}
