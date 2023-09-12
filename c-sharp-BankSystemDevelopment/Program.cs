using c_sharp_BankSystemDevelopment;

internal class Program
{
    private static void Main(string[] args)
    {
        UserRegistration userRegistration = new UserRegistration();
        AccountOperations accountOperations = new AccountOperations();
        Menu menu = new Menu(userRegistration, accountOperations);


        //userRegistration.LoadUsersFromJson();
        menu.Start();
    }
}