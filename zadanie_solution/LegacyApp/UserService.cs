using System;

namespace LegacyApp
{
    public class UserService : IUserService
    {
        private readonly IClientRepository _clientRepository;
        private readonly UserCreditService _userCreditService;

        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (!IsValidInput(firstName, lastName, email, dateOfBirth))
                return false;

            var client = _clientRepository.GetById(clientId);
            if (client == null)
                return false;

            int creditLimit = DetermineCreditLimit(client, lastName, dateOfBirth);

            if (creditLimit < 500 && creditLimit > 0)
                return false;

            var user = CreateUser(firstName, lastName, email, dateOfBirth, client, creditLimit);

            SaveUser(user);

            return true;
        }

        private bool IsValidInput(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
                return false;

            if (!email.Contains("@") || !email.Contains("."))
                return false;

            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.Month < dateOfBirth.Month || (DateTime.Now.Month == dateOfBirth.Month && DateTime.Now.Day < dateOfBirth.Day))
                age--;

            if (age < 21)
                return false;

            return true;
        }

        private int DetermineCreditLimit(Client client, string lastName, DateTime dateOfBirth)
        {
            if (client.Type == "VeryImportantClient")
                return 0;

            int creditLimit = _userCreditService.GetCreditLimit(lastName, dateOfBirth);

            if (client.Type == "ImportantClient")
                creditLimit *= 2;

            return creditLimit;
        }

        private User CreateUser(string firstName, string lastName, string email, DateTime dateOfBirth, Client client, int creditLimit)
        {
            return new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName,
                HasCreditLimit = creditLimit > 0,
                CreditLimit = creditLimit
            };
        }

        private void SaveUser(User user)
        {
            UserDataAccess.AddUser(user);
        }
    }
}
