using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LastLab.Models;
using LastLab.DataAbstractionLayer;
using MySqlX.XDevAPI;
using MySqlX.XDevAPI.Common;


namespace LastLab.Controllers;

public class HomeController : Controller
{
    // GET: Main
    public ActionResult Index()
    {
        return View("AllCities");
    }

    public string GetCities()
    {
        DAL dal = new DAL();
        List<City> cities = dal.getCities();
        string result;
        if (cities.Count == 0)
        {
            result = "<h3>No cities</h3>";
        }
        else
        {
            result = "<table><thead><th>Name</th><th>County</th></thead>";
            foreach (City city in cities)
            {
                result += "<tr><td>" + city.name+ "</td><td>" + city.county + "</td><td></tr>";
            }

            result += "</table>";

        }



        return result;
    }

    [HttpGet("Home/AddDeparture")]
    public ActionResult AddDeparture()
    {
        string departureCityName = Request.Query["departureCity"];
        bool response = false;

        DAL dal = new DAL();
        List<City> cities = dal.getCities();
        City departureCity = new City();


        foreach (City city in cities)
        {
            if(city.name == departureCityName)
            {
                response = true;
                departureCity = city;
                break;
            }
        }

        if (response == true)
        {
            MyGlobals.activeRoute.Push(departureCity);
            return RedirectToAction("RouteView");
        }
        return RedirectToAction("ErrorAddRouteCity");
    }

    [HttpGet("Home/ErrorAddDeparture")]
    public ActionResult ErrorAddDeparture()
    {
        return View("ErrorAddDeparture");
    }
    [HttpGet("Home/RouteView")]
    public ActionResult RouteView()
    {
        return View("RouteView");
    }

    [HttpGet("Home/ErrorAddRouteCity")]
    public ActionResult ErrorAddRouteCity()
    {
        return View("ErrorAddRouteCity");
    }


    
    [HttpGet("Home/RouteGoBack")]
    public ActionResult RouteGoBack()
    {
        MyGlobals.activeRoute.Pop();
        if(MyGlobals.activeRoute.Count == 0)
        {
            return View("AllCities");
        }
        else
        {
            ViewData["departureCity"] = MyGlobals.activeRoute.Peek().name;
            return View("RouteView");
        }

    }

    [HttpGet("Home/RouteGoBackError")]
    public ActionResult RouteGoBackError()
    {
        if (MyGlobals.activeRoute.Count == 0)
        {
            return View("AllCities");
        }
        else
        {
            ViewData["departureCity"] = MyGlobals.activeRoute.Peek().name;
            return View("RouteView");
        }

    }



    public string GetCityLinks()
    {
        DAL dal = new DAL();
        List<City> cities = dal.getCityLinks();
        string result;
        if (cities.Count == 0)
        {
            result = "<h3>No cities</h3>";
        }
        else
        {
            result = "<table><thead><th>Name</th><th>County</th></thead>";
            foreach (City city in cities)
            {
                result += "<tr><td>" + city.name + "</td><td>" + city.county + "</td><td></tr>";
            }

            result += "</table>";

        }



        return result;
    }




























    [HttpGet("Home/Login")]
    public ActionResult Login()
    {
        String username = Request.Query["username"];
        String mother = Request.Query["mother"];
        String father = Request.Query["father"];

        DAL dal = new DAL();
        bool loggedin = dal.login(username, mother, father);
        if (loggedin == true)
        {
            MyGlobals.username = username;
            MyGlobals.mother = mother;
            MyGlobals.father = father;
            return RedirectToAction("HomeLogin");
        }
        return RedirectToAction("ErrorLogin");
    }



    [HttpGet("Home/ErrorLogin")]
    public ActionResult ErrorLogin()
    {
        return View("ErrorLogin");
    }
    public ActionResult HomeLogin()
    {
        return View("HomeLogin");
    }
    [HttpGet("Home/AddSection")]
    public ActionResult AddSection()
    {
        return View("AddSection");
    }

    public ActionResult ViewSection()
    {
        return View("ViewSection");
    }
    [HttpGet("Home/ErrorAdd")]
    public ActionResult ErrorAdd()
    {
        return View("ErrorAdd");
    }
    [HttpGet("Home/SuccesAdd")]
    public ActionResult SuccesAdd()
    {
        return View("SuccesAdd");
    }



    public string GetSiblings()
    {
        DAL dal = new DAL();
        List<User> users = dal.getSiblings(MyGlobals.mother, MyGlobals.father);
        string result;
        if (users.Count == 0)
        {
            result = "<h3>No sibling for this user</h3>";
        }
        else
        {
            result = "<table><thead><th>Id</th><th>Name</th></thead>";
            foreach (User user in users)
            {
                result += "<tr><td>" + user.id + "</td><td>" + user.name + "</td><td></tr>";
            }

            result += "</table>";

        }



        return result;
    }

    public string GetFather()
    {
        DAL dal = new DAL();
        List<User> users = dal.getDescendigLine("father");
        string result;
        if (users.Count == 0)
        {
            result = "<h3>No descending for this user</h3>";
        }
        else
        {
            result = "";
            foreach (User user in users)
            {
                result += user.name + "<br>";
            }

            result += "</table>";

        }

        return result;
    }
    public string GetMother()
    {
        DAL dal = new DAL();
        List<User> users = dal.getDescendigLine("mother");
        string result;
        if (users.Count == 0)
        {
            result = "<h3>No descending for this user</h3>";
        }
        else
        {
            result = "";
            foreach (User user in users)
            {
                result += user.name + "<br>";
            }

            result += "</table>";

        }

        return result;
    }



    [HttpGet("Home/AddParents")]
    public ActionResult AddParents()
    {
        string username = Request.Query["username"];
        string mother = Request.Query["mother"];
        string father = Request.Query["father"];

        DAL dal = new DAL();
        bool response = dal.addParents(username, mother, father);
        if (response == true)
        {
            return RedirectToAction("SuccesAdd");
        }
        return RedirectToAction("ErrorAdd");
    }













    [HttpGet("Home/SaveBook")]
    public ActionResult SaveBook()
    {
        Book book = new Book();
        book.title = Request.Query["title"];
        book.category = Request.Query["category"];
        book.author = Request.Query["author"];
        book.genre = "2";

        DAL dal = new DAL();
        dal.saveBook(book);
        return RedirectToAction("Index");
    }


    public string GetBooksFromCategory()
    {
        string category = Request.Query["category"];
        DAL dal = new DAL();
        List<Book> books = dal.getBookstByCategory(category);
        ViewData["studentList"] = books;
        string result;
        if (books.Count == 0)
        {
            result = "<h3>No books for this category</h3>";
        }
        else
        {
            result = "<table><thead><th>Id</th><th>Title</th><th>Author</th><th>Category</th></thead>";
            foreach (Book book in books)
            {
                result += "<tr><td>" + book.id + "</td><td>" + book.title + "</td><td>" + book.author + "</td><td>" + book.category + "</td><td></tr>";
            }

            result += "</table>";

        }



        return result;
    }

    [HttpGet("Home/DeleteBookView")]
    public ActionResult DeleteBookView()
    {
        return View("DeleteBook");
    }

    [HttpGet("Home/DeleteBook")]
    public ActionResult DeleteBook()
    {
        string title = Request.Query["title"];


        DAL dal = new DAL();
        dal.deleteBook(title);
        return RedirectToAction("Index");
    }
}


