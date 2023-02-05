<?php
 if (isset($_GET['interview'])) {
 $outcome=rand(0,100);
 if($outcome>50)
	echo "true";
 else
	echo "false";
 }else{
	echo "ProdajniKanal nije definisan";	
 }
?>
