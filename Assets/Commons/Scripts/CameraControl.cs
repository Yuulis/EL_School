using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject AgentCamera;
    public GameObject MapCamera;
    public GameObject SceneCamera;

    Settings settings;

    void Start()
    {
        settings = FindObjectOfType<Settings>();
    }

    void Update()
    {
        if (settings.DrawTrail) {
            AgentCamera.SetActive(true);
            MapCamera.SetActive(true);
            SceneCamera.SetActive(false);
        }
        else {
            AgentCamera.SetActive(false);
            MapCamera.SetActive(false);
            SceneCamera.SetActive(true);
        }
    }
}
