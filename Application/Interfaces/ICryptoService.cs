namespace Application.Interfaces;

public interface ICryptoService
{
    string GetMD5Hash(string input);

    bool Equals(string md5, string input);
}
