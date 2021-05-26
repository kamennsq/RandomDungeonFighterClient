using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardListener : MonoBehaviour
{
    [SerializeField]
    private InputField[] fields;

    private int currentActiveField;
    // Start is called before the first frame update
    void Start()
    {
        currentActiveField = 0;
        fields[currentActiveField].ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            switch (currentActiveField)
            {
                case 0: currentActiveField = 1; fields[currentActiveField].ActivateInputField(); break;
                case 1: currentActiveField = 0; fields[currentActiveField].ActivateInputField(); break;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AccountController.sendLoginMessage();
        }
    }
}
