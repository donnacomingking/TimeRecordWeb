using TimeRecordWeb.Models;

namespace TimeRecordWeb.Helpers
{
    public interface IUserAPIClient
    {
        string GetToken(User user);
    }
}