using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotParent : MonoBehaviour
{
    public int Slot;
   
    public void SlotInactive()
    {
        if (Slot == 1) WaveManager_.instance.Slot1 = false;

        if (Slot == 2) WaveManager_.instance.Slot2 = false;

        if (Slot == 3) WaveManager_.instance.Slot3 = false;

        if (Slot == 4) WaveManager_.instance.Slot4= false;
    }
}
