using Microsoft.Data.Sqlite;
using PhoneBook.Exceptions;
using PhoneBook.Model;
using System.Xml.Linq;
using System.Data.SQLite;
using NLog;
using System.ComponentModel;

namespace PhoneBook.Services
{
    public class DictionaryPhoneBookService : IPhoneBookService
    {
        private Dictionary<string, string> _phoneBookEntries;
        private readonly Logger logger = LogManager.GetCurrentClassLogger(); // creates a logger using the class name


        public DictionaryPhoneBookService()
        {
            _phoneBookEntries = new Dictionary<string, string>();
        }

        public void Add(PhoneBookEntry phoneBookEntry)
        {
            try
            {
                if (phoneBookEntry.Name == null || phoneBookEntry.PhoneNumber == null)
                {
                    throw new ArgumentException("Name and phone number must both be specified.");
                }
                _phoneBookEntries.Add(phoneBookEntry.Name, phoneBookEntry.PhoneNumber);
                AddData(phoneBookEntry.Name, phoneBookEntry.PhoneNumber);
                logger.Info("Added Name: " + phoneBookEntry.Name + " having Phone Number: " + phoneBookEntry.PhoneNumber);
            }
            catch (ArgumentException ex)
            {
                logger.Error("Name and phone number must both be specified.", ex);
            }
            catch (Exception ex)
            {
                logger.Error("Generic Exception Logged.", ex);
            }
        }

        public void Add(string name, string phoneNumber)
        {
            try
            {
                if (name == null || phoneNumber == null)
                {
                    throw new ArgumentException("Name and phone number must both be specified.");
                }

                _phoneBookEntries.Add(name, phoneNumber);
                AddData(name, phoneNumber);
                logger.Info("Added Name: " + name + " having Phone Number: " + phoneNumber);
            }
            catch (ArgumentException ex)
            {
                logger.Error("Name and phone number must both be specified.", ex);
            }
            catch (Exception ex)
            {
                logger.Error("Generic Exception Logged.", ex);
            }

        }

        public static void AddData(string name, string phoneNumber)
        {
            SQLiteConnection con;
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            con = new SQLiteConnection("Data Source=Phonebook.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "insert into Phonebook(Name,PhoneNumber) VALUES(@nameParam, @phParam)";
            cmd.Parameters.AddWithValue("@nameParam", name);
            cmd.Parameters.AddWithValue("@phParam", phoneNumber);
            cmd.ExecuteNonQuery();
            con.Close();


        }

        public IEnumerable<PhoneBookEntry> List()
        {
            List<PhoneBookEntry> entriesList = new List<PhoneBookEntry>();

            //foreach (var name in _phoneBookEntries.Keys)
            //{
            //    entriesList.Add(new PhoneBookEntry { Name = name, PhoneNumber = _phoneBookEntries[name] });
            //}

            entriesList = RetrieveDataFromDB();
            logger.Info("Retrieved All List of PhoneBook Entries.");

            return entriesList;
        }

        public List<PhoneBookEntry> RetrieveDataFromDB()
        {
            int counter = 0;
            SQLiteConnection con;
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            con = new SQLiteConnection("Data Source=Phonebook.sqlite;Version=3;");
            cmd = new SQLiteCommand("Select * From Phonebook", con);
            con.Open();
            dr = cmd.ExecuteReader();
            List<PhoneBookEntry> entriesList = new List<PhoneBookEntry>();
            while (dr.Read())
            {
                counter++;
                entriesList.Add(new PhoneBookEntry { Name = dr[1].ToString(), PhoneNumber = dr[2].ToString() });

            }
            con.Close();
            _phoneBookEntries = new Dictionary<string, string>();
            foreach (var item in entriesList)
            {
                _phoneBookEntries.Add(item.Name, item.PhoneNumber);
            }

            return entriesList;

        }

        public void DeleteByName(string name)
        {
            if (!_phoneBookEntries.ContainsKey(name))
            {
                throw new NotFoundException($"No phonebook entry found containing name {name}.");
            }

            _phoneBookEntries.Remove(name);
            DeleteByNameFromDB(name);
            logger.Info("Deleted By Name: " + name);

        }

        public void DeleteByNameFromDB(string name)
        {
            SQLiteConnection con;
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            con = new SQLiteConnection("Data Source=Phonebook.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "delete from Phonebook where Name = '" + name + "'";
            cmd.ExecuteNonQuery();
            con.Close();
        }


        public void DeleteByNumber(string PhoneNumber)
        {
            var name = _phoneBookEntries.Where(kvp => kvp.Value == PhoneNumber).FirstOrDefault().Key;
            if (name == null)
            {
                throw new NotFoundException($"No phonebook entry found containing phone number {PhoneNumber}.");
            }

            _phoneBookEntries.Remove(name);
            DeleteByNumberFromDB(PhoneNumber);
            logger.Info("Deleted Entry By Phone Number: " + PhoneNumber);
        }

        public void DeleteByNumberFromDB(string PhoneNumber)
        {
            SQLiteConnection con;
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            con = new SQLiteConnection("Data Source=Phonebook.sqlite;Version=3;");
            cmd = new SQLiteCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "delete from Phonebook where PhoneNumber = '" + PhoneNumber + "'";
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}