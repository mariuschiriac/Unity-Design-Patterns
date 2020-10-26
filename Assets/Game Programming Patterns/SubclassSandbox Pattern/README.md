# Subclass Sandbox Pattern 
## Intent 

Define behavior in a subclass using a set of operations provided by its base class.




## The Pattern 

A base class defines an abstract sandbox method and several provided operations. Marking them protected makes it clear that they are for use by derived classes. Each derived sandboxed subclass implements the sandbox method using the provided operations.



## When to Use It 

The Subclass Sandbox pattern is a very simple, common pattern lurking in lots of codebases, even outside of games. If you have a non-virtual protected method laying around, you’re probably already using something like this. Subclass Sandbox is a good fit when:

- You have a base class with a number of derived classes.

- The base class is able to provide all of the operations that a derived class may need to perform.

- There is behavioral overlap in the subclasses and you want to make it easier to share code between them.

- You want to minimize coupling between those derived classes and the rest of the program.





