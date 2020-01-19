# Train Exercise

## What is this? 

This is a programming exercise based on the one presented here: https://github.com/gehel/exo-train

## Assumptions 

1. Trains always "move" right-to-left. Nothing in the test cases suggests otherwise. So, the left-hand end of the string represents the front of the train in all cases. 
2. "Fill" always changes an entire freight car in a single go, not character-by-character. Cars are always filled front-to-back on the train. 

## Issues with the Exercise

- As this is a coding challenge or kata and the "requirements" are expressed _via_ the unit-level examples, it's okay that the programmer is given several examples at once. In "real life" (whatever _that_ means) when using classic-style TDD we would write exactly one failing test case at a time. It might be useful to mention this in the instructions, in case a novice practitioner of TDD or of unit testing should get the wrong idea when they see multiple examples presented at once.
- If this is meant as a classic-style TDD exercise, then it is an error to give the programmer any production code whatsoever as a starting point. There should be only test cases (unit-level examples). As presented, a mostly-empty Java class is given. However, no test case has yet "forced" the programmer to create a class, and the process of emergent design has not revealed any domain objects that one might wish to represent as classes. When doing code katas, it's useful for us to pretend we don't already know that _train_ will be a domain object, so that we can practice the _technique_ of emergent design. The goal isn't to create an ASCII train, but to practice technique. In addition, the exercise can be more broadly useful if no source language is assumed. Not all source languages have "classes."
- The instructions state that when an error occurs an exception should be thrown. However, not all languages handle errors by throwing exceptions. To avoid coupling the unit test suite with the implementation at too low a level, it might be advisable to assert that the application follows whatever error-handling conventions have been agreed by the team or defined in the organization where the team works. Granted, it is difficult to avoid this degree of coupling with implementation while still having a useful unit test suite. If some degree of abstraction is feasible, it might be worth considering. For Java in particular, asking programmers to throw exceptions on a one-off basis, without any sort of structure for exception-handling across the whole application or the whole production environment, invites anti-patterns like "swallowed" exceptions, stack traces that are visible to end users, and meaningless or hard-to-understand log messages. There is no value in practicing such anti-patterns in a code kata.
- Some of the examples include more than one behavior, and more than one logical assertion. This is an error in unit test design. Whether it includes TDD and emergent design or not, any programming exercise that includes unit tests should address unit test design.
- There's a strong emphasis in the industry today on _naming_ and on structuring test suites in a way that helps people understand what the application does. In the examples, the _modifyTrain()_ test method calls _detachHead()_ and _detachEnd()_ methods (yes, two different behaviors expressed in the same example). "Head" and "end" are not corresponding words in English. One could say "head" and "tail," or "beginning" and "end," or "front" and "back," or "leading" and "trailing," or any other word pair that goes together fluidly. The name _modifyTrain()_ is not expressive of the behavior of the application. In addition, the write-up describes a freight train as a "cargo train," which is not the correct (or at least, not the common) terminology. In "real life," when people are careless with names in that way, it indicates they have not understood the problem domain adequately. Whether it includes TDD and emergent design or not, taking care with names should be part of any programming exercise.

## Bash solution 

I chose to do a solution in bash so the implementation would be very different from Java - in particular, to avoid the ideas of _classes_ and _exceptions_. Most examples and exercises you can find online assume Java will be the only language ever used for anything. As a "community," we need to keep our skills at a more general level than any single language. Therefore, it's good to work exercises in different types of languages. Otherwise, we risk "thinking in Java" all the time. 

For this solution, I used classic-style TDD with the unit test framework, bash-spec: https://github.com/neopragma/bash-spec. I chose that framework because it tends to guide the programmer toward a modular and functional-ish design. Shell scripts typically end up with a monolithic design that is hard to test at the unit level. 

The test functions in _train_spec_ more-or-less follow those given in the original exercise, with the multi-assertion cases broken out into separate cases. The functions are called like this: 

```shell 
print_header train 
passenger_train
restaurant_train
freight_train
fill_first_freight_car
fill_second_freight_car
fill_last_freight_car
attempt_to_fill_freight_car_when_train_is_full
assemble_mixed_train
fill_first_freight_car_on_a_mixed_train
double_headed_train 
detach_last_car 
detach_first_car
print_trailer
``` 

The test output looks like this: 

```shell 
Examples for train

     PASS: it assembles a passenger train
     PASS: it assembles a restaurant train
     PASS: it assembles an empty freight train
     PASS: it fills the first empty car starting from the front of the train
     PASS: it fills the second empty car starting from the front of the train
     PASS: it fills the last empty car starting from the front of the train
     PASS: it reports an error on attempt to fill freight car when train is full
     PASS: it assembles a train containing passenger cars and freight cars
     PASS: it fills the first freight car on a mixed train
     PASS: it assembles a double-headed train
     PASS: it detaches the last car of the train (right)
     PASS: it detaches the first car of the train (left)

12 examples were run
12 passed
0 failed
```

In _train_spec_2_ the same examples are coded but the function calls are organized this way: 

```shell 
print_header train 

describe "General train assembly"
  double_headed_train 
  detach_last_car 
  detach_first_car

describe "A passenger train"
  passenger_train
  restaurant_train

describe "A freight train"  
  freight_train
  fill_first_freight_car
  fill_second_freight_car
  fill_last_freight_car
  attempt_to_fill_freight_car_when_train_is_full

describe "A mixed passenger and freight train"  
  assemble_mixed_train
  fill_first_freight_car_on_a_mixed_train

print_trailer
```

The test output looks like this: 

```shell 
Examples for train

General train assembly
     PASS: it assembles a double-headed train
     PASS: it detaches the last car of the train (right)
     PASS: it detaches the first car of the train (left)

A passenger train
     PASS: it assembles a passenger train
     PASS: it assembles a restaurant train

A freight train
     PASS: it assembles an empty freight train
     PASS: it fills the first empty car starting from the front of the train
     PASS: it fills the second empty car starting from the front of the train
     PASS: it fills the last empty car starting from the front of the train
     PASS: it reports an error on attempt to fill freight car when train is full

A mixed passenger and freight train
     PASS: it assembles a train containing passenger cars and freight cars
     PASS: it fills the first freight car on a mixed train

12 examples were run
12 passed
0 failed
``` 

I think both the test source and the test output are more descriptive of the application's behavior than the original version. I also think this sort of thing should be part of any TDD-based or unit test-based code kata. The structure and naming of the examples are important factors in the overall quality of the code base, and should be practiced along with other basic development skills. 



