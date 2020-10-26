# State Pattern 
## Definition

Allow an object to alter its behavior when its internal state changes. The object will appear to change its class.



## Participants

The classes and objects participating in this pattern are:

### Context  (Account)
* defines the interface of interest to clients
* maintains an instance of a ConcreteState subclass that defines the current state.

### State  (State)
* defines an interface for encapsulating the behavior associated with a particular state of the Context.

### Concrete State  (RedState, SilverState, GoldState)
* each subclass implements a behavior associated with a state of Context

