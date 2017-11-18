module FsUno.Tests.``When starting game``

open NUnit.Framework
open System
open Game


[<Test>]
let ``Started game should be started``() =
    Given []
    |> When ( StartGame{ GameId = GameId 1; PlayerCount = 4; FirstCard = Digit(3, Red)})
    |> Expect [ GameStarted{ GameId = GameId 1; PlayerCount = 4; FirstCard = Digit(3, Red)} ]

[<Test>]
let ``0 players should be rejected``() =
    Given []
    |> When ( StartGame{ GameId = GameId 1; PlayerCount = 0; FirstCard = Digit(3, Red)} )
    |> ExpectThrows<ArgumentException>


[<Test>]
let ``Game should not be started twice``() =
    Given [GameStarted{ GameId = GameId 1; PlayerCount = 4; FirstCard = Digit(3, Red)} ]
    |> When ( StartGame{ GameId = GameId 1; PlayerCount = 4; FirstCard = Digit(2, Red)} )
    |> ExpectThrows<InvalidOperationException>
