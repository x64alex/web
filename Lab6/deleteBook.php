<!DOCTYPE html>
<html>

<head>
    <title>Book Management DU</title>
    <link rel="stylesheet" href="style.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
</head>

<body>
    <nav>
        <a href="browseBook.php">Browse Book</a>
        <a href="addBook.php">Add Book</a>
        <a href="updateBook.php">Update Book</a>
        <a href="#">Delete Book</a>
    </nav>

    <div id="bu">
        <form method="post" action="<?php echo $_SERVER['PHP_SELF']; ?>">
            <label for="idDel">Book title to delete: </label>
            <input type="text" name="idDel">

            <input type="button" value="Delete">
        </form>
    </div>

    <?php

    $con = new mysqli("localhost", "root", "", "Bookmanagement");

    if (!$con) {
        die('Could not connect: ' . mysqli_error());
    }

    if ($_SERVER["REQUEST_METHOD"] == "POST") {
        $nameToDelete = mysql_real_escape_string($_POST["idDel"]);

        if ($nameToDelete) {
            $sql = "DELETE FROM Book WHERE (Bookname = '$nameToDelete' AND password = '$passToDelete')";
            $con->query($sql);
        }

        mysqli_close($con);
    }
    ?>

</body>

</html>