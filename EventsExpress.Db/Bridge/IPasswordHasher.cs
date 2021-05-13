namespace EventsExpress.Db.Bridge
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password, string salt);

        string GenerateSalt();
    }
}
