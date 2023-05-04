<html>
<body>

Welcome <?php echo $_POST["username"]; ?><br>
Your email address is: <?php echo $_POST["email"]; ?>
  Welcomexx <?php echo $_POST["password"]; ?><br>
Your email address isxx: <?php echo $_POST["fullname"]; ?>
  
  
  <?php

            $con = new mysqli("localhost", "root", "", "example");

            if (!$con){
                die('Could not connect: ' . mysqli_error());
            }
            
            if ($_SERVER["REQUEST_METHOD"] == "POST"){
                $username = mysqli_real_escape_string($con,$_POST["username"]);
                $password1 = mysqli_real_escape_string($con,$_POST["password"]);
                $fullname1 = mysqli_real_escape_string($con,$_POST["fullname"]);
                $age = mysqli_real_escape_string($con,$_POST["age"]);
                $role = mysqli_real_escape_string($con,$_POST["role"]);
                $email = mysqli_real_escape_string($con,$_POST["email"]);

                echo $_POST["username"]; 

                $sql = "INSERT INTO user (username, password, fullname, age, role, email) VALUES ( '$username', '$password1', '$fullname1','$age', '$role',  '$email')";
                
               if (!mysqli_query($con, $sql)) {
					printf("%d Row inserted.\n", mysqli_affected_rows($con));
				}
                mysqli_close($con);
            }
        ?>
  </body>
</html>
