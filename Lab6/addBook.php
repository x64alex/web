<html>

<head>
  <title>Book Management AU</title>
  <link rel="stylesheet" href="style.css">
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
</head>

<body>

  <nav>
    <a href="browseBook.php">Browse Book</a>
    <a href="#">Add Book</a>
    <a href="updateBook.php">Update Book</a>
    <a href="deleteBook.php">Delete Book</a>
  </nav>

  <form method="post" class="form" id="add-Book">
    <label for="title">Title: </label>
    <input name="title" type="text">

    <br>
    <br>

    <label for="author">Author: </label>
    <input name="author" type="text">

    <br>
    <br>

    <label for="category">Category: </label>
    <input name="category" type="text">

    <br>
    <br>

    <label for="pages">Pages: </label>
    <input name="pages" type="number">

    <br>
    <br>

    <label for="genre">Genre: </label>
    <input name="genre" type="text">

    <br>
    <br>

    <br><br><br><br>

    <input type="submit" value="Add Book" />

  </form>


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

    if (!mysqli_query($con, $sql)) {
      printf("%d Book added.\n", mysqli_affected_rows($con));
    }
    mysqli_close($con);
  }
  ?>
</body>

</html>