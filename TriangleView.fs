namespace CoreGraphicsFun

open System
open System.Drawing

open CoreAnimation
open CoreGraphics
open Foundation
open UIKit

type Press = In | Move | Out

type TriangleView() as self =
    inherit UIView()

    let mutable shape = new CGPath()
    let label = new UILabel()

    let mutable pressed = Out
    let press x =
        pressed <- x
        self.SetNeedsDisplay()

    do
        shape.AddLines([| new CGPoint(0.0f,100.0f);
                          new CGPoint(60.0f, 0.0f);
                          new CGPoint(120.0f,100.0f) |])
        shape.CloseSubpath()
        self.Bounds <- shape.BoundingBox
        self.BackgroundColor <- UIColor.Clear

        label.Center <- self.Center
        label.TextAlignment <- UITextAlignment.Center
        label.Font <- UIFont.FromName("Helvetica-Bold", nfloat 10.0f)
        self.AddSubview(label)
    
    override x.Draw(rectangle : CGRect) =
        use graphics = UIGraphics.GetCurrentContext()
        let color, name =
            match pressed with
            | In -> UIColor.Red, "Red"
            | Move -> UIColor.Yellow, "Yellow"
            | Out -> UIColor.Green, "Green"

        color.SetStroke()
        graphics.SetLineDash(nfloat 0.0f, [| nfloat 5.0f; nfloat 2.0f |])
        graphics.SetLineWidth(nfloat 1.2f)
        graphics.AddPath(shape)
        graphics.DrawPath(CGPathDrawingMode.Stroke)

        color.SetFill()
        label.LineBreakMode <- UILineBreakMode.WordWrap
        label.Lines <- nint 0
        label.Text <- sprintf "\n%s\nTriangle View" name
        label.DrawText(rectangle)

    override x.PointInside(point : CGPoint, e : UIEvent) =
        shape.ContainsPoint(point, true)

    override x.TouchesBegan(touches : NSSet, e : UIEvent) =
        press In

        let touch = touches.AnyObject :?> UITouch
        if (touch.TapCount = nint 2) then
            x.RemoveFromSuperview()

    override x.TouchesMoved(touches : NSSet, e : UIEvent) =
        press Move

        let touch = touches.AnyObject :?> UITouch
        let offsetX = touch.PreviousLocationInView(x).X - touch.LocationInView(x).X
        let offsetY = touch.PreviousLocationInView(x).Y - touch.LocationInView(x).Y
        let point = new CGPoint(x.Frame.X - offsetX, x.Frame.Y - offsetY)
        x.Frame <- new CGRect(point, x.Frame.Size)

    override x.TouchesEnded(touches : NSSet, e : UIEvent) =
        press Out
        