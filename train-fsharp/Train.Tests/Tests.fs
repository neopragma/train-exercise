module Tests

open System
open Xunit
open Train.Trains
open Train.StringExtensions

[<Fact>]
let ``It assembles a passenger train`` () =
    Assert.Equal("<HHHH::|OOOO|::|OOOO|", choochoo("HPP"))

[<Fact>]
let ``It assembles a restaurant train`` () =
    Assert.Equal("<HHHH::|OOOO|::|hThT|::|OOOO|", choochoo("HPRP"))

[<Fact>]
let ``It assembles a double-headed train`` () =
    Assert.Equal("<HHHH::|OOOO|::|hThT|::|OOOO|::HHHH>", choochoo("HPRPH"))

[<Fact>]
let ``It assembles a freight train`` () =
    Assert.Equal("<HHHH::|____|::|____|::|____|", choochoo("HCCC"))

[<Fact>]
let ``It loads the first available freight car`` () =
    Assert.Equal("<HHHH::|^^^^|::|____|::|____|", loadNextCar(choochoo("HCCC")))

[<Fact>]
let ``It loads the last freight car`` () =
    let mutable thetrain = choochoo("HCCC")
    thetrain <- loadNextCar(thetrain)
    thetrain <- loadNextCar(thetrain)
    Assert.Equal("<HHHH::|^^^^|::|^^^^|::|^^^^|", loadNextCar(thetrain))

[<Fact>]
let ``It throws on attempt to load full train`` () = 
    let mutable thetrain = choochoo("HC") 
    thetrain <- loadNextCar(thetrain) 
    Assert.Throws<FullTrainException>(fun () ->
        loadNextCar(thetrain) |> ignore 
    ) |> ignore    

[<Fact>]
let ``It detaches the first car`` () = 
    Assert.Equal("|OOOO|::|hThT|::|OOOO|", detachFirstCar(choochoo("HPRP")))

[<Fact>]
let ``It detaches the last car`` () =
    Assert.Equal("<HHHH::|OOOO|::|hThT|", detachLastCar(choochoo("HPRP")))
