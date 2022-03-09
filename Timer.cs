using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    float currentTime = 0f;
    [SerializeField] float startingTime = 10f;
    public GameManager gameManager;
    public float overdelay = 1f;

    [SerializeField] Text countdown;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= Time.deltaTime;
        countdown.text = currentTime.ToString("0");

        if(currentTime <= 0)
        {
            currentTime = 0;
            Invoke("gameoverscreen", overdelay);
            FindObjectOfType<DialogueNPC>().OutOfRange();
            FindObjectOfType<DialogueSystem>().OutOfRange();
            FindObjectOfType<PlayerController>().stop();
            FindObjectOfType<fracture>().outofrange();
            //FindObjectOfType<fracture>().stopliatin();
        }
    }
    public void gameoverscreen()
    {
        gameManager.Setup();
        FindObjectOfType<PlayerController>().unlock();
    }
}
