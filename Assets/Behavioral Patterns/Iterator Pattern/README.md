# Iterator Pattern 
## Definition

Provide a way to access the elements of an aggregate object sequentially without exposing its underlying representation.



## Participants

The classes and objects participating in this pattern are:

### Iterator  (AbstractIterator)
* defines an interface for accessing and traversing elements.

### ConcreteIterator  (Iterator)
* implements the Iterator interface.
* keeps track of the current position in the traversal of the aggregate.

### Aggregate  (AbstractCollection)
* defines an interface for creating an Iterator object

### ConcreteAggregate  (Collection)
* implements the Iterator creation interface to return an instance of the proper ConcreteIterator

