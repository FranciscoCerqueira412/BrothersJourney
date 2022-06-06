using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayerRed : MonoBehaviour
{
    public GameObject[] playerPrefab;


    public void Start()
    {
        Vector2 positionGreen = new Vector2(-173.87f, 4.35f);
        Vector2 positionRed = new Vector2(-173.03f, 4.35f);
        PhotonNetwork.Instantiate(playerPrefab[0].name, positionGreen, Quaternion.identity);
        PhotonNetwork.Instantiate(playerPrefab[1].name, positionRed, Quaternion.identity);
        

    }


}
