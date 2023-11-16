using System;
using System.Collections;

namespace DesignPattern.Patterns
{
	public class Strategy
	{
		public Strategy()
		{
            /*
			 * All objects have the same function, but the objects's function difference
			 * 
			 * 
			 * 
			 * Advantages & disadvantages

				Advantage
					- Algorithms can be interchanged flexibly
					- Separate the algorithm part from the part that uses the algorithm
					- Inheritance can be replaced by algorithm encapsulation
					- Increase open-closed properties: When changing the algorithm or adding a new algorithm, there is no need to change the context code
				Defect
					- Should not be applied if there are only a few treatments and changes are infrequent.
					- Clients must recognize the differences between strategies.

			  *When to use it
					- Want to use different variants of a handle in an object and be able to switch between handlers at runtime.
					- When there are multiple equivalent classes that differ only in the way they implement some behavior.
					- When you want to separate the business logic of a class from the implementation details of the processes.
					- When the class has a large conditional operator that converts between variants of the same handler.
			 */
        }
    }

    #region NOT USE PATTERN
    public class StrategyWithoutPattern
    {
		double GetPromotionPrice(double originPrice, string typePromotion)
		{
			if (typePromotion == "blackFriday")
			{
				//simulator 100000 line code
				//...
				return (originPrice * 0.8);
			}

            if (typePromotion == "preOrd")
                return (originPrice * 0.9);

            if (typePromotion == "default")
                return originPrice;

            //......

            return 0;
		}
	}
	#endregion

	#region USING PATTERN
	public interface IPromotionPrice
	{
		double GetPromotionPrice(double originPrice);
	}

	public class PromotionPriceContext
	{
		IPromotionPrice _promotionPrice;
		public PromotionPriceContext(IPromotionPrice promotionPrice)
		{
			_promotionPrice = promotionPrice;
        }

		public void SetStrategy(IPromotionPrice promotionPrice)
		{
			_promotionPrice = promotionPrice;
		}

		public double GetPrice(double originPrice)
		{
			return _promotionPrice.GetPromotionPrice(originPrice);
        }
    }

    public class GetBlackFridayPromotionPrice : IPromotionPrice
    {
        //sale-off 80%
        public double GetPromotionPrice(double originPrice) => originPrice * 0.8;
    }

    public class GetPreOrderPromotionPrice : IPromotionPrice
    {
        //sale-off 90%
        public double GetPromotionPrice(double originPrice) => originPrice * 0.9;
    }

    public class StrategyWithPattern
	{
		double _originPrice = 0;
        public StrategyWithPattern(double defaultPrice)
        {
			_originPrice = defaultPrice;

			//main
			GetPrice();
        }

		double GetPrice()
		{
            var price = 0d;

            PromotionPriceContext ctx = new PromotionPriceContext(new GetBlackFridayPromotionPrice());
            price = ctx.GetPrice(_originPrice); // BlackFridayPrice

            //change promotion from BlackFriday to preOrd
            ctx.SetStrategy(new GetPreOrderPromotionPrice());
            price = ctx.GetPrice(_originPrice); // preOrdPrice

			return price;
        }
    }
    #endregion
}

