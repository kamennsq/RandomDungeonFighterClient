using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoutButton : MonoBehaviour
{
    public void logout()
    {
        AccountController.logOut();
    }
}
