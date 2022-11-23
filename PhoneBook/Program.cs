using Microsoft.OpenApi.Models;
using PhoneBook.Services;
using System.Data.SQLite;

namespace PhoneBook
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "PhoneBook API",
                    Description = "A simple API for managing a phone book."
                });
            });

            builder.Services.AddSingleton<IPhoneBookService, DictionaryPhoneBookService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            CreateDatabaseAndTable();

            app.Run();
        }

        static void CreateDatabaseAndTable()
        {
            SQLiteConnection con;
            SQLiteCommand cmd;
            SQLiteDataReader dr;
            if (!File.Exists("Phonebook.sqlite"))
            {
                SQLiteConnection.CreateFile("Phonebook.sqlite");

                string sql = @"CREATE TABLE Phonebook(
                               ID INTEGER PRIMARY KEY AUTOINCREMENT ,
                               Name           TEXT      NOT NULL,
                               PhoneNumber            TEXT       NOT NULL
                            );";
                con = new SQLiteConnection("Data Source=Phonebook.sqlite;Version=3;");
                con.Open();
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();

                //The below code was only for testing at initial phase.
                //-----------------------------------------------------------------------------------
                //AddData("avinash","111-111-1111");
                //AddData("biraaj", "222-222-2222");
                //AddData("gyana", "333-333-3333");
                //AddData("chintamani", "444-444-4444");
                //SelectData();
                //-----------------------------------------------------------------------------------

            }
            else
            {
                con = new SQLiteConnection("Data Source=Phonebook.sqlite;Version=3;");

                //SelectData();
            }
        }



        //The below code was only for testing at initial phase.

        //-----------------------------------------------------------------------------------
        //public static void AddData(string name, string phoneNumber)
        //{
        //    SQLiteConnection con;
        //    SQLiteCommand cmd;
        //    SQLiteDataReader dr;
        //    con = new SQLiteConnection("Data Source=Phonebook.sqlite;Version=3;");
        //    cmd = new SQLiteCommand();
        //    con.Open();
        //    cmd.Connection = con;
        //    cmd.CommandText = "insert into Phonebook(Name,PhoneNumber) values ('" + name + "','" + phoneNumber + "')";
        //    cmd.ExecuteNonQuery();
        //    con.Close();
        //}

        //public static void SelectData()
        //{
        //    int counter = 0;
        //    SQLiteConnection con;
        //    SQLiteCommand cmd;
        //    SQLiteDataReader dr;
        //    con = new SQLiteConnection("Data Source=Phonebook.sqlite;Version=3;");
        //    cmd = new SQLiteCommand("Select * From Phonebook", con);
        //    con.Open();
        //    dr = cmd.ExecuteReader();
        //    while (dr.Read())
        //    {
        //        counter++;
        //        Console.Write("Data");
        //        Console.WriteLine(dr[0] + " : " + dr[1] + " " + dr[2]);

        //    }
        //    con.Close();

        //}
        //-----------------------------------------------------------------------------------
    }
}