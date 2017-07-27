using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIocContainer.Tests
{
    public interface A
    {
        int print();
    }
    public class B : A
    {
        private C c;
        public B(C c)
        {
            this.c = c;
        }

        public int print()
        {
          return c.print();
        }
    }

    public class C
    {
        private D d;
        public C(D d)
        {
            this.d = d;
        }
        public int print()
        {
           return d.print();
        }

    }

    public class D
    {
        private E e;
        public D(E e)
        {
            this.e = e;
        }
        public int print()
        {
            return e.print();
        }
    }
    
    public class E
    {
        public E()
        {

        }

        public int print()
        {
            return 1;
        }
    }
}
