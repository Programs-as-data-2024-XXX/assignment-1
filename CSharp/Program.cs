using Exercise14;


Expr e = new Add(new CstI(17), new Var("z"));
Console.WriteLine(e.toString());

// Exercise 1.4 (ii)

Expr e2 = new Mul(new Add(new CstI(2), new Var("x")), new Sub(new CstI(3), new Var("y")));
Console.WriteLine(e2.toString());

Expr e3 = new Sub(new CstI(88), new CstI(44));
Console.WriteLine(e3.toString());

Expr e4 = new Add(new Var("q"), new Var("r"));
Console.WriteLine(e4.toString());