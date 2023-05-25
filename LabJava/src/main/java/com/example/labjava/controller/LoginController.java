package com.example.labjava.controller;

import java.io.*;

import com.example.labjava.model.Authenticator;
import com.example.labjava.model.User;
import jakarta.servlet.RequestDispatcher;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.*;
import jakarta.servlet.annotation.*;

@WebServlet(name = "loginServlet", value = "/login-servlet")
public class LoginController extends HttpServlet {
    private String message;

    public void init() {
        message = "Hello Login!";
    }

    public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html");

        // Hello
        PrintWriter out = response.getWriter();
        out.println("<html><body>");
        out.println("<h1>" + message + "</h1>");
        out.println("</body></html>");
    }
    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response) throws ServletException, IOException {

        String username = request.getParameter("username");
        String password = request.getParameter("password");
        RequestDispatcher rd = null;

        Authenticator authenticator = new Authenticator();
        String result = authenticator.authenticate(username, password);
        if (result.equals("success")) {
            rd = request.getRequestDispatcher("/succes.jsp");
            User user = new User(username, password);
            request.setAttribute("user", user);
            // Normally, here we should set the "user" attribute on the session like this:
            // HttpSession session = request.getSession();
            // session.setAttribute("user", user);
            // .. and then, in all JSP/Servlet pages we should check if the "user" attribute exists in the session
            // and if not, we should return/exit the method:
            // HttpSession session = request.getSession();
            // String user = session.getAttribute("user");
            // if (user==null || user.equals("")) {
            //        return;
            // }
        } else {
            rd = request.getRequestDispatcher("/error.jsp");
        }
        rd.forward(request, response);
    }

    public void destroy() {
    }



}

//    CREATE TABLE user_profiles (
//        id INT AUTO_INCREMENT PRIMARY KEY,
//        name VARCHAR(50) NOT NULL,
//    email VARCHAR(100) NOT NULL,
//    picture VARCHAR(100),
//    age INT,
//    home_town VARCHAR(50)
//);