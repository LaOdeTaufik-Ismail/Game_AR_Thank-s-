using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSelection : MonoBehaviour
{
    public GameObject[] tank;
    public int selectedTank = 0;

    public void NextTank()
    {
        Debug.Log(selectedTank);
        tank[selectedTank].SetActive(false);
        selectedTank = (selectedTank + 1) % tank.Length;
        tank[selectedTank].SetActive(true);
    }
    public void PreviusTank()
    {
        tank[selectedTank].SetActive(false);
        selectedTank--;
        if(selectedTank < 0)
        {
            selectedTank += tank.Length;

        }
        tank[selectedTank].SetActive(true);
    }
   
    public void TankSelected()
    {
        PlayerPrefs.SetInt("selectedTank", selectedTank);

    }
}
