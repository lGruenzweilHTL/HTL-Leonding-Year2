# The Factory Pattern

## Question 1 (1): What is the Factory Design Pattern?

The Factory Pattern is not really a Pattern. Instead it's a **Code Construct**.
Basically, it simplifies object creation in one place.

The Factory should be the only place that actually refers to concrete classes.

The Factory Pattern reduces code duplication by enforcing the **DRY** Principle, which means **D**on't **R**epeat **Y**ourself.


> If you write the same thing once, you should encapsulate it.
>
> If you write the same thing twice, you should backup and encapsulate it.
>
> If you write the same thing three times, you should stop being a programmer.

## Question 2 (3): What are the main components of the Factory Pattern?
- Factory Method (Creator) - Creates a new Object of the required type.
- Created Object (Product) - The object that has been created.

Let's look at a very basic example: **The Simple Pizza Factory**.
It just creates a pizza of the specified pizza type.

```cs
public class SimplePizzaFactory {
    public static IPizza CreatePizza(PizzaType type, List<string> ingredients) {
        switch (type) {
            case PizzaType.NewYork:
                return new NewYorkPizza(ingredients);

            case PizzaType.Chicago:
                return new ChicagoPizza(ingredients);

            default:
                throw new ArgumentException();
        }
    }
}
```

In this example the `CreatePizza` method is the **Creator**, as it creates the specific Pizza (NewYork or Chicago)
and the `IPizza` interface is the **Product**, as it is the thing that is created.

In this example the **Product** is either a `NewYorkPizza` or a `ChicagoPizza`

## Question 3 (7): What is the role of a Factory Method in the Factory Pattern?

- The **factory method pattern** uses factory methods to deal with the problem of creating objects without having to specify the exact class of the object that will be created.
- This is done by calling a factory method - either specified by an interface and implemented by child classes, or implemented in a base class and optionally overridden by a derived class - rather than by calling a constructor.
