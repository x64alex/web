using System;
using LastLab.Models;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Zlib;

namespace LastLab.DataAbstractionLayer
{
    public class DAL
    {

        public void saveBook(Book book)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Book (title, author, category, pages, genre) VALUES ('" + book.title + "', '" + book.author + "','" + book.category + "'," + book.pages + ",'" + book.genre + "')";

                cmd.ExecuteNonQuery();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }

        }

        public List<Book> getBookstByCategory(string category)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";
            List<Book> books = new List<Book>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                if (category == "")
                {
                    cmd.CommandText = "SELECT * FROM Book";
                }
                else
                {
                    cmd.CommandText = "SELECT * FROM Book WHERE category ='" + category + "'";
                }
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    Book book = new Book();
                    book.id = myreader.GetInt32("id");
                    book.title = myreader.GetString("title");
                    book.author = myreader.GetString("author");
                    book.category = myreader.GetString("category");
                    book.pages = myreader.GetInt32("pages");
                    book.genre = myreader.GetString("genre");
                    books.Add(book);
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return books;

        }

        public void updateBook(Book book)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update Book SET title = "+book.title+", author = "+book.author+", category = "+book.category+", pages = "+book.pages+", genre = "+book.genre+" where id = "+book.id;

                cmd.ExecuteNonQuery();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }

        }

        public String deleteBook(string bookTitle)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Book where title = '" + bookTitle+"'";

                cmd.ExecuteNonQuery();

                return "Record deleted successfully";

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
                return "Error deleting record: " + ex.Message;
            }
        }
    }
}

