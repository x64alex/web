<!DOCTYPE html>
<html>

<body>
<nav>
    <a href="browseBook.html">Browse Book</a>
    <a href="addBook.html">Add Book</a>
    <a href="updateBook.html">Update Book</a>
    <a href="deleteBook.html">Delete Book</a>
  </nav>

<?php

$con = new mysqli("localhost", "root", "", "example");

if (!$con) {
  die('Could not connect: ' . mysqli_error());
}

if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $id = mysqli_real_escape_string($con, $_POST["id"]);
  $title = mysqli_real_escape_string($con, $_POST["title"]);
  $author = mysqli_real_escape_string($con, $_POST["author"]);
  $category = mysqli_real_escape_string($con, $_POST["category"]);
  $pages = mysqli_real_escape_string($con, $_POST["pages"]);
  $genre = mysqli_real_escape_string($con, $_POST["genre"]);

  $sql = "Update Book SET title = '$title', author = '$author', category = '$category', pages = '$pages', genre = '$genre' where id = '$id'";

  if (mysqli_query($con, $sql)) {
    printf("%d Book updated     .\n", mysqli_affected_rows($con));
  }
  mysqli_close($con);
}
?>
</body>

</html>