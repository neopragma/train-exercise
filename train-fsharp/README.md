# F# solution

I wanted to do a solution in F# because it is not object-oriented, and I wanted to practice the exercise in languages quite different from Java. For .NET development, my preferred unit testing framework is xUnit. This is not necessary for the exercise; you can use any testing framework you wish, or none at all. Here is how I set up the F# project: 

```shell
mkdir [project]
cd [project]
dotnet new sln 
mkdir Train
cd Train 
dotnet new classlib -lang "F#"
cd .. 
dotnet sln add Train/Train.fsproj 
mkdir Train.Tests 
cd Train.Tests 
dotnet new xunit -lang "F#"
dotnet add reference ../Train/Train.fsproj
``` 

That sequence of commands results in a single sample unit test case in file Train.fsproj/Tests.fs, named 'My test'. It asserts true, so it passes. Run it to see if you have set up the project properly: 

```shell 
cd Train.Tests 
dotnet test 
```

If things are set up properly, the test framework will find one test case and run it, and it will pass. 

The original unit test cases have the issues described elsewhere in this documentation. We can replace the sample test case with a set of cases that correspond with the original kata specification. To do this in a classic TDD way, we write just one of the test cases at a time, make it pass, and perform any necessary refactoring. 

The first one is:

```java 
    @Test
    public void passengerTrain() {
        Train train = new Train("HPP");
        assertEquals("<HHHH::|OOOO|::|OOOO|", train.print());
    }
``` 

An F#-flavoured xUnit version of that might look like this: 

```
module Tests

open System
open Xunit

[<Fact>]
let ``It assembles a passenger train`` () =
    Assert.Equal("<HHHH::|OOOO|::|OOOO|", choochoo("HPP"))
``` 

This results in "error FS0039: The value or constructor 'choochoo' is not defined," which is correct for this point in the process. 

Add a reference to a namespace and module in the Tests.fs file where you intend to put the production code for the _choochoo_ function, like this: 

```
open Train.Trains 
``` 

Create some code in Trains/Library.fs for the _choochoo_ function: 

```
namespace Train

module Trains =
    let choochoo trainspec : string =
        "this is not a train"
```

When you run _dotnet test_ you should see something like this: 

```shell 
A total of 1 test files matched the specified pattern.
It assembles a passenger train [4ms]
  Error Message:
   Assert.Equal() Failure
          ↓ (pos 0)
Expected: <HHHH::|OOOO|::|OOOO|
Actual:   this is not a train
          ↑ (pos 0)
  Stack Trace:
     at Tests.It assembles a passenger train() in /Users/connexxo/Documents/Projects/train-exercise/train-fsharp/Train.Tests/Tests.fs:line 9
                                                                                                                                                                
Test Run Failed.
Total tests: 1
``` 

This means you're set up to start test-driving the solution. You'll make the first example pass by writing the _simplest thing that could possibly work_, which will be "obviously stupid," like this:

```
    let choochoo trainspec : string =
        "<HHHH::|OOOO|::|OOOO|"
```

That's okay. Part of the technique is to implement only what is necessary. When you know (or _think_ or _believe_ or _wonder if_) the solution needs more, the way to drive out the "more" is by writing additional examples - _not_ by racing ahead with more implementation logic that hasn't been test-driven, and end up with a complicated mess. 

The Shaker song, "Simple Gifts," applies: 

```
'Tis the gift to be simple, 'tis the gift to be free
    'Tis the gift to come down where we ought to be
```

You _can_ write code without test-driving it, but please don't call what you're doing "TDD." If you do so, then the meaning of TDD will become blurred, and the term will lose its sense.

Further examples could include those in the original problem definition or others, such as a passenger train with a different number of cars attached, like "HPPPPP". As you add more examples, the implementation will become more complete and robust. As Robert C. "Uncle Bob" Martin has put it: 

```
As tests become more specific, code becomes more general.
```

After a few rounds of TDD, you might end up with something similar to the implementation in this project. The implementation is probably not a very good example of F#, as that language isn't a specialty of mine. But thanks to the unit test suite, you can improve the implementation with confidence, as your changes will be protected by a "safety net" of unit tests. 

Why not try it yourself? Clone the repo and improve the implementation. See that the examples run "green" both before and after your modifications. That way, you'll know you've improved the _design_ of the code without changing its _behavior_. If you break a test case, it means you inadvertently changed the _behavior_ of the code when your intent was strictly to improve the _design_. 

