# Event Queue Pattern 

## Intent 

Decouple when a message or event is sent from when it is processed.




## The Pattern 

A queue stores a series of notifications or requests in first-in, first-out order. Sending a notification enqueues the request and returns. The request processor then processes items from the queue at a later time. Requests can be handled directly or routed to interested parties. This decouples the sender from the receiver both statically and in time.




## When to Use It 

If you only want to decouple who receives a message from its sender, patterns like Observer and Command will take care of this with less complexity. You only need a queue when you want to decouple something in time.

I mention this in nearly every chapter, but it’s worth emphasizing. Complexity slows you down, so treat simplicity as a precious resource.

I think of it in terms of pushing and pulling. You have some code A that wants another chunk B to do some work. The natural way for A to initiate that is by pushing the request to B.

Meanwhile, the natural way for B to process that request is by pulling it in at a convenient time in its run cycle. When you have a push model on one end and a pull model on the other, you need a buffer between them. That’s what a queue provides that simpler decoupling patterns don’t.

Queues give control to the code that pulls from it — the receiver can delay processing, aggregate requests, or discard them entirely. But queues do this by taking control away from the sender. All the sender can do is throw a request on the queue and hope for the best. This makes queues a poor fit when the sender needs a response.


## Tips


The main design of the Unity engine is carried out around the component model.
