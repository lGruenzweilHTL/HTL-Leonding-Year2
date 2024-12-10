using System.Text;
using Marathons;

Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("*** Marathon ***");

var p1 = new Participant(591, "Jeb", new(2, 05, 41));
var p2 = new Participant(24, "Bill", new(4, 15, 23));
var p3 = new Participant(167, "Bob", new(3, 34, 18));
var p4 = new Participant(408, "Val", new(2, 05, 41));
var marathon = new Marathon("Linz", new (2022, 10, 30));

marathon.AddParticipant(p1);
marathon.AddParticipant(p2);
marathon.AddParticipant(p3);
marathon.AddParticipant(p4);
marathon.RemoveParticipant(p2.StartNo);

Console.WriteLine($"Here the results for {marathon}:");
foreach (var resultLine in marathon.GetResultList())
{
    Console.WriteLine(resultLine);
}