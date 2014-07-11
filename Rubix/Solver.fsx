#r "../references/OpenTK.dll"
#r "../references/OpenTK.GLControl.dll"
#load "functional3d.fs"

open Functional3D
open System.Drawing

type Cublet = 
    | Green
    | Yellow
    | Red
    | Orange
    | Blue
    | White

let buildSolvedFace (c : Cublet) =
   Array2D.init 3 3 (fun x y -> c)

let flatten (A:'a[,]) = A |> Seq.cast<'a>

let renderFace (face : Cublet [,]) =
    face
    |> Array2D.mapi (
        fun x y color -> 
            Fun.cube
            |> Fun.color Color.Blue
            |> Fun.scale(0.9, 0.9, 0.9)
            |> Fun.translate (float x, float y, 0.0)
    )
    |> flatten
    |> Seq.reduce ($)


let test =
    buildSolvedFace Cublet.Red
    |> renderFace
