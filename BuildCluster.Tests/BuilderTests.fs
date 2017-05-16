namespace BuildCluster.Tests

module BuilderTests =
    open NUnit.Framework
    open BuildCluster

    [<Test>]
    let ``Build When Null Input Then Error``() = 
        let builder = new Builder() :> IBuilder

        let sut = builder.Build null

        Assert.AreEqual((Error [|"can't compile nothing"|]), sut);

    [<Test>]
    let ``Build When Empty Input Then Error``() = 
        let builder = new Builder() :> IBuilder      
        
        let sut = builder.Build System.String.Empty

        Assert.AreEqual((Error [|"can't compile nothing"|]), sut);
    
    [<Test>]
    let ``Build When Do Nothing Valid C# Program Then Success Result``() = 
        let doNothingProgram = @"using System;
                                    namespace A
                                    {
                                        public class DoNothing
                                        {
                                            public void Do()
                                            {
                                            }
                                        }
                                    }"
        
        let builder = new Builder() :> IBuilder      
        
        let sut = builder.Build doNothingProgram

        Assert.AreEqual(Success, sut)

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
        
        let builder = new Builder() :> IBuilder      
        
        let sut = builder.Build doNothingProgram

        Assert.AreEqual(Error [|"can't compile nothing"|], sut)


    



