using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneController : MonoBehaviour
{
    public static BattleSceneController controller;

    private void Awake()
    {
        controller = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitTimeToLoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setInitialParameters()
    {
        print(DungeonController.mob.health);
    }

    IEnumerator waitTimeToLoad()
    {
        yield return new WaitForSeconds(1f);
        setInitialParameters();
    }
}
