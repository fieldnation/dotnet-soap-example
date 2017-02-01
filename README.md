# dotnet-soap-example
Example consuming Field Nation SOAP API with C# and ASP.NET 4.5 MVC


### To Use
==========

* Generate a SOAP API token with your admin user account at https://app.fieldnation.com/api/
* Modify the Web.config to include this token and any other defaults you may want to change
* Set the FN_HASH_SECRET in the Web.config and then enable pushback at https://app.fieldnation.com/integration/pushback/setup.php

The pushback handler is defined in Controllers/Api/PushbackController.cs. This should give you an example on how to validate the Field Nation hash and how to respond to the pushback. You will need to implement the logic to handle pushback objects in a way that makes sense for your domain.

The mapping of your WorkOrder/Ticket/etc. that needs to get created on Field Nation is found in Models/WorkOrder.cs. There is a method in this file that will map your business object into a Field Nation WorkOrder object and create it on Field Nation. You can review the object schema at https://api.fieldnation.com/docs/soap/types. Update the mapping to fit your business logic.

### Creating a Service Reference

The calls to Field Nation are made for a service reference in the application. In visual studio you need to right click on your project in your solution, and add a service reference. The dialog will ask you for a URL for the service (in our case, the wsdl URL), and then will ask you about the methods you want created. After you click okay, all of the methods will be created for you.


###  Using your service reference

You can see an example that uses the service reference in Models/WorkOrder.cs. This partial class has a method that will map your business object into a Field Nation work order object. Please note that you do need to create a [SOAP client](https://github.com/fieldnation/dotnet-soap-example/blob/master/FieldNationApp/FieldNationApp/Models/WorkOrder.cs#L12) that will then make the request to the SOAP service. All of the wsdl methods/types will be exposed on the `using FieldNationApp.FieldNationSoapService;` class.

### Overriding the WSDL

It may be beneficial to change the wsdl URL at some point. There are a couple of ways to accomplish this. One way is to update the "address" string [here](https://github.com/fieldnation/dotnet-soap-example/blob/master/FieldNationApp/FieldNationApp/Web.config#L112). If you need to dynamically change the address at run time you can do so by overriding the remoteAddress via the `SoapHandlerPortTypeClient` constructor. There are [a number of constructor overloads](https://github.com/fieldnation/dotnet-soap-example/blob/master/FieldNationApp/FieldNationApp/Service%20References/FieldNationSoapService/Reference.cs#L5278-L5295) that will allow you to pass a remoteAddress. You could change the example [here](https://github.com/fieldnation/dotnet-soap-example/blob/master/FieldNationApp/FieldNationApp/Models/WorkOrder.cs#L12) to set the remoteAddress based on your business logic (say, load a different wsdl based on if the build is debug or release). Our SOAP Client proxy object is derived from the [System.ServiceModel.ClientBase class](https://msdn.microsoft.com/en-us/library/ms576141(v=vs.110).aspx). [Here](https://msdn.microsoft.com/en-us/library/ms574922(v=vs.110).aspx) is an example a constructor that allows you to set the remoteAddress.
