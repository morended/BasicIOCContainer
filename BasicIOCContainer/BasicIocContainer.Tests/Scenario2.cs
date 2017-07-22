using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIocContainer.Tests
{
    public interface U
    {
        int print();
    }
    public class V : U
    {
        private W w;
        private X x;
        private Y y;
        private static int count = 0;
        public int sum = 0;
        public V(W w, X x, Y y)
        {
            count++;
            this.w = w;
            this.x = x;
            this.y = y;
            count++;
        }

        public int print()
        {
          return w.print(sum+count);
        }
    }

    public class W
    {
        private Y y;
        private static int count = 0;
        public W(Y y)
        {
            count++;
            this.y = y;
        }
        public int print(int sum)
        {
            
           return y.print(sum + count);
        }

    }

    public class X
    {
        private W w;
        private static int count = 0;
        public X(W w)
        {
            count++;
            this.w = w;
        }
        public int print(int sum)
        {
            return w.print(sum + count);
        }
    }
    
    public class Y
    {
        private static int count = 0;
        public Y()
        {
            count++;
        }

        public int print(int sum)
        {
            return sum + count;
        }
    }
}
