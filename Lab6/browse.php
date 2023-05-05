<?php

// get the category parameter from URL
$queryString = $_SERVER['QUERY_STRING'];
$queryArray = explode(':', $queryString);
// echo "<h2>" . $queryArray[1] . "</h2>";

// Get data
$con = new mysqli("localhost", "root", "", "example");
$sql = "SELECT * FROM Book WHERE category = '$queryArray[1]'";
$result = mysqli_query($con, $sql);

// Output the data
if (mysqli_num_rows($result) > 0) {
    // output data of each row
    while($row = mysqli_fetch_assoc($result)) {
      echo "id: " . $row["id"]. " - Title: " . $row["title"]. " - Author: " . $row["author"]. " - Category: " . $row["category"]. " - Genre: " . $row["genre"]."<br>";
    }
  } else {
    echo "0 results";
  }
mysqli_close($con);

?>