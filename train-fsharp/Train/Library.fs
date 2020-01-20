namespace Train

open Train.StringExtensions

module Trains = 

    exception FullTrainException of string 
 
    let cowcatcher (train : string) : string = 
        let mutable result = train
        if train.StartsWith("H") then 
            result <- "<" + result 
        if train.EndsWith("H") then 
            result <- result + ">"    
        result            

    let choochoo trainspec : string =
        let mutable sb = System.Text.StringBuilder()
        let mutable delim = ""
        String.iter(fun c ->
            sb <- sb.Append(delim)
            match c with 
            | 'C' ->
                sb <- sb.Append("|____|")
            | 'H' ->
                sb <- sb.Append("HHHH")
            | 'P' -> 
                sb <- sb.Append("|OOOO|")
            | 'R' -> 
                sb <- sb.Append("|hThT|")    
            delim <- "::"
        ) trainspec 
        cowcatcher(sb.ToString())
 
    let loadNextCar (thetrain: string) : string = 
        if thetrain.IndexOf("|____|") < 0 then 
            raise (FullTrainException("""Can't load a full train!"""))
        else    
            thetrain.ReplaceFirst("|____|", "|^^^^|")

    let detachFirstCar (thetrain: string) : string = 
           let ix = thetrain.IndexOf("::")
           if ix < 0 then 
               thetrain
           else 
               thetrain.Substring(ix+2)     

    let detachLastCar (thetrain: string) : string =
        let ix = thetrain.LastIndexOf("::")
        if ix < 0 then 
            thetrain
        else 
            thetrain.Remove(ix)
 