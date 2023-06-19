package com.example.labjava.controller;

import com.example.labjava.model.Article;
import com.example.labjava.model.DBManager;
import com.example.labjava.model.User;
import jakarta.servlet.RequestDispatcher;
import jakarta.servlet.ServletContext;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;
import jakarta.servlet.http.HttpSession;
import org.json.JSONArray;
import org.json.JSONObject;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.Optional;

@WebServlet(name = "articlesController", value = "/articlesController")
public class ArticlesController extends HttpServlet {

    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
            String name = request.getParameter("name");
            String journalName = request.getParameter("journalName");


            response.setContentType("application/json");
            DBManager dbmanager = new DBManager();
            ArrayList<Article> articles = dbmanager.getArticles(name, journalName);

            JSONArray jsonAssets = new JSONArray();
            for (int i = 0; i < articles.size(); i++) {
                JSONObject jObj = new JSONObject();
                jObj.put("id", articles.get(i).getId());
                jObj.put("user", articles.get(i).getUser());
                jObj.put("summary", articles.get(i).getSummary());
                jObj.put("date", articles.get(i).getDate());
                jsonAssets.put(jObj);
            }
            PrintWriter out = new PrintWriter(response.getOutputStream());
            out.println(jsonAssets);
            out.flush();
    }

    protected void doPost(HttpServletRequest request,
                          HttpServletResponse response) throws ServletException, IOException {

        String jName = request.getParameter("jName");
        String aSummary = request.getParameter("aSummary");

        HttpSession session = request.getSession();
        String name = (String) session.getAttribute("name");

        RequestDispatcher rd = null;

        DBManager DBManager = new DBManager();
        String result = DBManager.addArticles(name,aSummary,jName);
        if (result.equals("false")) {
            rd = request.getRequestDispatcher("/error.jsp");


        } else {
            ServletContext sc = request.getServletContext();
            String htmlMessage = "<p><b>" + result+ "</p>";
            if (sc.getAttribute("messages") == null) {
                sc.setAttribute("messages", htmlMessage);
            } else {
                String currentMessages = (String) sc.getAttribute("messages");
                sc.setAttribute("messages", htmlMessage + currentMessages);
            }

            rd = request.getRequestDispatcher("/succes.jsp");
        }
        rd.forward(request, response);
    }
}
