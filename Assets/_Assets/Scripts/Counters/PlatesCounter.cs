using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlatesSpawned;
    public event EventHandler OnPlatesRemoved;


    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int plateSpawnAmount;
    private int plateSpawnAmountMax = 4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0f;
           

            if (GameManager.Instance.IsGamePlaying() &&  plateSpawnAmount < plateSpawnAmountMax)
            {
                plateSpawnAmount++;

                OnPlatesSpawned?.Invoke(this, EventArgs.Empty);

            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            //player is empty handed
            if (plateSpawnAmount > 0)
            {
                //There is atleast one plate
                plateSpawnAmount--;

                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                OnPlatesRemoved?.Invoke(this, EventArgs.Empty);

            }
        }
    }
}
