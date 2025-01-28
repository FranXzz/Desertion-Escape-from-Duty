using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    // Metodo para abrir LinkedIn
    public void OpenLinkedIn()
    {
        Application.OpenURL("https://www.linkedin.com/in/francorubenmilazzo/");
    }

    // Metodo para abrir GitHub
    public void OpenGitHub()
    {
        Application.OpenURL("https://github.com/FranXzz");
    }
}
