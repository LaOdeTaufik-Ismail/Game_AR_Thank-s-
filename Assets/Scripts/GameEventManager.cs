using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEventManager : MonoBehaviour
{
    public GameObject[] PlacementObjectPf;
    GameObject tankPlace;
    string tankName;
    private void Start()
    {
        int selectedTank = PlayerPrefs.GetInt("selectedTank");
        tankPlace = PlacementObjectPf[selectedTank];
        tankName = tankPlace.name + "(Clone)";
        Debug.Log(tankName);

    }
    void Update()
    {

        if(GameObject.Find("Turret(Clone)") == null && GameObject.Find("Tank - Enemy AR(Clone)") == null)
        {
            Debug.Log("Win");
            SceneManager.LoadScene("Outro");
        }
        if (GameObject.Find(tankName) == null)
        {
            SceneManager.LoadScene(1);
            Debug.Log("Lose");
        }
    }
}
