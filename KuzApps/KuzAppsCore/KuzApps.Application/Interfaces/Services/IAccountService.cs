namespace KuzApps.Application.Interfaces.Services;

public interface IAccountService
{
    Task<AccountIndexWebModel> GetIndexData(string? userName);
    Task<(bool success, string[] errors)> RequestRegisterUser(AccountRegisterWebModel model);
    Task<bool> LoginPasswordSignIn(AccountLoginWebModel model);
    Task<AccountEditWebModel?> GetEditData(string? userName);
    Task<(bool success, string[] errors)> RequestUpdateUser(string? userName, AccountEditWebModel model);
    Task<(bool success, string[] errors)> ChangeUserPassword(string? userName, AccountPasswordWebModel model);
    Task SignOut();
    Task<bool> UserNameIsFree(string userName);
}
