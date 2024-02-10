# Minimal APIs - Pizza Store project

This project was created to learn the basics of using minimal APIs on Asp.Net core. It was developed using .Net 8.

It consists on a simple API that uses CRUD operations over SqLite. EF Core was used as O/RM. 

Additionaly, Swagger service was added to create an OpenAPI and test API operations.

Also, Extension methods were created to help on the organization of Program.cs file and placed on *Extensions* folder. This techinique was used because, when using minimal APIs, all the services and middleware declarations alongside all API mappings are made on a single file (Program.cs). Because of this, the file can become less readable. 

## TODO List

The TODO list for this project:

- [ ] Add responses for all operations (Ok, BadRequest...)
- [ ] Handle errors
- [ ] Add logging
- [ ] Add Authenthication and authorization
