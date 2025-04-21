using System.Collections.Generic;

namespace Script.Pesan
{
    [System.Serializable]
    public class Order
    {
        public BananaType Type;

        public int Amount;

        public List<ToppingType> ToppingsList;


        public Order(BananaType type, int amount, List<ToppingType> toppingsList)
        {
            Type = type;
            Amount = amount;
            ToppingsList = toppingsList;
        }
        
        public Order()
        {
            Type = BananaType.Fresh;
            Amount = 1;
            ToppingsList = new List<ToppingType>();
        }
    }
}