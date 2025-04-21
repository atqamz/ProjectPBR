using System.Collections.Generic;
using UnityEngine;

namespace Script.Masak
{
    public class FinishFoodTray : MonoBehaviour
    {
        [SerializeField] private List<Transform> foodPlaceHolders;
    
        public void AddFoodToFinishTray(Transform food)
        {
            var randomIndex = Random.Range(0, foodPlaceHolders.Count);
            var selectedPlaceholder = foodPlaceHolders[randomIndex];
        
            food.SetParent(selectedPlaceholder);
            food.localPosition = Vector3.zero; 
        }
    
    }
}
