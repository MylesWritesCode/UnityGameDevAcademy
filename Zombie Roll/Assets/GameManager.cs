using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int selectedZombiePosition = 0;
    public GameObject selectedZombie;
    public List<GameObject> zombies;
    public Vector3 selectedSize;
    public Vector3 defaultSize;

    // Use this for initialization
    void Start ()
    {
        SelectZombie(selectedZombie);
    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown("left"))
        {
            GetZombieLeft();
        }

        if (Input.GetKeyDown("right"))
        {
            GetZombieRight();
        }

        if (Input.GetKeyDown("up"))
        {
            ZombiePush();
        }
    }

    void GetZombieLeft()
    {
        if (selectedZombiePosition == 0)
        {
            selectedZombiePosition = 3;
            SelectZombie(zombies[3]);
        }
        else
        {
            selectedZombiePosition = selectedZombiePosition - 1;
            GameObject newZombie = zombies[selectedZombiePosition];
            SelectZombie(newZombie);

        }
    }

    void GetZombieRight()
    {
        if (selectedZombiePosition == 3)
        {
            selectedZombiePosition = 0;
            SelectZombie(zombies[0]);
        }
        else
        {
            selectedZombiePosition = selectedZombiePosition + 1;
            SelectZombie(zombies[selectedZombiePosition]);
        }
    }

    void ZombiePush()
    {
        Rigidbody rb = selectedZombie.GetComponent<Rigidbody>();
        rb.AddForce(0, 0, 10, ForceMode.Impulse);
    }

    void SelectZombie(GameObject newZombie)
    {
        selectedZombie.transform.localScale = defaultSize;
        selectedZombie = newZombie;
        selectedZombie.transform.localScale = selectedSize;
    }
}
