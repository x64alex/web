using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LastLab.Models;
using LastLab.DataAbstractionLayer;

namespace LastLab.Controllers;

public class HomeController : Controller
{
    // GET: Main
    public ActionResult Index()
    {
        return View("FilterBooks");
    }

    [HttpGet("Home/AddBook")]
    public ActionResult AddBook()
    {
        return View("AddNewBook");
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


