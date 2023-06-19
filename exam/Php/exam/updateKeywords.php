<!DOCTYPE html>
<html>

<body>
<nav>
    <a href="browse.html">All websites</a>
    <a href="browse3matches.html">All websites 3 matches</a>
    <a href="updateKeywords.html">Update keywords</a>
  </nav>

<?php

$con = new mysqli("localhost", "root", "", "db1");

if (!$con) {
  die('Could not connect: ' . mysqli_error());
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
  $id = mysqli_real_escape_string($con, $_POST["id"]);
  $keyword1 = mysqli_real_escape_string($con, $_POST["keyword1"]);
  $keyword2 = mysqli_real_escape_string($con, $_POST["keyword2"]);
  $keyword3 = mysqli_real_escape_string($con, $_POST["keyword3"]);
  $keyword4 = mysqli_real_escape_string($con, $_POST["keyword4"]);
  $keyword5 = mysqli_real_escape_string($con, $_POST["keyword5"]);

  $sql1 = "SELECT Count(*) AS C FROM documents WHERE id =  '$id'";
  $result1 = mysqli_query($con, $sql1);
  $number =  mysqli_fetch_assoc($result1)["C"];


  $sql = "Update documents SET keyword1 = '$keyword1', keyword2= '$keyword2', keyword3 = '$keyword3', keyword4 = '$keyword4', keyword5 = '$keyword5' where id = '$id';";

  echo '<script>console.log("Welcome to GeeksforGeeks! "); </script>';
  echo "{$sql}";

  mysqli_query($con, $sql);

  // Perform a query, check for error

  }
  

?>
</body>

</html>