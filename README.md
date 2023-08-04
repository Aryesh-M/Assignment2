1) Implemented the Company controller functions, all the way down to data access layer

2) Made all Company controller functions to be asynchronous

3) Created a new repository to get and save employee information with the following data model properties:

* string SiteId,
* string CompanyCode,
* string EmployeeCode,
* string EmployeeName,
* string Occupation,
* string EmployeeStatus,
* string EmailAddress,
* string Phone,
* DateTime LastModified

4) Created an employee controller to get the following properties for client-side:

* string EmployeeCode,
* string EmployeeName,
* string CompanyName,
* string OccupationName,
* string EmployeeStatus,
* string EmailAddress,
* string PhoneNumber,
* string LastModifiedDateTime

5) Added a logger to the solution and add proper error handling
