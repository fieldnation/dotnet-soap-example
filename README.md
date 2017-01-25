# dotnet-soap-example
Example consuming Field Nation SOAP API with C# and ASP.NET 4.5 MVC


### To Use
==========

* Generate a SOAP API token with your admin user account at https://app.fieldnation.com/api/
* Modify the Web.config to include this token and any other defaults you may want to change
* Set the FN_HASH_SECRET in the Web.config and then enable pushback at https://app.fieldnation.com/integration/pushback/setup.php

The pushback handler is defined in Controllers/Api/PushbackController.cs. This should give you an example on how to validate the Field Nation hash and how to respond to the pushback. You will need to implement the logic to handle pushback objects in a way that makes sense for your domain.

The mapping of your WorkOrder/Ticket/etc. that needs to get created on Field Nation is found in Models/WorkOrder.cs. There is a method in this file that will map your business object into a Field Nation WorkOrder object and create it on Field Nation. You can review the object schema at https://api.fieldnation.com/docs/soap/types. Update the mapping to fit your business logic.
