using System.Collections.Generic;
using UnityEngine;

namespace Script.Masak
{
    public class CookingWok : MonoBehaviour
    {
        public List<Transform> foodPlaceHolders;
        public bool[] isFoodPlaceHoldersFull;
        
        private void Awake()
        {
            isFoodPlaceHoldersFull = new bool[foodPlaceHolders.Count];
        }
        
        public void AddFoodToWok(Transform food)
        {
            var isWokFull = true;

            for (var i = 0; i < foodPlaceHolders.Count; i++)
            {
                if (isFoodPlaceHoldersFull[i]) continue;
                
                food.SetParent(foodPlaceHolders[i]);
                food.localPosition = Vector3.zero; 
                isFoodPlaceHoldersFull[i] = true;
                isWokFull = false; 
                break;
            }
            if (!isWokFull) return;
            Debug.Log("The wok is full. Food is destroyed.");
            Destroy(food.gameObject); 
        }

        public void RemoveFoodFromWok(Transform food)
        {
            var index = foodPlaceHolders.IndexOf(food.transform.parent);
            if (index != -1)
            {
                isFoodPlaceHoldersFull[index] = false;
                
                food.SetParent(null);
                
                food.localPosition = Vector3.zero; 
            }
            else
            {
                Debug.LogWarning("Food not found in the wok.");
            }
        }

    }
}
