<!DOCTYPE html>
<html>

<head>
    <title>Book Management UU</title>
    <link rel="stylesheet" href="style.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
</head>

<body>
    <nav>
        <a href="browseBook.php">Browse Book</a>
        <a href="addBook.php">Add Book</a>
        <a href="#">Update Book</a>
        <a href="deleteBook.php">Delete Book</a>
    </nav>

    <form class="form" id="update-car" method="post" action="<?php echo $_SERVER['PHP_SELF']; ?>">

        <label for="Bookname">Bookname: </label>
        <input name="Bookname" type="text">

        <br>
        <br>

        <label for="password">Password: </label>
        <input password="password" type="text">

        <br>
        <br>

        <label for="full-name">Full name: </label>
        <input name="full-name" type="text">

        <br>
        <br>

        <label for="age">Age: </label>
        <input name="age" type="number">

        <br>
        <br>

        <label for="role">Role: </label>
        <input name="role" type="text">

        <br>
        <br>

        <label for="email">Email: </label>
        <input name="email" type="text">

        <br><br><br><br>

        <input type="button" value="Update Book">
    </form>

    <?php

    $con = new mysqli("localhost", "root", "", "Bookmanagement");

    if (!$con) {
        die('Could not connect: ' . mysqli_error());
    }

    if ($_SERVER["REQUEST_METHOD"] == "POST") {
        $Bookname = mysqli_real_escape_string($con, $_POST["Bookname"]);
        $password1 = mysqli_real_escape_string($con, $_POST["password"]);
        $fullname1 = mysqli_real_escape_string($con, $_POST["fullname"]);
        $age = mysqli_real_escape_string($con, $_POST["age"]);
        $role = mysqli_real_escape_string($con, $_POST["role"]);
        $email = mysqli_real_escape_string($con, $_POST["email"]);

        echo $_POST["Bookname"];

        $sql = "UPDATE user SET username = '$username', password ='$password', full-name='$fullname'
        , age = '$age', role = '$role', email = '$email'";

        if (!mysqli_query($con, $sql)) {
            printf("%d Book updated.\n", mysqli_affected_rows($con));
        }
        mysqli_close($con); 
    }
    ?>
</body>

</html>