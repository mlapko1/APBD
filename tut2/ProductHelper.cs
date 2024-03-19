using System;

namespace tut2
{
    public class ProductHelper
    {
        public static double GetRequiredTemperature(RefrigeratedProductType productType)
        {
            switch (productType)
            {
                case RefrigeratedProductType.Bananas:
                    return 13.0;
                case RefrigeratedProductType.Milk:
                    return 4.0;
                case RefrigeratedProductType.Meat:
                    return -2.0;
                case RefrigeratedProductType.Vegetables:
                    return 4.0;
                case RefrigeratedProductType.Fish:
                    return -1.0;
                default:
                    throw new ArgumentOutOfRangeException(nameof(productType), $"No temperature defined for {productType}.");
            }
        }
    }
}