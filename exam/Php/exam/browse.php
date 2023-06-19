<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

$con = new mysqli("localhost", "root", "", "db1");
if (mysqli_connect_errno()) {
  echo "Failed to connect to MySQL: " . mysqli_connect_error();
  exit();
}

$sql = "SELECT * FROM websites";
$result = mysqli_query($con, $sql);


// Output the data
if (mysqli_num_rows($result) > 0) {
    // output data of each row
    while($row = mysqli_fetch_assoc($result)) {
      $id1 = $row["id"];
      $url = $row["url"];

      $sql = "SELECT Count(*) AS C FROM documents WHERE idwebsites =  '$id1'";
      $result1 = mysqli_query($con, $sql);
      $number =  mysqli_fetch_assoc($result1)["C"];
      
      echo "id: " . $id1 ."url: " . $row["url"] . " number documents: " . $number . "<br>";
    }
  } else {
    echo "0 results";
  }

mysqli_close($con);

?>