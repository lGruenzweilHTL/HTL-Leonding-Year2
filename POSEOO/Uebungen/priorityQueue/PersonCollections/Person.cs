namespace PersonCollections;

public class Person
{
 public string FirstName { get; set; } = String.Empty;
 public string LastName { get; set; } = String.Empty;

 public override string ToString()
 {
  return $"{LastName} {FirstName}";
 }
 
}
