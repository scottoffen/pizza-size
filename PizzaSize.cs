using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SampleProject
{
    public enum PizzaSize
    {
        [PizzaSizeMetadata(Radius = 6, Thickness = 0.5, CrustStyle = "Pan")]
        Small,

        [PizzaSizeMetadata(Radius = 12, Thickness = 1.0, CrustStyle = "Thin")]
        Medium,

        [PizzaSizeMetadata(Radius = 14, Thickness = 1.5, CrustStyle = "Thick")]
        Large,

        [PizzaSizeMetadata(Radius = 18, Thickness = 1.5, CrustStyle = "Deep Dish")]
        Family
    }

    internal class PizzaSizeMetadata : Attribute
    {
        public int Radius { get; set; }

        public double Thickness { get; set; }

        public string CrustStyle { get; set; }
    }

    public static class PizzaSizeExtensions
    {
        private static readonly ConcurrentDictionary<PizzaSize, PizzaSizeMetadata> Metadata = new ConcurrentDictionary<PizzaSize, PizzaSizeMetadata>();
        private static readonly ConcurrentDictionary<PizzaSize, string> CrustStyles = new ConcurrentDictionary<PizzaSize, string>();

        static PizzaSizeExtensions()
        {
            foreach(var ps in Enum.GetValues(typeof(PizzaSize)).Cast<PizzaSize>())
            {
                var metadata = GetMetadata(ps);
                Metadata[ps] = metadata;
                CrustStyles[ps] = metadata.CrustStyle;
            }
        }

        private static PizzaSizeMetadata GetMetadata(PizzaSize ps)
        {
            var info = ps.GetType().GetMember(ps.ToString());
            var attributes = info[0].GetCustomAttributes(typeof(PizzaSizeMetadata), false);
            return attributes.Length > 0 ? attributes[0] as PizzaSizeMetadata : new PizzaSizeMetadata();
        }

        public static int GetRadius(this PizzaSize ps)
        {
            return GetMetadata(ps).Radius;
        }

        public static double GetThickness(this PizzaSize ps)
        {
            return GetMetadata(ps).Thickness;
        }

        public static string GetCrustStyle(this PizzaSize ps)
        {
            return CrustStyles[ps];
        }
    }
}