It is used Entity Framework 6 and code first approach.
The Migrations are enabled.
When run the program for the first time run the two commands below:
1) Add-Migration and 2) Update-Database in the Package Manager Console 
This will create a database in your localhost with a SuperAdmin inside (from the seed method) 
in order to be possible to run the program (because username and password is needed for login).
