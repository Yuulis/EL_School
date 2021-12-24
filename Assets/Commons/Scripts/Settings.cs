using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    // Training mode 
    // 1 : Only1F
    public int TrainingMode;

    // When Agent could run away, it will use this material for a few seconds.
    public Material Success_Floor;

    // When Agent couldn't run away, it will use this material for a few seconds.
    public Material Failed_Floor;

    // Agent speed
    public float AgentSpeed = 0.6f;

    // Draw trail?
    public bool DrawTrail = false;
}
