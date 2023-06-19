<%@ page contentType="text/html; charset=UTF-8" pageEncoding="UTF-8" %>
<!DOCTYPE html>
<html>
<head>
  <title>JSP - Hello World</title>
</head>
<body>
<h1><%= "Hello! Please enter name below" %></h1>
<br/>
<form action="name-servlet" method="post">
  Enter name : <input type="text" name="name"> <BR>
  <input type="submit" value="Submit"/>
</form>
</body>
</html>