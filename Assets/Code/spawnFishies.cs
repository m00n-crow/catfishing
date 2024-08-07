using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFishies : MonoBehaviour
{
    private float time = 2f;

    public float maxFish = 5f;
    public BoxCollider2D spawnArea;
    public List<GameObject> allTheFishies;

    public static int amountOfFish = 0;
    public bool canArchiSpawn = true;
    public static bool archiCaught = false;

    GameObject randomizedFish, newFish;

    private void Start()
    {
        amountOfFish = 0;
        spawnArea = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        time = GameObject.Find("TimeManager").GetComponent<TimeManager>().hour;

        // if the amount of fish in that list is less than the max number of fish: spawn a fish
        if (amountOfFish < maxFish)
        {
            spawnAFish();
            Debug.Log("a new catfish has arrived");
        }

        //Goodbye Archi
        if (time > 0)
        {
            canArchiSpawn = true;
            GameObject Archi;
            Archi = GameObject.Find("Archie(Clone)");
            if (Archi != null) Destroy(Archi);
        }
    }
    void instantiate()
    {
        // Get the bounds of the Box Collider
        Bounds bounds = spawnArea.bounds;

        // Generate a random position within the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        Vector2 spawnPosition = new Vector2(x, y);

        // Instantiate the object at the random position
        GameObject instantiatedFish = Instantiate(newFish, spawnPosition, Quaternion.identity);

        // add 1 to the number of catfishies
        amountOfFish++;
    }
    void spawnAFish()
    {
        randomizedFish = allTheFishies[Random.Range(0, allTheFishies.Count)];

        if (time == 0)
        {
            if (randomizedFish.GetComponent<fishScript>().time0)
            { 
                newFish = randomizedFish;
                if (newFish.name == "Archie" && canArchiSpawn && !archiCaught)
                {
                    instantiate();
                    canArchiSpawn = false;
                }
                else if (newFish.name == "Archie" && !canArchiSpawn) spawnAFish();
                else instantiate();
            }
        else spawnAFish();
        }
        else if (time == 1)
        {
            if (randomizedFish.GetComponent<fishScript>().time1)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
            
        }
        else if (time == 2)
        {
            if (randomizedFish.GetComponent<fishScript>().time2)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 3)
        {
            if (randomizedFish.GetComponent<fishScript>().time3)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 4)
        {
            if (randomizedFish.GetComponent<fishScript>().time4)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 5)
        {
            if (randomizedFish.GetComponent<fishScript>().time5)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 6)
        {
            if (randomizedFish.GetComponent<fishScript>().time6)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 7)
        {
            if (randomizedFish.GetComponent<fishScript>().time7)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 8)
        {
            if (randomizedFish.GetComponent<fishScript>().time8)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 9)
        {
            if (randomizedFish.GetComponent<fishScript>().time9)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 10)
        {
            if (randomizedFish.GetComponent<fishScript>().time10)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 11)
        {
            if (randomizedFish.GetComponent<fishScript>().time11)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 12)
        {
            if (randomizedFish.GetComponent<fishScript>().time12)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 13)
        {
            if (randomizedFish.GetComponent<fishScript>().time13)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 14)
        {
            if (randomizedFish.GetComponent<fishScript>().time14)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 15)
        {
            if (randomizedFish.GetComponent<fishScript>().time15)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 16)
        {
            if (randomizedFish.GetComponent<fishScript>().time16)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 17)
        {
            if (randomizedFish.GetComponent<fishScript>().time17)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 18)
        {
            if (randomizedFish.GetComponent<fishScript>().time18)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 19)
        {
            if (randomizedFish.GetComponent<fishScript>().time19)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 20)
        {
            if (randomizedFish.GetComponent<fishScript>().time20)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 21)
        {
            if (randomizedFish.GetComponent<fishScript>().time21)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 22)
        {
            if (randomizedFish.GetComponent<fishScript>().time22)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        else if (time == 23)
        {
            if (randomizedFish.GetComponent<fishScript>().time23)
            {
                newFish = randomizedFish;
                instantiate();
            }
            else spawnAFish();
        }
        
    }
// Public method to get the spawn bounds
    public Bounds GetSpawnBounds()
    {

        return spawnArea.bounds;
    }   
}
