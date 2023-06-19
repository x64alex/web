package com.example.labjava.model;

import java.sql.*;
import java.util.ArrayList;
import java.util.Date;
import java.util.Optional;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;

public class DBManager {
    private Statement stmt;

    public DBManager() {
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

    public ArrayList<Article> getArticles(String name, String journalName){
        ArrayList<Article> articles = new ArrayList<Article>();
        ResultSet rs;
        try {

            rs = stmt.executeQuery("select * from journals j ,articles a where" +
                    " j.name = '"+journalName+"' AND " +
                    " a.user = '"+name+"' AND " +
                    "j.id = a.journalid");
            while (rs.next()) {
                int id = rs.getInt("id");
                String user = rs.getString("user");
                int journalid = rs.getInt("journalid");
                String summary = rs.getString("summary");
                java.util.Date date = rs.getDate("date");

                Article article = new Article(id,user,journalid,summary,date);
                articles.add(article);
            }
            rs.close();
        } catch (SQLException e) {
            e.printStackTrace();
        }
        return articles;
    }

    public String addArticles(String userName, String summary, String journalName){
        Integer jId = -1;
        ResultSet rs;
        try {

            rs = stmt.executeQuery("select * from journals j  where" +
                    " j.name = '"+journalName+"'");
            while (rs.next()) {
                int id = rs.getInt("id");
                jId = id;
            }
            DateTimeFormatter dtf = DateTimeFormatter.ofPattern("yyyyMMdd");
            LocalDateTime now = LocalDateTime.now();
            System.out.println(dtf.format(now));
            String date = dtf.format(now);
            if(jId != -1){
                stmt.execute("insert into articles(user,journalid,summary,date) values("+userName +","+jId+","+ summary+","+date +")\n");
            }
            else{
                stmt.execute("insert into journals(name) values("+journalName +")\n");
                rs = stmt.executeQuery("select * from journals j  where" +
                        " j.name = '"+journalName+"'");
                while (rs.next()) {
                    int id = rs.getInt("id");
                    jId = id;
                }

                stmt.execute("insert into articles(user,journalid,summary,date) values("+userName +","+jId+","+ summary+","+date +")\n");
            }
            rs.close();
            return "Article: "+userName+" "+summary+" "+date+" added.";
        } catch (SQLException e) {
            e.printStackTrace();
            return "false";
        }
    }
}