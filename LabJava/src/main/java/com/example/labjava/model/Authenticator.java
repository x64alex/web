package com.example.labjava.model;

import java.sql.*;
import java.util.ArrayList;
import java.util.Optional;

public class Authenticator {
    private Statement stmt;

    public Authenticator() {
        connect();
    }

    public void connect() {
        try {
            Class.forName("com.mysql.cj.jdbc.Driver");
            Connection con = DriverManager.getConnection("jdbc:mysql://localhost/web", "root", "12345678");
            stmt = con.createStatement();
        } catch(Exception ex) {
            System.out.println("error at connect:"+ex.getMessage());
            ex.printStackTrace();
        }
    }

    public Optional<User> authenticate(String username, String password) {
        ResultSet rs;
        Optional<User> response = Optional.empty();
        System.out.println(username+" "+password);
        try {
            rs = stmt.executeQuery("select * from users where name='"+username+"' and password='"+password+"'");
            if (rs.next()) {
                int id = rs.getInt("id");
                String name = rs.getString("name");
                String email = rs.getString("email");
                String picture = rs.getString("picture");
                int age = rs.getInt("age");
                String homeTown = rs.getString("home_town");

                User user = new User(id, name, email, picture, age, homeTown, password);

                response = Optional.of(user);;
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return response;
    }
    public Boolean register(String name, String password, String email, String picture, int age, String homeTown) {
        ResultSet rs;
        boolean response = false;
        try {
            int rowsAffected  = stmt.executeUpdate("INSERT INTO users (name, email, picture, age, home_town, password) VALUES ('"+name+"', '"+email+"', '"+picture+"', '"+age+"', '"+homeTown+"', '"+password+"')");
            if(rowsAffected > 0) {
                System.out.println("Insertion successful");
                response = true;
            } else {
                System.out.println("Insertion failed");
            }
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return response;
    }

    public ArrayList<User> getUsers(String name, String email, String picture, String age, String homeTown){
        ArrayList<User> users = new ArrayList<User>();
        ResultSet rs;
        try {

            rs = stmt.executeQuery("select * from users where " +
                    "(SELECT POSITION('"+name+"' IN name) )>0 AND" +
                    " (SELECT POSITION('"+email+"' IN email) )>0 AND " +
                    "(SELECT POSITION('"+picture+"' IN picture) )>0 AND " +
                    "(SELECT POSITION('"+age+"' IN age) )>0 AND "+
                    "(SELECT POSITION('"+homeTown+"' IN home_town) )>0");
            while (rs.next()) {
                int id = rs.getInt("id");
                String nameQuery = rs.getString("name");
                String emailQuery = rs.getString("email");
                String pictureQuery = rs.getString("picture");
                int ageQuery = rs.getInt("age");
                String homeTownQuery = rs.getString("home_town");
                String password = rs.getString("password");

                User user = new User(id, nameQuery, emailQuery, pictureQuery, ageQuery, homeTownQuery, password);
                users.add(user);
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return users;
    }
}