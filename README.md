# MovieExpress

Application Requirements Overview Each person will register on the platform and fund their account in order to purchase items from the kiosk.


Owner/Administrator (Super User) a) The owner purchases products from various suppliers and records the information in the application using a maintenance screen. b) The owner would also be required to mark some products as not available and these should not be displayed in the application. (The supplier information and different prices from various suppliers does not have to be saved in the application.) c) Your administrator user should have the same functionality as a normal user, i.e. purchase items.

Users a) When a user logs into the system for the first time they should have the option to fund their account. b) The balance should be displayed and a user should be able to fund their wallet by typing in an amount. c) The user will then view the available products page, choose a category and then browse through the available products. d) The user can then choose an item and add it to his/her basket. e) Once added, the user will have the option to repeat the previous step or navigate to checkout to confirm his/her order and choose to have his order collected/delivered. f) The application should check the final basket amount against the available balance and deduct the amount from the user’s account.

## Screenshots

### All Products
Shows all available products in the database that are available, the user can be able to add to cart
<p><img src="METTWeb/Media/Products/AllProducts.PNG" width="300" /></p>

### Filter Products By Drinks
Shows all the drinks that are available 
<p><img src="METTWeb/Media/Products/DrinkFilters.PNG" width="600" height="500" /></p>


### Filter Products By Bread
Shows all the breads that are available 
<p><img src="METTWeb/Media/Products/AllBread.PNG" width="300" /></p>

### Show All Products Added In The CartItem
Shows all the products added into the cart
<p><img src="METTWeb/Media/Products/cartItems.PNG" width="300" /></p>

### Show All Orders With Order Details
Shows all the orders of a specific user with the details of each order
<p><img src="METTWeb/Media/Products/OrderWithOrderDetails.PNG" width="300" /></p>

### Deposit
User can be able to make a deposit
<p><img src="METTWeb/Media/Products/deposit.PNG" width="300" /></p>

### Transaction,
Show All the transactions of a use with the description
<p><img src="METTWeb/Media/Products/transaction.PNG" width="300" /></p>

## Features
* Login/Logout .
* Add Product to cart
* Select delivery type.
* Create order when checking out
* See TransactionList
* Filter Products By Category

## Technologies:
* ASP.NET Core
* RESTFul Services ASP.NET Web API, Entity Frameworke(Code-First).
* CSLA
* Javasscript
* Database: MySQL and SQL Server
* Git for source control
