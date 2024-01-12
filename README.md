

POC Question Asking Assaigment 

Instructions :

to run locally in port 44383: 
https://localhost:44383/api/questions

to run with Swagger:
https://localhost:44383/Index.html

In appsettings.json file change the data source and database in ConnectionString in order to connect to sql server. 

in Package Manager Console run :

add-migration -o Data initial

update-database

