namespace Train

module StringExtensions =

    type System.String with
        member x.ReplaceFirst(search : string, replace : string) : string = 
            let pos = x.IndexOf(search)
            if pos < 0 then 
                x
            else    
                x.Substring(0, pos) + replace + x.Substring(pos + search.Length);
