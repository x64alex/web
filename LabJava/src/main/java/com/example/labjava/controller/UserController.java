package com.example.labjava.controller;

import com.example.labjava.model.Authenticator;
import com.example.labjava.model.User;
import com.mysql.cj.xdevapi.JsonArray;
import com.mysql.cj.xdevapi.JsonValue;
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
            String name = request.getParameter("name");
            String email = request.getParameter("email");
            String age;
            try {
                age = request.getParameter("age");
            } catch (NumberFormatException e) {
                age = "";
            }
            String homeTown = request.getParameter("home");
            String picture = request.getParameter("picture");


            response.setContentType("application/json");
            Authenticator dbmanager = new Authenticator();
            ArrayList<User> users = dbmanager.getUsers(name, email, picture,  age, homeTown);

            JSONArray jsonAssets = new JSONArray();
            for (int i = 0; i < users.size(); i++) {
                JSONObject jObj = new JSONObject();
                jObj.put("id", users.get(i).getId());
                jObj.put("name", users.get(i).getName());
                jObj.put("email", users.get(i).getEmail());
                jObj.put("age", users.get(i).getAge());
                jObj.put("homeTown", users.get(i).getHomeTown());
                jObj.put("picture", users.get(i).getPicture());
                jsonAssets.put(jObj);
            }
            PrintWriter out = new PrintWriter(response.getOutputStream());
            out.println(jsonAssets);
            out.flush();
        }
    }

    protected void doPost(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {

    }
}
