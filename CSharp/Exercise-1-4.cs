using System;
using System.Collections;
using System.Runtime.Intrinsics.X86;

namespace Exercise14
{
    public abstract class Expr 
    {
        public abstract int Eval(Dictionary<string, int> env);
        public abstract string toString();    
        public abstract Expr Simplify();
    }

    public class CstI : Expr 
    {
        public int value;

        public CstI(int value) 
        {
            this.value = value;
        }

        public override int Eval(Dictionary<string, int> env) 
        {
            return value;
        }

        public override string toString() 
        {
            return value.ToString();
        }

        public override Expr Simplify() 
        {
            return this;
        }
    }

    public class Var : Expr 
    {
        public string name;

        public Var(string name) 
        {
            this.name = name;
        }

        public override int Eval(Dictionary<string, int> env) 
        {
            return env[name];
        }

        public override string toString() 
        {
            return name;
        }

        public override Expr Simplify() 
        {
            return this;
        }
    }

    public abstract class Binop : Expr 
    {
        public Expr e1, e2;
    }

    public class Add : Binop 
    {
        public Add(Expr e1, Expr e2) 
        {
            this.e1 = e1;
            this.e2 = e2;
        }

        public override int Eval(Dictionary<string, int> env) 
        {
            return e1.Eval(env) + e2.Eval(env);
        }

        public override string toString() 
        {
            return "(" + e1.toString() + " + " + e2.toString() + ")";
        }

        public override Expr Simplify()
        {
            Expr se1 = e1.Simplify();
            Expr se2 = e2.Simplify();  

            if (se1 is CstI && se1.Eval(new Dictionary<string, int>()) == 0)
            {
                return se2;
            }
            if (se2 is CstI && se2.Eval(new Dictionary<string, int>()) == 0)
            {
                return se1;
            }
            return new Add(se1, se2);
            
        }
    }

    public class Sub : Binop 
    {
        public Sub(Expr e1, Expr e2) 
        {
            this.e1 = e1;
            this.e2 = e2;
        }

        public override int Eval(Dictionary<string, int> env) 
        {
            return e1.Eval(env) - e2.Eval(env);
        }

        public override string toString() 
        {
            return "(" + e1.toString() + " - " + e2.toString() + ")";
        }

        public override Expr Simplify()
        {
            Expr se1 = e1.Simplify();
            Expr se2 = e2.Simplify();  

            if (se2 is CstI && se2.Eval(new Dictionary<string, int>()) == 0)
            {
                return se1;
            }
            if (se1.Eval(new Dictionary<string, int>()) == se2.Eval(new Dictionary<string, int>()))
            {
                return new CstI(0);
            }
            return new Sub(se1, se2);
            
        }
    }

    public class Mul : Binop 
    {
        public Mul(Expr e1, Expr e2) 
        {
            this.e1 = e1;
            this.e2 = e2;
        }

        public override int Eval(Dictionary<string, int> env) 
        {
            return e1.Eval(env) * e2.Eval(env);
        }

        public override string toString() 
        {
            return "(" + e1.toString() + " * " + e2.toString() + ")";
        }

        public override Expr Simplify()
        {
            Expr se1 = e1.Simplify();
            Expr se2 = e2.Simplify();  

            if (se1 is CstI && se1.Eval(new Dictionary<string, int>()) == 0)
            {
                return new CstI(0);
            }
            if (se2 is CstI && se2.Eval(new Dictionary<string, int>()) == 0)
            {
                return new CstI(0);
            }
            if (se1 is CstI && se1.Eval(new Dictionary<string, int>()) == 1)
            {
                return se2;
            }
            if (se2 is CstI && se2.Eval(new Dictionary<string, int>()) == 1)
            {
                return se1;
            }
            return new Mul(se1, se2);
            
        }
    }
} 