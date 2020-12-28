using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextManager : MonoBehaviour
{
    public TutorialTextData[] tutorials;
    int currentTutorial;
    float tutorialTimer;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < tutorials.Length; i++)
        {
            tutorials[i].Setup();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorials.Length > currentTutorial)
        {
            tutorialTimer += Time.deltaTime;
            if (tutorialTimer > tutorials[currentTutorial].timeVisible)
            {
                tutorials[currentTutorial].EnableText(false);
                tutorialTimer = 0;
                currentTutorial++;
            }
        }
    }
}
