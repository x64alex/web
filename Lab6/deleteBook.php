<!DOCTYPE html>
<html>

<body>
    <nav>
        <a href="browseBook.php">Browse Book</a>
        <a href="addBook.html">Add Book</a>
        <a href="updateBook.html">Update Book</a>
        <a href="deleteBook.html">Delete Book</a>
    </nav>

    <?php

$conn = new mysqli("localhost", "root", "", "example");

if (!$conn) {
  die('Could not connect: ' . mysqli_error());
}
$title = $_POST["titleDelete"];

// sql to delete a record
$sql = "DELETE FROM Book where title = '$title'";

if (mysqli_query($conn, $sql)) {
  echo "Record deleted successfully";
} else {
  echo "Error deleting record: " . mysqli_error($conn);
}
$conn->close();

?>

</body>

</html>