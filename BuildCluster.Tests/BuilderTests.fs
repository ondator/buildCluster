namespace BuildCluster.Tests

module BuilderTests =
    open NUnit.Framework
    open BuildCluster

    [<Test>]
    let ``Build When Null Input Then Error``() = 
        let sut = new Builder() :> IBuilder        
        Assert.Throws<System.ArgumentNullException> 
            (fun () -> sut.Build null |> ignore) 
            |> ignore;

    [<Test>]
    let ``Build When Empty Input Then Error``() = 
        let sut = new Builder() :> IBuilder        
        Assert.Throws<System.ArgumentNullException> 
            (fun () -> sut.Build System.String.Empty |> ignore) 
            |> ignore;


    



