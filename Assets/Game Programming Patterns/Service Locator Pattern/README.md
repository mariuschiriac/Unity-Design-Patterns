# Service Locator Pattern 

## Intent 

Provide a global point of access to a service without coupling users to the concrete class that implements it.




## The Pattern 

A service class defines an abstract interface to a set of operations. A concrete service provider implements this interface. A separate service locator provides access to the service by finding an appropriate provider while hiding both the provider’s concrete type and the process used to locate it.



## When to Use It 


Instead of using a global mechanism to give some code access to an object it needs, first consider passing the object to it instead. That’s dead simple, and it makes the coupling completely obvious. That will cover most of your needs.

But… there are some times when manually passing around an object is gratuitous or actively makes code harder to read. Some systems, like logging or memory management, shouldn’t be part of a module’s public API. The parameters to your rendering code should have to do with rendering, not stuff like logging.

Likewise, other systems represent facilities that are fundamentally singular in nature. Your game probably only has one audio device or display system that it can talk to. It is an ambient property of the environment, so plumbing it through ten layers of methods just so one deeply nested call can get to it is adding needless complexity to your code.

In those kinds of cases, this pattern can help. As we’ll see, it functions as a more flexible, more configurable cousin of the Singleton pattern. When used well, it can make your codebase more flexible with little runtime cost.

The core difficulty with a service locator is that it takes a dependency — a bit of coupling between two pieces of code — and defers wiring it up until runtime. This gives you flexibility, but the price you pay is that it’s harder to understand what your dependencies are by reading the code.




## Tips



The service locator pattern is very similar to the singleton pattern in many ways, so it is worth considering both to decide which one is more suitable for your needs.

The Unity engine combines this mode with the component mode and uses it in the GetComponent() method.