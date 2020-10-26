# Component Pattern 

## Intent 

Allow a single entity to span multiple domains without coupling the domains to each other




## The Pattern 

A single entity spans multiple domains. To keep the domains isolated, the code for each is placed in its own component class. 
The entity is reduced to a simple container of components.




## When to Use It 

Components are most commonly found within the core class that defines the entities in a game, but they may be useful in other places as well. This pattern can be put to good use when any of these are true:

- You have a class that touches multiple domains which you want to keep decoupled from each other.
- A class is getting massive and hard to work with.
- You want to be able to define a variety of objects that share different capabilities, but using inheritance doesn’t let you pick the parts you want to reuse precisely enough.

