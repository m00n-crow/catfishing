using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFishies : MonoBehaviour
{
    [Range(0,24)]
    public float time = 2f;

    public float maxFish = 5f;

    private List<GameObject> swimmingFishies;
    public List<GameObject> allTheFishies;

    GameObject randomizedFish, newFish;

    private void Start()
    {
        swimmingFishies = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
        time = GameObject.Find("TimeManager").GetComponent<TimeManager>().hour;
        if(swimmingFishies.Count < maxFish)
        {
            spawnAFish();
        }
    }
   
    void spawnAFish()
    {
        randomizedFish = allTheFishies[Random.Range(0, allTheFishies.Count)];

        if (time == 0)
        {
            if (randomizedFish.GetComponent<fishScript>().time0) newFish = randomizedFish;
            else spawnAFish();
        }
        else if (time == 1)
        {
            if (randomizedFish.GetComponent<fishScript>().time1)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 2)
        {
            if (randomizedFish.GetComponent<fishScript>().time2)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 3)
        {
            if (randomizedFish.GetComponent<fishScript>().time3)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 4)
        {
            if (randomizedFish.GetComponent<fishScript>().time4)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 5)
        {
            if (randomizedFish.GetComponent<fishScript>().time5)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 6)
        {
            if (randomizedFish.GetComponent<fishScript>().time6)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 7)
        {
            if (randomizedFish.GetComponent<fishScript>().time7)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 8)
        {
            if (randomizedFish.GetComponent<fishScript>().time8)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 9)
        {
            if (randomizedFish.GetComponent<fishScript>().time9)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 10)
        {
            if (randomizedFish.GetComponent<fishScript>().time10)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 11)
        {
            if (randomizedFish.GetComponent<fishScript>().time11)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 12)
        {
            if (randomizedFish.GetComponent<fishScript>().time12)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 13)
        {
            if (randomizedFish.GetComponent<fishScript>().time13)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 14)
        {
            if (randomizedFish.GetComponent<fishScript>().time14)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 15)
        {
            if (randomizedFish.GetComponent<fishScript>().time15)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 16)
        {
            if (randomizedFish.GetComponent<fishScript>().time16)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 17)
        {
            if (randomizedFish.GetComponent<fishScript>().time17)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 18)
        {
            if (randomizedFish.GetComponent<fishScript>().time18)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 19)
        {
            if (randomizedFish.GetComponent<fishScript>().time19)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 20)
        {
            if (randomizedFish.GetComponent<fishScript>().time20)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 21)
        {
            if (randomizedFish.GetComponent<fishScript>().time21)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 22)
        {
            if (randomizedFish.GetComponent<fishScript>().time22)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        else if (time == 23)
        {
            if (randomizedFish.GetComponent<fishScript>().time23)
            {
                newFish = randomizedFish;
                GameObject instantiatedFish = Instantiate(newFish);
                swimmingFishies.Add(instantiatedFish);
            }
            else spawnAFish();
        }
        
    }

  
}
