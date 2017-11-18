open FsUno
open Game
open CommandHandlers

// uncomment to use the toy in-memory event store
open ToyInMemoryEventStore

// uncomment to use async agent version (against the event store)
//open Async
//let readStream store streamId version count =
//    async { return readStream store streamId version count }
//let appendToStream store streamId expectedVersion newEvents =
//    async { return appendToStream store streamId expectedVersion newEvents }

// uncomment to use the EventStore
//open EventStore


[<EntryPoint>]
let main _ =

    let eventHandler = new EventHandler()
    use store =
        create()
        |> subscribe eventHandler.Handle


    let handle = Game.create (readStream store) (appendToStream store)

    handle (StartGame { GameId = GameId 1; PlayerCount = 4; FirstCard = Digit(3, Red)})

    handle (PlayCard { GameId = GameId 1; Player = 0; Card = Digit(3, Blue)})

    handle (PlayCard { GameId = GameId 1; Player = 1; Card = Digit(8, Blue)})

    handle (PlayCard { GameId = GameId 1; Player =  2; Card = Digit(8, Yellow)})

    handle (PlayCard { GameId = GameId 1; Player = 3; Card = Digit(4, Yellow)})

    handle (PlayCard { GameId = GameId 1; Player = 0; Card = Digit(4, Green)})


    System.Console.ReadLine() |> ignore

    0 // return an integer exit code
