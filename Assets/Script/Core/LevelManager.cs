using Script.Pesan;
using UnityEngine;

namespace Script.Core
{
    public class LevelManager : MonoBehaviour
    {
        int _currentLevel;
        int _minToppings;
        int _maxToppings;
        
        public Order CurrentOrder;
        
        public void GenerateOrder()
        {
            CurrentOrder = OrderGenerator.GenerateOrder(_minToppings, _maxToppings);
            Debug.Log(CurrentOrder);
        }
    }
}