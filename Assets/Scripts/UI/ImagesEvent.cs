using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImagesEvent : MonoBehaviour
{
    public void DestroyImage()
    {
        Destroy(gameObject);
    }

    public void ReloadSceen()
    {
        SceneManager.LoadScene(0);
    }
}
