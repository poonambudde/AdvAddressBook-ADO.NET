using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AdvAddressBook_ADO.NET
{
    public class AddressBookRepo
    {
        //Give path for Database Connection
        public static string connectionString = @"Data Source=(Localdb)\MSSQLLocalDB;Initial Catalog=AddressBook_Service;Integrated Security=True";
        //Represents a connection to Sql Server Database
        SqlConnection connection = new SqlConnection(connectionString);

        public SqlCommand SqlCommand { get; private set; }

        // Checks the connection.
        public void DataBaseConnection()
        {
            try
            {
                this.connection.Open();
                Console.WriteLine("connection established");
                this.connection.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // Adds the new contact to data base.
        public bool addNewContactToDataBase(AddressBookModel addressBookModel)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand cmd = new SqlCommand("SpAddAddressBookDetails", this.connection);
                    //setting command type as stored procedure
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", addressBookModel.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", addressBookModel.LastName);
                    cmd.Parameters.AddWithValue("@Address", addressBookModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressBookModel.City);
                    cmd.Parameters.AddWithValue("@State", addressBookModel.State);
                    cmd.Parameters.AddWithValue("@Zip", addressBookModel.Zip);
                    cmd.Parameters.AddWithValue("@PhoneNumber", addressBookModel.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", addressBookModel.Email);
                    cmd.Parameters.AddWithValue("@AddressBookName", addressBookModel.AddressBookName);
                    cmd.Parameters.AddWithValue("@AddressBookType", addressBookModel.AddressBookType);
                    this.connection.Open();
                    //Return the number of rows updated
                    var result = cmd.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        // Edits the existing contact to data base.
        public bool EditExiContactToDataBase(AddressBookModel addressBookModel, string firstName)
        {
            try
            {
                using (this.connection)
                {
                    string query = @"update AddressBook_Table set lastname=@LastName,address=@Address,city=@City,
                    state=@State,zip=@Zip,phonenumber=@PhoneNumber,email=@Email,addressbookname=@AddressBookName,
                    addressbooktype=@AddressBookType  where FirstName=@firstName";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", addressBookModel.LastName);
                    cmd.Parameters.AddWithValue("@Address", addressBookModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressBookModel.City);
                    cmd.Parameters.AddWithValue("@State", addressBookModel.State);
                    cmd.Parameters.AddWithValue("@Zip", addressBookModel.Zip);
                    cmd.Parameters.AddWithValue("@PhoneNumber", addressBookModel.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", addressBookModel.Email);
                    cmd.Parameters.AddWithValue("@AddressBookName", addressBookModel.AddressBookName);
                    cmd.Parameters.AddWithValue("@AddressBookType", addressBookModel.AddressBookType);
                    this.connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        // Deletes the existing contact in data base.
        public bool deleteExiContactInDataBase(string firstName)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand cmd = new SqlCommand("SpAddAddressBookDetailsForDelete", this.connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    this.connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        // Person belonging to city or state.
        public void personBelongingCityOrState()
        {
            try
            {
                AddressBookModel addressBookModel = new AddressBookModel();
                using (this.connection)
                {
                    string query = @"select * from AddressBook_Table where city='mumbai' Or state='andhra';";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            addressBookModel.FirstName = sqlDataReader.GetString(0); ;
                            addressBookModel.LastName = sqlDataReader.GetString(1);
                            addressBookModel.Address = sqlDataReader.GetString(2);
                            addressBookModel.City = sqlDataReader.GetString(3);
                            addressBookModel.State = sqlDataReader.GetString(4);
                            addressBookModel.Zip = sqlDataReader.GetInt64(5);
                            addressBookModel.PhoneNumber = sqlDataReader.GetInt64(6);
                            addressBookModel.Email = sqlDataReader.GetString(7);
                            addressBookModel.AddressBookName = sqlDataReader.GetString(8);
                            addressBookModel.AddressBookType = sqlDataReader.GetString(9);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", addressBookModel.FirstName, addressBookModel.LastName, addressBookModel.Address, addressBookModel.City, addressBookModel.State, addressBookModel.Zip, addressBookModel.PhoneNumber, addressBookModel.Email, addressBookModel.AddressBookName, addressBookModel.AddressBookType);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        // Count By City And State.
        public void CountByCityAndState()
        {
            try
            {
                AddressBookModel addressBookModel = new AddressBookModel();
                using (this.connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"select COUNT(*) as CityCount, City from AddressBook_Table group by City;
                        select COUNT(*) as StateCount, State from AddressBook_Table group by State;; ", connection))
                    {
                        this.connection.Open();
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                addressBookModel.City = sqlDataReader.GetString(0);
                                int countCIty = sqlDataReader.GetInt32(1);
                                Console.WriteLine("{0},{1}", addressBookModel.City, countCIty);
                                Console.WriteLine("\n");
                            }
                            if (sqlDataReader.NextResult())
                            {
                                while (sqlDataReader.Read())
                                {
                                    addressBookModel.State = sqlDataReader.GetString(0);
                                    int stateCount = sqlDataReader.GetInt32(1);
                                    Console.WriteLine("{0},{1}", addressBookModel.State, stateCount);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        // sorted Alphabetically By First Name.
        public void sortedAlphabeticallyByFirstName()
        {
            try
            {
                AddressBookModel addressBookModel = new AddressBookModel();
                using (this.connection)
                {
                    string query = @"select * from AddressBook_Table where City = 'Latur' order by FirstName,LastName;;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    this.connection.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            addressBookModel.FirstName = sqlDataReader.GetString(0); ;
                            addressBookModel.LastName = sqlDataReader.GetString(1);
                            addressBookModel.Address = sqlDataReader.GetString(2);
                            addressBookModel.City = sqlDataReader.GetString(3);
                            addressBookModel.State = sqlDataReader.GetString(4);
                            addressBookModel.Zip = sqlDataReader.GetInt64(5);
                            addressBookModel.PhoneNumber = sqlDataReader.GetInt64(6);
                            addressBookModel.Email = sqlDataReader.GetString(7);
                            addressBookModel.AddressBookName = sqlDataReader.GetString(8);
                            addressBookModel.AddressBookType = sqlDataReader.GetString(9);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", addressBookModel.FirstName, addressBookModel.LastName, addressBookModel.Address, addressBookModel.City, addressBookModel.State, addressBookModel.Zip, addressBookModel.PhoneNumber, addressBookModel.Email, addressBookModel.AddressBookName, addressBookModel.AddressBookType);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Data Found");
                    }
                    sqlDataReader.Close();
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        // Identify AddressBook With Name And Type.
        public bool identifyAddressBookWithNameAndType(string firstName, string addressType, string adressName)
        {
            try
            {
                using (this.connection)
                {
                    string query = @"update AddressBook_Table set AddressBookName=@AddressBookName , AddressBookType=@AddressBookType where FirstName=@FirstName;";
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@AddressBookType", addressType);
                    cmd.Parameters.AddWithValue("@AddressBookName", adressName);
                    this.connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        // Get Number Of Person Count By Type.
        public void getNumberOfPersonCountByType()
        {
            try
            {
                AddressBookModel addressBookModel = new AddressBookModel();
                using (this.connection)
                {
                    using (SqlCommand command = new SqlCommand(
                        @"select count(AddressBookType) as 'NumberOfContacts' from AddressBook_Table where AddressBookType='Friends';
                        select count(AddressBookType) as 'NumberOfContacts' from AddressBook_Table where AddressBookType='Family';", connection))
                    {
                        this.connection.Open();
                        using (SqlDataReader sqlDataReader = command.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                var count = sqlDataReader.GetInt32(0);
                                Console.WriteLine("Number of person belonging to adress book type friend = {0}", count);
                                Console.WriteLine("\n");
                            }
                            if (sqlDataReader.NextResult())
                            {
                                while (sqlDataReader.Read())
                                {
                                    var count = sqlDataReader.GetInt32(0);
                                    Console.WriteLine("Number of person belonging to adress book type family= {0}", count);
                                    Console.WriteLine("\n");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }

        // Add Person to both friend and family.
        public bool addPersonToBothFriendAndFamily(AddressBookModel addressBookModel)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand cmd = new SqlCommand("SpAddAddressBookDetails", this.connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", addressBookModel.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", addressBookModel.LastName);
                    cmd.Parameters.AddWithValue("@Address", addressBookModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressBookModel.City);
                    cmd.Parameters.AddWithValue("@State", addressBookModel.State);
                    cmd.Parameters.AddWithValue("@Zip", addressBookModel.Zip);
                    cmd.Parameters.AddWithValue("@PhoneNumber", addressBookModel.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", addressBookModel.Email);
                    cmd.Parameters.AddWithValue("@AddressBookName", addressBookModel.AddressBookName);
                    cmd.Parameters.AddWithValue("@AddressBookType", addressBookModel.AddressBookType);
                    this.connection.Open();
                    var result = cmd.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }

                    SqlCommand cmd1 = new SqlCommand("SpAddAddressBookDetails", this.connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FirstName", addressBookModel.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", addressBookModel.LastName);
                    cmd.Parameters.AddWithValue("@Address", addressBookModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressBookModel.City);
                    cmd.Parameters.AddWithValue("@State", addressBookModel.State);
                    cmd.Parameters.AddWithValue("@Zip", addressBookModel.Zip);
                    cmd.Parameters.AddWithValue("@PhoneNumber", addressBookModel.PhoneNumber);
                    cmd.Parameters.AddWithValue("@Email", addressBookModel.Email);
                    cmd.Parameters.AddWithValue("@AddressBookName", addressBookModel.AddressBookName);
                    cmd.Parameters.AddWithValue("@AddressBookType", addressBookModel.AddressBookType);
                    this.connection.Open();
                    var result1 = cmd.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
    }
}