<%--
  Created by IntelliJ IDEA.
  User: acantor
  Date: 19.06.2023
  Time: 17:12
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<head>
    <title>Title</title>
</head>
<body>
<a href="index.jsp">Go to home page</a>
<%
    String name=(String)session.getAttribute("name");
    out.println("Hello "+name);
%>
<form action="articlesController" method="post">
    Enter journal name: <input type="text" name="jName"> <BR>
    Enter article summary: <input type="text" name="aSummary"> <BR>
    <input type="submit" value="Add"/>
</form>
</body>
</html>
