using System;
using System.Linq;

namespace SampleProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            foreach (var ps in Enum.GetValues(typeof(PizzaSize)).Cast<PizzaSize>())
            {
                Console.WriteLine($"{ps.ToString()} : Radius {ps.GetRadius()}, Thickness {ps.GetThickness()}, Crust Style {ps.GetCrustStyle()}");
            }
        }
    }
}
