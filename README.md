# Train Exercise

## What is this? 

This is a programming exercise based on the one presented here: https://github.com/gehel/exo-train

## Assumptions 

1. Trains always "move" right-to-left. Nothing in the test cases suggests otherwise. So, the left-hand end of the string represents the front of the train in all cases. 
2. "Fill" always changes an entire freight car in a single go, not character-by-character. Cars are always filled front-to-back on the train. 

## Solutions

- [Issues with the exercise](issues.md)




## Bash solution 

I chose to do a solution in bash so the implementation would be very different from Java - in particular, to avoid the ideas of _classes_ and _exceptions_. Most examples and exercises you can find online assume Java will be the only language ever used for anything. As a "community," we need to keep our skills at a more general level than any single language. Therefore, it's good to work exercises in different types of languages. Otherwise, we risk "thinking in Java" all the time. 

For this solution, I used classic-style TDD with the unit test framework, bash-spec: https://github.com/neopragma/bash-spec. I chose that framework because it tends to guide the programmer toward a modular and functional-ish design. Shell scripts typically end up with a monolithic design that is hard to test at the unit level. 

The test functions in _train_spec_ more-or-less follow those given in the original exercise, with the multi-assertion cases broken out into separate cases. The functions are called like this: 

```shell 
print_header train 
passenger_train
restaurant_train
freight_train
load_first_freight_car
load_second_freight_car
load_last_freight_car
attempt_to_load_freight_car_when_train_is_full
assemble_mixed_train
load_first_freight_car_on_a_mixed_train
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
     PASS: it loads the first empty car starting from the front of the train
     PASS: it loads the second empty car starting from the front of the train
     PASS: it loads the last empty car starting from the front of the train
     PASS: it reports an error on attempt to load freight car when train is full
     PASS: it assembles a train containing passenger cars and freight cars
     PASS: it loads the first freight car on a mixed train
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
  load_first_freight_car
  load_second_freight_car
  load_last_freight_car
  attempt_to_load_freight_car_when_train_is_full

describe "A mixed passenger and freight train"  
  assemble_mixed_train
  load_first_freight_car_on_a_mixed_train

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
     PASS: it loads the first empty car starting from the front of the train
     PASS: it loads the second empty car starting from the front of the train
     PASS: it loads the last empty car starting from the front of the train
     PASS: it reports an error on attempt to load freight car when train is full

A mixed passenger and freight train
     PASS: it assembles a train containing passenger cars and freight cars
     PASS: it loads the first freight car on a mixed train

12 examples were run
12 passed
0 failed
``` 

I think both the test source and the test output are more descriptive of the application's behavior than the original version. I also think this sort of thing should be part of any TDD-based or unit test-based code kata. The structure and naming of the examples are important factors in the overall quality of the code base, and should be practiced along with other basic development skills. 

### Next steps 

For the bash solution, we could develop a console UI based on the bash _dialog_ feature to enable users to build different kinds of trains. We could also add functionality such as unloading a freight train, or having passengers open and close their windows. 

