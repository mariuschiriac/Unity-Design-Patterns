# Data Locality Pattern 

## Intent 

Accelerate memory access by arranging data to take advantage of CPU caching.




## The Pattern

Modern CPUs have caches to speed up memory access. These can access memory adjacent to recently accessed memory much quicker. Take advantage of that to improve performance by increasing data locality — keeping data in contiguous memory in the order that you process it.






## When to Use It 

The first rule of using data locality is to use it when you encounter performance problems. Do not apply it to the corners of the code base that are not frequently used. After optimizing the code, the result is often more complicated and less flexible.

As far as this mode is concerned, you have to confirm that your performance problems are indeed caused by cache misses. If the code is slow for other reasons, this pattern will naturally not help.

A simple method of performance evaluation is to manually add instructions and use a timer to check the time elapsed between two points in the code. In order to find bad cache usage, know how many cache misses and where they occur, you need to use a more sophisticated tool-profilers.

The component pattern is the most common example of optimization for caching. For any critical code that needs to touch a lot of data, it is important to consider data locality.



