namespace OilRigWebApi.Models
{
    public interface UserInterface
    {
        void AddUser(User user);

        void UpdateUser(User user);

        void DeleteUser(string ID);

        User GetUserSingleRecord(string ID);

        List<User> GetUserRecords();
    }
}
