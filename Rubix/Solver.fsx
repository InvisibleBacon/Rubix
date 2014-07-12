#r "../references/OpenTK.dll"
#r "../references/OpenTK.GLControl.dll"
#load "functional3d.fs"

open Functional3D
open System.Drawing

type cublet = 
    | Green
    | Yellow
    | Red
    | Orange
    | Blue
    | White

let getCubletColor = function
    | Green -> Color.Green
    | Yellow -> Color.Yellow
    | Red -> Color.Red
    | Orange -> Color.Orange
    | Blue -> Color.Blue
    | White -> Color.White

let buildSolvedFace (c : cublet) =
   Array2D.init 3 3 (fun x y -> c)

let flatten (A:'a[,]) = A |> Seq.cast<'a>

type cubeFace = cublet [,]

let renderFace (face : cubeFace) =
    face
    |> Array2D.mapi (
        fun x y cublet -> 
            Fun.cube
            |> Fun.color Color.Black
            $
            (Fun.cube
                |> Fun.color (getCubletColor cublet)
                |> Fun.scale(1.0, 1.0, 0.1)
                |> Fun.translate (0.0, 0.0, 0.5))
            |> Fun.scale(0.9, 0.9, 0.9)
            |> Fun.translate (float x, float y, 0.0)
    )
    |> flatten
    |> Seq.reduce ($)

type cube = {
    front : cubeFace;
    back : cubeFace;
    left: cubeFace;
    right: cubeFace;
    top: cubeFace;
    bottom: cubeFace;
}

let solved = {
    front = (buildSolvedFace cublet.Red);
    back = (buildSolvedFace cublet.Orange);
    left = (buildSolvedFace cublet.Green);
    right = (buildSolvedFace cublet.Blue);
    top = (buildSolvedFace cublet.White);
    bottom = (buildSolvedFace cublet.Yellow);
}

let renderCube (c : cube) =
    renderFace c.front $
    (renderFace c.left |> Fun.rotate (0.0, 270.0, 0.0) |> Fun.translate (0.0, 0.0, -2.0)) $
    (renderFace c.right |> Fun.rotate (0.0, 90.0, 0.0) |> Fun.translate (2.0, 0.0, 0.0)) $
    (renderFace c.back |> Fun.rotate (0.0, 180.0, 0.0) |> Fun.translate (2.0, 0.0, -2.0)) $
    (renderFace c.top |> Fun.rotate (270.0, 0.0, 0.0) |> Fun.translate (0.0, 2.0, 0.0)) $
    (renderFace c.bottom |> Fun.rotate (90.0, 0.0, 0.0) |> Fun.translate (0.0, 0.0, -2.0))
    |> Fun.translate (-1.0, -1.0, 1.0)

let test =
    solved
    |> renderCube
