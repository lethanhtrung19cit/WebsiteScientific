<?php
$conn=mysqli_connect("localhost","root","");
mysqli_select_db($conn,"android_db");

$uname=$_GET['t1'];
$pwd=$_GET['t2'];

$qry="select * from tbl_user where username='$uname' and password='$pwd'";

$raw=mysqli_query($conn,$qry);

$count=mysqli_num_rows($raw);

if($count>0)
 echo "found";
else
 echo "not found";
?>