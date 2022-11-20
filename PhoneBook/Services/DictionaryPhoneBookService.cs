using Microsoft.Data.Sqlite;
using PhoneBook.Exceptions;
using PhoneBook.Model;
using System.Xml.Linq;
using System.Data.SQLite;

namespace PhoneBook.Services
{
    public class DictionaryPhoneBookService : IPhoneBookService
    {
        private Dictionary<string, string> _phoneBookEntries;

        public DictionaryPhoneBookService()
        {
            _phoneBookEntries = new Dictionary<string, string>();
        }

        public void Add(PhoneBookEntry phoneBookEntry)
        {
            if (phoneBookEntry.Name == null || phoneBookEntry.PhoneNumber == null)
            {
                throw new ArgumentException("Name and phone number must both be specified.");
            }

            _phoneBookEntries.Add(phoneBookEntry.Name, phoneBookEntry.PhoneNumber);
            AddData(phoneBookEntry.Name, phoneBookEntry.PhoneNumber);
        }

        public void Add(string name, string phoneNumber)
        {
            if (name == null || phoneNumber == null)
            {
                throw new ArgumentException("Name and phone number must both be specified.");
            }

            _phoneBookEntries.Add(name, phoneNumber);
            AddData(name, phoneNumber);



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
            cmd.CommandText = "insert into Phonebook(Name,PhoneNumber) values ('" + name + "','" + phoneNumber + "')";
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