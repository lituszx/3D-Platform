using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int currentCoins;
    public Text textCoins;
    public void AddCoin(int _coinToAdd)
    {
        currentCoins += _coinToAdd;
        textCoins.text = "" + currentCoins;
    }
    private void Update()
    {
        if(currentCoins == 100)
        {
            currentCoins = 0;
            transform.GetComponent<HPManager>().currentLife++;
        }
    }
}
