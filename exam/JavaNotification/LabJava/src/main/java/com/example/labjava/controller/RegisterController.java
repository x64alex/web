package com.example.labjava.controller;

import com.example.labjava.model.DBManager;
import jakarta.servlet.RequestDispatcher;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;

@WebServlet(name = "registerServlet", value = "/register-servlet")
public class RegisterController extends HttpServlet {
    public void init() {}

    public void doGet(HttpServletRequest request, HttpServletResponse response) throws IOException {
        response.setContentType("text/html");
        PrintWriter out = response.getWriter();
        out.println("<html><body>");
        out.println("<h1>" + "Need to login first!" + "</h1>");
        out.println("</body></html>");
    }
    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response) throws ServletException, IOException {

        String name = request.getParameter("name");
        String password = request.getParameter("password");
        String email = request.getParameter("email");
        String picture = request.getParameter("picture");
        int age = Integer.parseInt(request.getParameter("age"));
        String homeTown = request.getParameter("homeTown");

        RequestDispatcher rd = null;

        DBManager DBManager = new DBManager();
        Boolean result = DBManager.register(name, password, email, picture, age, homeTown);
        if (result) {
            rd = request.getRequestDispatcher("/registerSucces.jsp");
        } else {
            rd = request.getRequestDispatcher("/error.jsp");
        }
        rd.forward(request, response);
    }

    public void destroy() {
    }



}