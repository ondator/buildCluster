namespace BuildCluster

open System.Threading.Tasks

type IBuilder = 
    abstract Build : string -> unit

type Builder() = 

    interface IBuilder with
        member this.Build text = 
            match text with
            | t when System.String.IsNullOrEmpty t -> nullArg "text"
            | _ -> ()
