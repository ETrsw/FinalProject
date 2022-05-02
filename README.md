SandwichAPI uses EntityFrameWorkCore and MySql server for its database.



Postman/browser examples of endpoints:
1.	Get a list of sandwiches method GET URL  https://localhost:7068/api/sandwiches this returns a list of sandwich names and their ID 
![getall](https://raw.githubusercontent.com/ETrsw/FinalProject/master/getrequestall.png)	

2.https://localhost:7068/api/sandwiches/{id} to get the name and ingredients of a specific sandwichID using the get method. 

![getone](https://raw.githubusercontent.com/ETrsw/FinalProject/master/getrequest2.png)
 
3. Adding a sandwich to the database :  
![Post Request example](https://raw.githubusercontent.com/ETrsw/FinalProject/master/Postrequest.png)	

Any of the ingredients can be empty only the sandwich name needs to be there.

4.Updating a sandwich name and ingredients using PUT method

Sandwich found using GET request in browser before change :  

![Item before being updated](https://raw.githubusercontent.com/ETrsw/FinalProject/master/Putrequest.png)

PUT Request in postman : 

![Item being updated in postman](https://raw.githubusercontent.com/ETrsw/FinalProject/master/putrequest2.png)

Updated sandiwch in browser after change:

![Item after being updated](https://raw.githubusercontent.com/ETrsw/FinalProject/master/Putrequest3.png)

5. https://localhost:7068/api/sandwiches/random will give you a random sandwich from what is available.
![random example](https://raw.githubusercontent.com/ETrsw/FinalProject/master/random.PNG)
