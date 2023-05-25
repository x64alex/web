<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <title>Insert title here</title>
</head>
<body>
<%
    com.example.labjava.model.User user = (com.example.labjava.model.User) request.getAttribute("user");
    out.println("Welcome "+user.getName());
%>
</body>
</html>