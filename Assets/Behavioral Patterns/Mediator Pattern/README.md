# Mediator Pattern 
## Definition

Define an object that encapsulates how a set of objects interact. Mediator promotes loose coupling by keeping objects from referring to each other explicitly, and it lets you vary their interaction independently.



## Participants

The classes and objects participating in this pattern are:

### Mediator  (IChatroom)
* defines an interface for communicating with Colleague objects

### ConcreteMediator  (Chatroom)
* implements cooperative behavior by coordinating Colleague objects
* knows and maintains its colleagues

### Colleague classes  (Participant)
* each Colleague class knows its Mediator object
* each colleague communicates with its mediator whenever it would have otherwise communicated with another colleague

