package com.example.labjava.controller;

import com.example.labjava.model.DBManager;
import com.example.labjava.model.User;
import jakarta.servlet.ServletException;
import jakarta.servlet.annotation.WebServlet;
import jakarta.servlet.http.HttpServlet;
import jakarta.servlet.http.HttpServletRequest;
import jakarta.servlet.http.HttpServletResponse;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.ArrayList;

@WebServlet(name = "userController", value = "/userController")
public class UserController  extends HttpServlet {
    protected void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
        String action = request.getParameter("action");

        if ((action != null) && action.equals("update")) {
//            // We update an asset
//            Asset asset = new Asset(Integer.parseInt(request.getParameter("id")),
//                    Integer.parseInt(request.getParameter("userid")),
//                    request.getParameter("description"),
//                    Integer.parseInt(request.getParameter("value")));
//            DBManager dbmanager = new DBManager();
//            Boolean result = dbmanager.updateAsset(asset);
//            PrintWriter out = new PrintWriter(response.getOutputStream());
//            if (result == true) {
//                out.println("Update asset succesfully.");
//            } else {
//                out.println("Error updating asset!");
//            }
//            out.flush();
        } else if ((action != null) && action.equals("getAll")) {

        }
    }

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

    }
}
