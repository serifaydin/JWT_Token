using Api_jwt.Model;

namespace Api_jwt.Middleware
{
    public interface IJWTManager
    {
        UserModel AuthenticateUser(UserModel model);
        string GenerateJSONWebToken(UserModel userInfo);
    }
}