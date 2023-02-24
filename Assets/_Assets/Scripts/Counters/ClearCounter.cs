using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{

    [SerializeField] private KitchenObjectSO kitchenObjectSO;
   
    
    public override void Interact(Player player)
    {
      if (!HasKitchenObject())
        {
            //There is no kitchen object
            if (player.HasKitchenObject())
            {
                //Player has kitchen Object
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player not carrying anything
            }
        }
      else
        {
            //there is no kitchen object
            if (player.HasKitchenObject())
            {
                //Player is carrying something
            }
            else
            {
                //Player is carrying nothing
                GetKitchenObject().SetKitchenObjectParent(player);

            }
        }
    }

    
}
