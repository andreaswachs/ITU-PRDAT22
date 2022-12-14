module Exercise_2_Tests

open Xunit
open Exercise_2

[<Fact>]
let ``Free variable is detected`` () =
    // let x = 0   y = a in a + x
    let e = Let([ ("x", CstI 0); ("y", Var "a") ], Prim("+", Var("a"), Var("x")))
    // For some reason Assert.Equal is dumb here
    let isExpected = [ "a" ] = freevars e
    Assert.True(isExpected)

[<Fact>]
let ``tcomp allows for sequential let expressions`` () =
    let e = Let([ ("x", CstI 5); ("y", CstI 10) ], Prim("+", Var("x"), Var("y")))

    let res = tcomp e []

    let expected = TLet (TCstI 5, TLet (TCstI 10, TPrim ("+", TVar 1, TVar 0)))
    Assert.Equal(expected, res)