# Facade Pattern 
## Definition

Provide a unified interface to a set of interfaces in a subsystem. Façade defines a higher-level interface that makes the subsystem easier to use.



## Participants

The classes and objects participating in this pattern are:

### Facade   (MortgageApplication)
* knows which subsystem classes are responsible for a request.
* delegates client requests to appropriate subsystem objects.

### Subsystem classes   (Bank, Credit, Loan)
* implement subsystem functionality.
* handle work assigned by the Facade object.
* have no knowledge of the facade and keep no reference to it.

