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
  $title = mysqli_real_escape_string($con, $_POST["title"]);
  $author = mysqli_real_escape_string($con, $_POST["author"]);
  $category = mysqli_real_escape_string($con, $_POST["category"]);
  $pages = mysqli_real_escape_string($con, $_POST["pages"]);
  $genre = mysqli_real_escape_string($con, $_POST["genre"]);

  $sql = "INSERT INTO Book (title, author, category, pages, genre) VALUES ( '$title', '$author', '$category','$pages', '$genre')";

  if (mysqli_query($con, $sql)) {
    printf("%d Book added.\n", mysqli_affected_rows($con));
  }
  mysqli_close($con);
}
?>
</body>
</html>