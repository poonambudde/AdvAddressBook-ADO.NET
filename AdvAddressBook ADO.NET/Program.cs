using System;

namespace AdvAddressBook_ADO.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome TO AddressBook Problems Using ADO.NET");

            AddressBookRepo addressBookRepo = new AddressBookRepo();
            AddressBookModel addressBookModel = new AddressBookModel();
            addressBookModel.FirstName = "Diksha";
            addressBookModel.LastName = "Budde";
            addressBookModel.Address = "Vakil Colony";
            addressBookModel.City = "latur";
            addressBookModel.State = "Maharashtra";
            addressBookModel.Zip = 413512;
            addressBookModel.PhoneNumber = 8149713160;
            addressBookModel.Email = "poonam@gmail.com";
            addressBookModel.AddressBookName = "friend address book";
            addressBookModel.AddressBookType = "Friend";
            
            addressBookRepo.DataBaseConnection();
            addressBookRepo.addNewContactToDataBase(addressBookModel);
            addressBookRepo.EditExiContactToDataBase(addressBookModel, "poonam");
            addressBookRepo.deleteExiContactInDataBase("Diksha");
            addressBookRepo.personBelongingCityOrState();
            addressBookRepo.CountByCityAndState();
            addressBookRepo.sortedAlphabeticallyByFirstName();
            addressBookRepo.identifyAddressBookWithNameAndType("Priya", "Friends", "friends address book");
            addressBookRepo.getNumberOfPersonCountByType();

            AddressBookModel addressBookModel1 = new AddressBookModel();
            addressBookModel1.FirstName = "Poonam";
            addressBookModel1.LastName = "budde";
            addressBookModel1.Address = "shivaji chawk";
            addressBookModel1.City = "latur";
            addressBookModel1.State = "Maharashtra";
            addressBookModel1.Zip = 413512;
            addressBookModel1.PhoneNumber = 8149713160;
            addressBookModel1.Email = "poonam@gmail.com";
            addressBookModel1.AddressBookName = "friend address book";
            addressBookModel1.AddressBookType = "Friends";

            addressBookModel1.FirstName = "Poonam";
            addressBookModel1.LastName = "budde";
            addressBookModel1.Address = "shivaji chawk";
            addressBookModel1.City = "latur";
            addressBookModel1.State = "Maharashtra";
            addressBookModel1.Zip = 413512;
            addressBookModel1.PhoneNumber = 8149713160;
            addressBookModel1.Email = "poonam@gmail.com";
            addressBookModel1.AddressBookName = "family address book";
            addressBookModel1.AddressBookType = "Family";

            addressBookRepo.addPersonToBothFriendAndFamily(addressBookModel1);
        }
    }
}
