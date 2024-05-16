using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//skript, um haken "spawnen" zu lassen, um ihn danach gespannt werfen zu können... evtl kann man den haken selbst zum spawner machen und ihn dann werfen, anstatt ihn zu spawnen
public class Spawner : MonoBehaviour
{
   public GameObject prefab;
   public Vector3 position;
   public void Spawn()
   {
       Instantiate(prefab, position, Quaternion.identity);
   }
}
