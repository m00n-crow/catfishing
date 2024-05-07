using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
   public GameObject prefab;
   public Vector3 position;
   public void Spawn()
   {
       Instantiate(prefab, position, Quaternion.identity);
   }
}
