using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenUrl : MonoBehaviour
{
    //[SerializeField] string ;
    public void OpenUrlMethod(string urlToOpen)
    {
        Application.OpenURL(urlToOpen);
    }
}
