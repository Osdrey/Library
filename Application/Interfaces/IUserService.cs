namespace Library.Application.Interfaces
{
    internal interface IUserService
    {
        void SearchAllUsers();
        void SearchUser();
        void CreateUser();
        void UpdateUser();
        void ReactivateUser();
        void DeactivateUser();
    }
}
