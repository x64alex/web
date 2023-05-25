<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@ page import="com.example.labjava.model.User" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>Insert title here</title>
    <style>
        .asset-name {
            background-color: cornflowerblue;
            border-right: solid 1px black;
        }
    </style>
    <script src="js/jquery-2.0.3.js"></script>
    <script src="js/ajax-utils.js"></script>
</head>

<body>
<%! User user; %>
<%  user = (User) session.getAttribute("user");
    if (user != null) {
        out.println("Welcome " + user.getName());
        out.println(" Email:  " + user.getEmail());
        out.println("HomeTown:  " + user.getHomeTown());
        out.println("Age: " + user.getAge());
    } else {
        out.println("Please login first");
    }
%>
<table>
    <tr><td>User name: </td><td><input type="text" id="user-name"></td></tr>
    <tr><td>User Email: </td><td><input type="text" id="user-email"></td></tr>
    <tr><td>User age: </td><td><input type="text" id="user-age"></td></tr>
    <tr><td>User hometown: </td><td><input type="text" id="user-home"></td></tr>
    <tr><td>User picture: </td><td><input type="text" id="user-picture"></td></tr>
    <tr><td><button id="getUsersBtn" type="button">Get users</button></td><td></td></tr>
</table>
<section><table id="asset-table"></table></section>

<script>
    $(document).ready(function(){
        $("#getUsersBtn").click(function() {
            getUsers($("#user-name").val(),
                $("#user-email").val(),
                $("#user-age").val(),
                $("#user-home").val(),
                $("#user-picture").val(),
                function(users) {
                    console.log(users);
                    $("#asset-table").html("");
                    $("#asset-table").append("<tr style='background-color: mediumseagreen'><td>Userid</td><td>Name</td><td>Email</td><td>Age</td><td>Homwtown</td><td>Picture</td></tr>");
                    for(i=0;i<users.length;i++) {
                        $("#asset-table").append("<tr><td>"+users[i].id+"</td>" +
                            "<td>"+users[i].name+"</td>"+
                            "<td>"+users[i].email+"</td>" +
                            "<td>"+users[i].age+"</td>"+
                            "<td>"+users[i].homeTown+"</td>" +
                            "<td>"+users[i].picture+"</td></tr>"
                        );
                    }

            })
        })

    });
</script>

</body>
</html>