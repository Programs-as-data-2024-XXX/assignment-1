(* Programming language concepts for software developers, 2010-08-28 *)

(* Evaluating simple expressions with variables *)

module Intro2

(* Association lists map object language variables to their values *)

let env = [("a", 3); ("c", 78); ("baf", 666); ("b", 111)];;

let emptyenv = []; (* the empty environment *)

let rec lookup env x =
    match env with 
    | []        -> failwith (x + " not found")
    | (y, v)::r -> if x=y then v else lookup r x;;

let cvalue = lookup env "c";;

let max x y : int = 
  if x > y then x else y

let min x y : int = 
  if x < y then x else y

let equals (x : int) (y : int) : int = 
  if x = y then 1 else 0

let exprIf  x y z: int = 
  if x <> 0 then y else z
 
(* Object language expressions with variables *)

type expr = 
  | CstI of int
  | Var of string
  | Prim of string * expr * expr
  | If of expr * expr * expr;;

let e1 = CstI 17;;

let e2 = Prim("+", CstI 3, Var "a");;

let e3 = Prim("+", Prim("*", Var "b", CstI 9), Var "a");;


(* Evaluation within an environment *)

let rec eval e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim("+", e1, e2) -> eval e1 env + eval e2 env
    | Prim("*", e1, e2) -> eval e1 env * eval e2 env
    | Prim("-", e1, e2) -> eval e1 env - eval e2 env
    | Prim("max", e1, e2) -> max (eval e1 env) (eval e2 env)
    | Prim("min", e1, e2) -> min (eval e1 env) (eval e2 env)
    | Prim("==", e1, e2) -> equals (eval e1 env) (eval e2 env) 
    | Prim _            -> failwith "unknown primitive"
    | If(e1, e2, e3) -> exprIf (eval e1 env) (eval e2 env) (eval e3 env);;


let rec eval2 e (env : (string * int) list) : int =
    match e with
    | CstI i            -> i
    | Var x             -> lookup env x 
    | Prim(ope, e1, e2) ->
      let i1 = eval e1 env
      let i2 = eval e2 env
      match ope with
        | "+" -> i1 + i2
        | "*" -> i1 * i2
        | "-" -> i1 - i2
        | "max" -> max i1 i2
        | "min" -> min i1 i2
        | "==" -> equals i1 i2
        | _ -> failwith "unknown primitive"
    | If(e1, e2, e3) -> exprIf (eval e1 env) (eval e2 env) (eval e3 env)

let e1v  = eval e1 env;;
let e2v1 = eval e2 env;;
let e2v2 = eval e2 [("a", 314)];;
let e3v  = eval e3 env;;

(* Exercise 1.2 *)

// (i)
type aexpr =
  | CstI of int
  | Var of string
  | Add of aexpr * aexpr
  | Mul of aexpr * aexpr
  | Sub of aexpr * aexpr

(* (ii)
    1) Sub(Var "v", Add(Var "w", Var "z"))
    2) Mul(CstI 2, Sub(Var "v", Add(Var "w", Var "z"))
    3) Add(Var "v", Add(Var "z", Add(Var "y", Var "x")))
*)

// (iii)
let rec fmt a  = 
  match a with
    | CstI i            -> (string) i
    | Var x             -> x
    | Add (a1, a2) -> "(" + (fmt a1) + "+" +  (fmt a2) + ")"
    | Mul (a1, a2) -> "(" + (fmt a1) + "*" +  (fmt a2) + ")"
    | Sub (a1, a2) -> "(" + (fmt a1) + "-" +  (fmt a2) + ")"
    // Another way to do this would be:
    //| Add (a1, a2) -> sprintf "(%s + %s)" (fmt a1) (fmt a2)
    //| Mul (a1, a2) -> sprintf "(%s * %s)" (fmt a1) (fmt a2)
    //| Sub (a1, a2) -> sprintf "(%s - %s)" (fmt a1) (fmt a2)

// (iv)
let rec simplify a = 
  match a with 
    | Add (CstI 0, a1) -> a1
    | Add (a1, CstI 0) -> a1
    | Sub (a1, CstI 0) -> a1
    | Sub (a1, a2) when a1 = a2 -> CstI 0
    | Mul (CstI 1, a1) -> a1
    | Mul (a1, CstI 1) -> a1
    | Mul (CstI 0, a1) -> CstI 0
    | Mul (a1, CstI 0) -> CstI 0
    | _ -> a

// (v)
let rec differentiate a =
  match a with
  | CstI _ -> CstI 0
  | Var _ -> CstI 1
  | Add (a1, a2) -> Add (differentiate a1, differentiate a2)
  | Sub (a1, a2) -> Sub(differentiate a1, differentiate a2)
  | Mul (a1, a2) -> Add(Mul(differentiate a1, a2), Mul(a1, differentiate a2)) 
