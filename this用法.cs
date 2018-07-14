
//this用法

//用法一：this代表当前类的实例对象
namespace Demo
{
    public class Test
    {
        private string scope = "全局变量";
        public string getResult()
        {
            string scope = "局部变量";
　　　　　　　// this代表Test的实例对象
　　　　　　　// 所以this.scope对应的是全局变量
　　　　　　  // scope对应的是getResult方法内的局部变量
            return this.scope + "-" + scope;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Test test = new Test();
                Console.WriteLine(test.getResult());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadLine();
            }

        }
    }
}