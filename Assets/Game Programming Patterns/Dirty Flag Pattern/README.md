# Dirty Flag Pattern 

## Intent 

Avoid unnecessary work by deferring it until the result is needed.




## The Pattern 

A set of primary data changes over time. A set of derived data is determined from this using some expensive process. A “dirty” flag tracks when the derived data is out of sync with the primary data. It is set when the primary data changes. If the flag is set when the derived data is needed, then it is reprocessed and the flag is cleared. Otherwise, the previous cached derived data is used.





## When to Use It


Just like other optimization modes, this mode will increase code complexity. Only consider using this mode when there are sufficiently large performance issues.

Dirty flags apply in these two situations:
-The current task has expensive computational overhead
-The current task has expensive synchronization overhead.
If one of the two is satisfied, that is, the conversion of the two from the original data to the target data will consume a lot of time, and the dirty mark mode can be considered to save overhead.

If the change speed of the original data is much higher than the use speed of the target data, the data will become invalid due to subsequent modification, and it is not suitable to use the dirty mark mode at this time.
