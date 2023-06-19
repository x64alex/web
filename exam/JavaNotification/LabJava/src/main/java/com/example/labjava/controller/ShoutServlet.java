package com.example.labjava.controller;

import jakarta.servlet.ServletContext;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import java.io.IOException;
import java.io.PrintWriter;

@WebServlet(urlPatterns = {"/shoutServlet"})
public class ShoutServlet extends HttpServlet {

    @Override
    protected void doGet(HttpServletRequest request, HttpServletResponse response)
            throws ServletException, IOException {
        String message = "No current updates";


        ServletContext sc = request.getServletContext();
        if (sc.getAttribute("messages") != null) {
            message = (String) sc.getAttribute("messages");
        }
        PrintWriter out = new PrintWriter(response.getOutputStream());
        out.println(message);
        out.flush();
    }
}
