namespace CoreGraphicsFun

open System
open System.Drawing

open CoreGraphics
open UIKit
open Foundation
open CoreAnimation

[<Register ("InitialView")>]
type InitialView() as self =
    inherit UIView()

    do
        let X, Y =
            let bounds = UIScreen.MainScreen.Bounds
            bounds.Width / nfloat 2.0f, bounds.Height / nfloat 2.0f

        self.BackgroundColor <- UIColor.White
        self.AddSubviews(
            [| nfloat 0.5f; nfloat 1.0f; nfloat 1.5f |]
            |> Array.map (fun y ->
                let view = new TriangleView()
                view.Center <- new CGPoint(X, y * Y)
                view :> UIView))

    override x.TouchesBegan(touches : NSSet, e : UIEvent) =
        let touch = touches.AnyObject :?> UITouch
        if (touch.TapCount = nint 2) then
            let view = new TriangleView()
            view.Center <- touch.LocationInView(self)
            view :> UIView |> x.AddSubview