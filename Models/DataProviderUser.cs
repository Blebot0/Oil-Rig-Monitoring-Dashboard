namespace OilRigWebApi.Models
{
    public class DataProviderUser : UserInterface
    {
        public PostgresContext ?_dbContext;
        public void AddUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public void DeleteUser(string ID)
        {
            throw new NotImplementedException();
        }

        public List<User> GetUserRecords()
        {
            throw new NotImplementedException();
        }

        public User GetUserSingleRecord(string ID)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
