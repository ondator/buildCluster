namespace BuildCluster.Tests

module BuilderTests =
    open NUnit.Framework
    open BuildCluster
    open FsUnit

    [<Test>]
    let ``Build When Null Input Then Error``() = 
        let sut = new Builder() :> IBuilder

        sut.Build null |> should equal (Error [|"can't compile nothing"|]);

    [<Test>]
    let ``Build When Empty Input Then Error``() = 
        let sut = new Builder() :> IBuilder      
        
        sut.Build null |> should equal (Error [|"can't compile nothing"|]);
    
    [<Test>]
    let ``Build When Do Nothing Valid C# Program Then Success Result``() = 
        let doNothingProgram = @"using System;
                                    namespace A
                                    {
                                        public class DoNothing
                                        {
                                            public static void Do()
                                            {
                                            }
                                        }
                                    }"
        
        let sut = new Builder() :> IBuilder      
        
        sut.Build doNothingProgram |> should equal Success

    [<Test>]
    let ``Build When Do Nothing InValid C# Program Then Error``() = 
        let doNothingProgram = @"using System;
                                    namespace A
                                    {
                                        public cl ass DoNothing
                                        {
                                            public void Do()
                                            {
                                            }
                                        }
                                    }"
        
        let sut = new Builder() :> IBuilder      
        
        sut.Build doNothingProgram |> should contain "The type or namespace name 'cl' could not be found (are you missing a using directive or an assembly reference?)"


