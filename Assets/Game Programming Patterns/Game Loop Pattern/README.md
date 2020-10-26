# Game Loop Pattern 

## Intent 

Decouple the progression of game time from user input and processor speed.




## The Pattern 

A game loop runs continuously during gameplay. Each turn of the loop, it processes user input without blocking, updates the game state, and renders the game. It tracks the passage of time to control the rate of gameplay.


