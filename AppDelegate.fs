namespace CoreGraphicsFun

open System

open UIKit
open Foundation

[<Register ("AppDelegate")>]
type AppDelegate () =
    inherit UIApplicationDelegate ()

    let window = new UIWindow(UIScreen.MainScreen.Bounds)

    override this.FinishedLaunching (app, options) =
        window.RootViewController <- new InitialViewController()
        window.MakeKeyAndVisible()
        true