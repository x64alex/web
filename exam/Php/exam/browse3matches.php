<?php
error_reporting(E_ALL);
ini_set('display_errors', 1);

// get the category parameter from URL
$queryString = $_SERVER['QUERY_STRING'];
$queryArray = explode('=', $queryString);
$keywordArray = explode('%20', $queryArray[1]);
// echo "<h2>" . $keywordArray[0] . "</h2>";


$con = new mysqli("localhost", "root", "", "db1");
if (mysqli_connect_errno()) {
  echo "Failed to connect to MySQL: " . mysqli_connect_error();
  exit();
}

$sql = "SELECT * FROM documents";
$result = mysqli_query($con, $sql);

// Output the data
if (mysqli_num_rows($result) > 0) {
    // output data of each row
    while($row = mysqli_fetch_assoc($result)) {
        $numb = 0;
        for ($x1 = 0; $x1 < count($keywordArray); $x1++) {
            if($row["keyword1"] == $keywordArray[$x1] ||
            $row["keyword2"] == $keywordArray[$x1] ||
            $row["keyword3"] == $keywordArray[$x1] ||
            $row["keyword4"] == $keywordArray[$x1] ||
            $row["keyword5"] == $keywordArray[$x1]
            ){
                $numb = $numb +1;
            }
        }
        if($numb==3){
            echo "id: " . $row["id"] ."name: ". $row["name"] . $row["keyword1"]  .$row["keyword2"]  .$row["keyword3"]  .$row["keyword4"]  . $row["keyword5"] . "<br>";
        }
    }
} else {
    echo "0 results";
}





mysqli_close($con);

?>