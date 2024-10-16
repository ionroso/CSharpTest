using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public static class FooExtension
    {
        public static void TestNull(this Foo? me)
        {
            Console.WriteLine(me.ToString());
        }
    }
    public class Foo
    {
    }

    public class TestExtensionObjectNull
    {
        public void Test()
        {
            Foo foo = null; // new Foo();
            foo.TestNull();
        }
    }
}
