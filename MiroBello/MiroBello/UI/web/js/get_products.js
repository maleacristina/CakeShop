<script>
	function loadDoc(category){
		var xhttp = new XMLHttpRequest();
		
		xhttp.onreadystatechange = function() {
			if (this.readyState == 4 && this.status == 200) {
				myFunction(this,category);
			}
		};
		xhttp.open("GET", "products_xml.xml", true);
		xhttp.send();
	}
	function myFunction(xml,category_name) {
		var i;
		var xmlDoc = xml.responseXML;
		var prodFromCategory;
		var isFirstTime=true;
		var x = xmlDoc.getElementsByTagName("Product");
		for (i = 0; i <x.length; i++) {
			if(x[i].getElementsByTagName("Category")[0].childNodes[0].nodeValue	== category_name){
			if((i%4 == 0) && (isFirstTime == false)){
				prodFromCategory += "<div class=\"clearfix\"></div></div><div class=\"w3ls_w3l_banner_nav_right_grid1\"><div class=\"col-md-3 w3ls_w3l_banner_left\"><div class=\"hover14 column\"><div class=\"agile_top_brand_left_grid w3l_agile_top_brand_left_grid\"><div class=\"agile_top_brand_left_grid1\"><figure><div class=\"snipcart-item block\"><div class=\"snipcart-thumb\"><a href=\"single.html\"><img src=\"" +
				x[i].getElementsByTagName("Image")[0].childNodes[0].nodeValue +
				"\" alt=\" \" class=\"img-responsive\" /></a><p>" +
				x[i].getElementsByTagName("Name")[0].childNodes[0].nodeValue +
				"</p><h4>$" +
				x[i].getElementsByTagName("Price")[0].childNodes[0].nodeValue +
				"</h4></div><div class=\"snipcart-details\"><form action=\"#\" method=\"post\"><fieldset><input type=\"hidden\" name=\"cmd\" value=\"_cart\" /><input type=\"hidden\" name=\"add\" value=\"1\" /><input type=\"hidden\" name=\"business\" value=\" \" /><input type=\"hidden\" name=\"item_name\" value=\"" +
				x[i].getElementsByTagName("Name")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"amount\" value=\"" +
				x[i].getElementsByTagName("Price")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"discount_amount\" value=\"" +
				x[i].getElementsByTagName("Discount")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"currency_code\" value=\"" +
				x[i].getElementsByTagName("Currency")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"return\" value=\" \" /><input type=\"hidden\" name=\"cancel_return\" value=\" \" /><input type=\"submit\" name=\"submit\" value=\"Add to cart\" class=\"button\" /></fieldset></form></div></div></figure></div></div></div></div>";
			}
			else if ((i%4 == 0) && (isFirstTime == true)){	
				prodFromCategory += "<div class=\"w3ls_w3l_banner_nav_right_grid1\"><div class=\"col-md-3 w3ls_w3l_banner_left\"><div class=\"hover14 column\"><div class=\"agile_top_brand_left_grid w3l_agile_top_brand_left_grid\"><div class=\"agile_top_brand_left_grid1\"><figure><div class=\"snipcart-item block\"><div class=\"snipcart-thumb\"><a href=\"single.html\"><img src=\"" +
				x[i].getElementsByTagName("Image")[0].childNodes[0].nodeValue +
				"\" alt=\" \" class=\"img-responsive\" /></a><p>" +
				x[i].getElementsByTagName("Name")[0].childNodes[0].nodeValue +
				"</p><h4>$" +
				x[i].getElementsByTagName("Price")[0].childNodes[0].nodeValue +
				"</h4></div><div class=\"snipcart-details\"><form action=\"#\" method=\"post\"><fieldset><input type=\"hidden\" name=\"cmd\" value=\"_cart\" /><input type=\"hidden\" name=\"add\" value=\"1\" /><input type=\"hidden\" name=\"business\" value=\" \" /><input type=\"hidden\" name=\"item_name\" value=\"" +
				x[i].getElementsByTagName("Name")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"amount\" value=\"" +
				x[i].getElementsByTagName("Price")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"discount_amount\" value=\"" +
				x[i].getElementsByTagName("Discount")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"currency_code\" value=\"" +
				x[i].getElementsByTagName("Currency")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"return\" value=\" \" /><input type=\"hidden\" name=\"cancel_return\" value=\" \" /><input type=\"submit\" name=\"submit\" value=\"Add to cart\" class=\"button\" /></fieldset></form></div></div></figure></div></div></div></div>";
				isFirstTime=false;
			}
			else{
				prodFromCategory += "<div class=\"col-md-3 w3ls_w3l_banner_left\"><div class=\"hover14 column\"><div class=\"agile_top_brand_left_grid w3l_agile_top_brand_left_grid\"><div class=\"agile_top_brand_left_grid1\"><figure><div class=\"snipcart-item block\"><div class=\"snipcart-thumb\"><a href=\"single.html\"><img src=\"" +
				x[i].getElementsByTagName("Image")[0].childNodes[0].nodeValue +
				"\" alt=\" \" class=\"img-responsive\" /></a><p>" +
				x[i].getElementsByTagName("Name")[0].childNodes[0].nodeValue +
				"</p><h4>$" +
				x[i].getElementsByTagName("Price")[0].childNodes[0].nodeValue +
				"</h4></div><div class=\"snipcart-details\"><form action=\"#\" method=\"post\"><fieldset><input type=\"hidden\" name=\"cmd\" value=\"_cart\" /><input type=\"hidden\" name=\"add\" value=\"1\" /><input type=\"hidden\" name=\"business\" value=\" \" /><input type=\"hidden\" name=\"item_name\" value=\"" +
				x[i].getElementsByTagName("Name")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"amount\" value=\"" +
				x[i].getElementsByTagName("Price")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"discount_amount\" value=\"" +
				x[i].getElementsByTagName("Discount")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"currency_code\" value=\"" +
				x[i].getElementsByTagName("Currency")[0].childNodes[0].nodeValue +
				"\" /><input type=\"hidden\" name=\"return\" value=\" \" /><input type=\"hidden\" name=\"cancel_return\" value=\" \" /><input type=\"submit\" name=\"submit\" value=\"Add to cart\" class=\"button\" /></fieldset></form></div></div></figure></div></div></div></div>";
			}
			}
		}
		document.getElementById("products").innerHTML = prodFromCategory;
	}
</script>