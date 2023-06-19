package com.example.labjava.controller;

import java.io.*;
import java.util.Optional;

import com.example.labjava.model.DBManager;
import com.example.labjava.model.User;
import jakarta.servlet.RequestDispatcher;
import jakarta.servlet.ServletException;
import jakarta.servlet.http.*;
import jakarta.servlet.annotation.*;

@WebServlet(name = "loginServlet", value = "/login-servlet")
public class LoginController extends HttpServlet {
    public void init() {}

    public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html");
        PrintWriter out = response.getWriter();
        out.println("<html><body>");
        out.println("<a href=\"index.jsp\">Go to home page</a>\n");
        out.println("<h1>" + "Need to login first!" + "</h1>");
        out.println("</body></html>");
    }
    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response) throws ServletException, IOException {

        String username = request.getParameter("username");
        String password = request.getParameter("password");
        RequestDispatcher rd = null;

        DBManager DBManager = new DBManager();
        Optional<User> result = DBManager.authenticate(username, password);
        if (result.isPresent()) {
            rd = request.getRequestDispatcher("/succes.jsp");
            User user = result.get();
             HttpSession session = request.getSession();
             session.setAttribute("user", user);
        } else {
            rd = request.getRequestDispatcher("/error.jsp");
        }
        rd.forward(request, response);
    }

    public void destroy() {
    }
}