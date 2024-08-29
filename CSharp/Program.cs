using Exercise14;


Expr e = new Add(new CstI(17), new Var("z"));
Console.WriteLine(e.toString());
