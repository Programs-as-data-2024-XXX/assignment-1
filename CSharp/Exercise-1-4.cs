using System;
using System.Collections;

namespace Exercise14
{
    public abstract class Expr 
    {
        public abstract int Eval();
        public abstract string toString();    
    }

    public class CstI : Expr 
    {
        public int value;

        public CstI(int value) 
        {
            this.value = value;
        }

        public override int Eval() 
        {
            return value;
        }

        public override string toString() 
        {
            return value.ToString();
        }
    }

    public class Var : Expr 
    {
        public string name;

        public Var(string name) 
        {
            this.name = name;
        }

        public override int Eval() 
        {
            return 1;
        }

        public override string toString() 
        {
            return name;
        }
    }

    public abstract class Binop : Expr 
    {
        public string op;
        public Expr e1, e2;
    }

    public class Add : Binop 
    {
        public Add(Expr e1, Expr e2) 
        {
            this.op = "+";
            this.e1 = e1;
            this.e2 = e2;
        }

        public override int Eval() 
        {
            return e1.Eval() + e2.Eval();
        }

        public override string toString() 
        {
            return e1.toString() + " " + op + " " + e2.toString();
        }
    }

    public class Sub : Binop 
    {
        public Sub(Expr e1, Expr e2) 
        {
            this.op = "-";
            this.e1 = e1;
            this.e2 = e2;
        }

        public override int Eval() 
        {
            return e1.Eval() - e2.Eval();
        }

        public override string toString() 
        {
            return e1.toString() + " " + op + " " + e2.toString();
        }
    }

    public class Mul : Binop 
    {
        public Mul(Expr e1, Expr e2) 
        {
            this.op = "*";
            this.e1 = e1;
            this.e2 = e2;
        }

        public override int Eval() 
        {
            return e1.Eval() * e2.Eval();
        }

        public override string toString() 
        {
            return e1.toString() + " " + op + " " + e2.toString();
        }
    }
} 