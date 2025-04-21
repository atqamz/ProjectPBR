namespace Script.Pesan
{
    public static class OrderGenerator
    {
        public static Order GenerateOrder(int minToppings, int maxToppings)
        {
            int numberOfToppings = UnityEngine.Random.Range(minToppings, maxToppings + 1);
            
            Order retval = new Order();
            
            BananaType randomBananaType = (BananaType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(BananaType)).Length);
            retval.Type = randomBananaType;

            for (int i = 0; i < numberOfToppings; i++)
            {
                ToppingType randomTopping = (ToppingType)UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(ToppingType)).Length);
                retval.ToppingsList.Add(randomTopping);
            }

            return retval;
        }
    }
}