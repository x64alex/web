using System;
using LastLab.Models;
using Microsoft.Extensions.FileSystemGlobbing;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities.Zlib;

namespace LastLab.DataAbstractionLayer
{
    public class DAL
    {
        public List<City> getCities()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";
            List<City> cities = new List<City>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM City";

                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    City city = new City();
                    city.id = myreader.GetInt32("id");
                    city.name = myreader.GetString("name");
                    city.county = myreader.GetString("county");

                    cities.Add(city);
                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return cities;

        }

        public List<DestinationCity> getCityLinks()
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";
            List<DestinationCity> destinationCities = new List<DestinationCity>();


            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                City lastCity = MyGlobals.activeRoute.Peek();

                cmd.CommandText = "SELECT * FROM Link WHERE idcity1="+lastCity.id;
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    DestinationCity city = new DestinationCity();
                    city.id = myreader.GetInt32("idcity2");
                    city.distance = myreader.GetInt32("distance");
                    city.duration = myreader.GetInt32("duration");
                    destinationCities.Add(city);
                }
                myreader.Close();


                foreach(DestinationCity city in destinationCities)
                {
                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.Connection = conn;

                    cmd1.CommandText = "SELECT * FROM City WHERE id=" + city.id;
                    MySqlDataReader myreader1 = cmd1.ExecuteReader();
                    myreader1.Read();
                    city.name = myreader1.GetString("name");
                    city.county = myreader1.GetString("county");

                    myreader1.Close();
                }




            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return destinationCities;

        }





































        public bool login(string username, string mother, string father)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";
            bool response = false;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = "SELECT id FROM Users1 WHERE username= '"+username+ "'";
                MySqlDataReader myreader1 = cmd1.ExecuteReader();
                myreader1.Read();
                int userid = myreader1.GetInt32("id");
                myreader1.Close();

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "SELECT COUNT(*) AS C FROM FamilyRelationship WHERE userid= '"+userid+
                    "' AND mother = '"+mother+"' AND father = '"+ father+"'";
                MySqlDataReader myreader2 = cmd2.ExecuteReader();
                myreader2.Read();
                int count = myreader2.GetInt32("C");


                response = count != 0;
                myreader2.Close();
                MyGlobals.userid = userid;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            return response;

        }

        public List<User> getSiblings(string mother, string father)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";
            List<User> users = new List<User>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM FamilyRelationship WHERE mother= '" + mother + "' " +
                    "AND father = '" + father + "'";

                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    User user = new User();
                    int userid = myreader.GetInt32("userid");
                    user.id = userid;
                    if(userid != MyGlobals.userid)
                    {
                        users.Add(user);
                    }
                }
                myreader.Close();
                foreach(User u in users)
                {
                    MySqlCommand cmd2 = new MySqlCommand();
                    cmd2.Connection = conn;
                    cmd2.CommandText = "SELECT username FROM Users1 WHERE id= '" + u.id + "'";
                    MySqlDataReader myreader2 = cmd2.ExecuteReader();
                    myreader2.Read();
                    string username = myreader2.GetString("username");
                    u.name = username;
                    myreader2.Close();
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return users;

        }



        public List<User> getDescendigLine(string motherOrFather)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";
            List<User> users = new List<User>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                int descendingLineId = -1;
                string descendingName;
                if(motherOrFather == "mother")
                {
                    descendingName= MyGlobals.mother;
                }
                else
                {
                    descendingName = MyGlobals.father;

                }

                User existentUser = new User();
                existentUser.id = MyGlobals.userid;
                existentUser.name = MyGlobals.username;
                users.Add(existentUser);

                while (true)
                {
                    MySqlCommand cmd1 = new MySqlCommand();
                    cmd1.Connection = conn;
                    cmd1.CommandText = "SELECT COUNT(*) AS C FROM Users1 WHERE username= '" +  descendingName+ "'";
                    MySqlDataReader myreader1 = cmd1.ExecuteReader();
                    myreader1.Read();
                    int c = myreader1.GetInt32("C");
                    myreader1.Close();
                    if (c == 0)
                    {
                        User lastUser = new User();
                        lastUser.name = descendingName;
                        users.Add(lastUser);
                        break;
                    }
                    else
                    {
                        MySqlCommand cmd2 = new MySqlCommand();
                        cmd2.Connection = conn;
                        cmd2.CommandText = "SELECT * FROM Users1 WHERE username= '" + descendingName + "'";
                        MySqlDataReader myreader2 = cmd2.ExecuteReader();
                        myreader2.Read();
                        int id = myreader2.GetInt32("id");
                        myreader2.Close();

                        User currentUser = new User();
                        currentUser.id = id;
                        currentUser.name = descendingName;
                        users.Add(currentUser);



                        MySqlCommand cmd = new MySqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT * FROM FamilyRelationship WHERE userid= '" + id + "'";

                        MySqlDataReader myreader = cmd.ExecuteReader();

                        while (myreader.Read())
                        {
                            User user = new User();
                            string motherName = myreader.GetString("mother");
                            string fatherName = myreader.GetString("father");

                            if (motherOrFather == "mother")
                            {
                                descendingName = motherName;
                            }
                            else
                            {
                                descendingName = fatherName;

                            }
                        }
                        myreader.Close();
                    }
                }


            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return users;

        }


        public bool addParents(string username, string mother, string father)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd='12345678';database=web;";
            bool response = false;

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd1 = new MySqlCommand();
                cmd1.Connection = conn;
                cmd1.CommandText = "SELECT id FROM Users1 WHERE username= '" + username + "'";
                MySqlDataReader myreader1 = cmd1.ExecuteReader();
                myreader1.Read();
                int userid = myreader1.GetInt32("id");
                myreader1.Close();

                MySqlCommand cmd2 = new MySqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "insert into FamilyRelationship(userid, mother, father) values(" + userid +
                    ",'" + mother + "','" + father + "')";
                MySqlDataReader myreader2 = cmd2.ExecuteReader();
                myreader2.Read();


                response = true;
                myreader2.Close();
                MyGlobals.userid = userid;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }
            return response;

        }















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

