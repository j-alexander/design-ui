namespace CoreGraphicsFun

open System
open System.Drawing

open UIKit
open Foundation

[<Register ("InitialViewController")>]
type InitialViewController() as self =
    inherit UIViewController()

    do
        self.View <- (new InitialView() :> UIView)

    override this.DidReceiveMemoryWarning () =
        base.DidReceiveMemoryWarning ()