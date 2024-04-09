using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlaceKeys : MonoBehaviour
{
    [SerializeField] GameObject[] keys;
    [SerializeField] GameObject[] waypoints;

    private int point;
    List<int> chosenWaypoints;

    private void OnEnable()  //choses a random location for the keys and moves them to that position
    {
        chosenWaypoints = new List<int>();

        foreach (GameObject key in keys)
        {
            key.gameObject.SetActive(true);
            point = Random.Range(0, waypoints.Length);
            while (chosenWaypoints.Contains(point))
            {
                point = Random.Range(0, waypoints.Length);
            }
            key.gameObject.transform.position = waypoints[point].transform.position;
            chosenWaypoints.Add(point);
        }
    }

}
