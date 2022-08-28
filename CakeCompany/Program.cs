// See https://aka.ms/new-console-template for more information

using CakeCompany.Provider;

var shipmentProvider = new ShipmentProvider();
string shipmentNumber= shipmentProvider.GetShipment();

Console.WriteLine("Shipment Details-> Shipment ID: " + shipmentNumber);
Console.ReadLine();
