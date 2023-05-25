package com.example.labjava.model;

import java.sql.*;

public class Authenticator {
    private Statement stmt;

    public Authenticator() {
        connect();
    }

    public void connect() {
        try {
            Class.forName("com.mysql.jdbc.Driver");
            Connection con = DriverManager.getConnection("jdbc:mysql://localhost/web", "root", "12345678");
            stmt = con.createStatement();
        } catch(Exception ex) {
            System.out.println("error at connect:"+ex.getMessage());
            ex.printStackTrace();
        }
    }

    public String authenticate(String username, String password) {
        ResultSet rs;
        String result = "error";
        System.out.println(username+" "+password);
        try {
            rs = stmt.executeQuery("select * from users where name='"+username+"' and password='"+password+"'");
            if (rs.next()) {
                result = "success";
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return result;
    }
}