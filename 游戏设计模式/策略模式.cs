using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ConsoleApplication1
{

    /*
     * 策略模式
     * 定义一系列的算法,把每一个算法封装起来,并且使它们可相互替换,隐藏了具体算法/行为实现细节
     * 本模式使得算法可独立于使用它的客户变化
     */
    public interface TravelStrategy
    {
        void TravelMethod();
    }

    public class TrainStrategy : TravelStrategy
    {
        public void TravelMethod()
        {
            Console.WriteLine("=========坐火车===========");
        }
    }

    public class AirplaneStrategy : TravelStrategy
    {
        public void TravelMethod()
        {
            Console.WriteLine("=========乘飞机===========");
        }
    }

    public class CarStrategy : TravelStrategy
    {
        public void TravelMethod()
        {
            Console.WriteLine("=========汽车===========");
        }
    }

    public enum TravelType
    {
        Train,
        Airplane,
        Car,
    }


    /// <summary>
    /// 环境类
    /// </summary>
    public class TravelContext
    {
        private TravelStrategy travel;

        public TravelContext(TravelType type)
        {
            //这种用到了switch其实不好，后期扩展了，这里就需要维护了
            switch (type)
            {
                case TravelType.Train:
                    travel = new TrainStrategy();
                    break;

                case TravelType.Airplane:
                    travel = new AirplaneStrategy();
                    break;

                case TravelType.Car:
                    travel = new CarStrategy();
                    break;
            }
        }


        public void TravelMethod()
        {
            travel.TravelMethod();
        }
    }





    /// <summary>
    /// 
    /// </summary>
    class Program
    {
       
        static void Main(string[] args)
        {
            //从这里可以看出，只要一个类，完全隐藏了具体算法的细节，也不知道其他类的存在了
            TravelContext context = new TravelContext(TravelType.Airplane);
            context.TravelMethod();


            Console.ReadKey();

        }
    }
}
