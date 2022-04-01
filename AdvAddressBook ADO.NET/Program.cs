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
            //addressBookRepo.addNewContactToDataBase(addressBookModel);
            //addressBookRepo.EditExiContactToDataBase(addressBookModel, "poonam");
            //addressBookRepo.deleteExiContactInDataBase("Diksha");
            //addressBookRepo.personBelongingCityOrState();
            //addressBookRepo.CountByCityAndState();
            //addressBookRepo.sortedAlphabeticallyByFirstName();
            addressBookRepo.identifyAddressBookWithNameAndType("Priya", "Friends", "friends address book");
        }
    }
}
