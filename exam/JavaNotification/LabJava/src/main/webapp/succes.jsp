<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<%@ page import="com.example.labjava.model.Article" %>
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
</head>

<body>
<a href="index.jsp">Go to home page</a>
<a href="addArticle.jsp">Add an article</a>
<%! String user; %>
<%  user = (String) session.getAttribute("name");
    if (user != "") {
        out.println("Welcome " + user);
    } else {
        out.println("Please login first");
    }
%>
<h3> Updates: </h3>
<div id="content">
No current updates
</div>
<table>
    <tr><td><h4>Get all your articles:</h4><td></tr>
    <tr><td>Journal name: </td><td><input type="text" id="journal-name"></td></tr>
    <tr><td><button id="getArticlesButton" type="button">Get articles</button></td><td></td></tr>
</table>
<section><table id="asset-table"></table></section>

<script>
    function getArticles(name,journalName, callbackFunction) {
        $.getJSON(
            "articlesController",
            { action: 'getAll', name: name, journalName: journalName},
            callbackFunction
        );
    }


    $(document).ready(function(){
        $("#getArticlesButton").click(function() {
            getArticles(${sessionScope.name},
                $("#journal-name").val(),
                function(articles) {
                    console.log(articles);
                    $("#asset-table").html("");
                    $("#asset-table").append("<tr style='background-color: mediumseagreen'><td>ArticleId</td><td>User</td><td>Summary</td><td>Date</td></tr>");
                    for(i=0;i<articles.length;i++) {
                        $("#asset-table").append("<tr><td>"+articles[i].id+"</td>" +
                            "<td>"+articles[i].user+"</td>"+
                            "<td>"+articles[i].summary+"</td>" +
                            "<td>"+articles[i].date+"</td>"
                        );
                    }

            })
        })


    });


    function reload(){
        var container = document.getElementById("content");
        var xhr = new XMLHttpRequest();
        xhr.open("GET", "shoutServlet", true);
        xhr.onreadystatechange = function() {
            if (xhr.readyState === 4 && xhr.status === 200) {
                var responseText = xhr.responseText;
                container.innerHTML = responseText
            }
        };
        xhr.send();
    }

    setInterval(reload, 1000);
</script>

</body>
</html>