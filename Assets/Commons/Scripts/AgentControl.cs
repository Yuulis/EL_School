using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentControl : Agent
{
    // General
    private bool onEpisode;
    private bool inSpawnArea;

    // Agent
    Rigidbody Agent_rb;

    // Floor
    public GameObject Floor;
    Material FloorMaterial;
    Renderer FloorRenderer;

    // Exits
    public GameObject Exit1;
    public GameObject Exit2;
    public GameObject Exit3;

    // Other Scripts
    Settings settings;


    // Agent Spawn
    public void SpawnAgent()
    {
        // OnlyExit
        if (settings.TrainingMode == 1)
        {
            this.transform.localPosition = new Vector3(UnityEngine.Random.Range(1.5f, 96.5f), 3.5f, UnityEngine.Random.Range(-30.5f, -2.5f));
        }

        // Only1F_Easy
        else if (settings.TrainingMode == 2)
        {
            this.transform.localPosition = new Vector3(UnityEngine.Random.Range(1.5f, 96.5f), 3.5f, UnityEngine.Random.Range(-30.5f, -2.5f));
        }
        
        // Reset Agent's status
        Agent_rb.angularVelocity = Vector3.zero;
        Agent_rb.velocity = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
    }

    // Moving Agent
    public void MoveAgent(ActionSegment<int> act)
    {
        var dirToGo = Vector3.zero;
        var rotateDir = Vector3.zero;
        var dirToGoForwardAction = act[0];
        var rotateDirAction = act[1];

        if (dirToGoForwardAction == 1) dirToGo = 1.0f * this.transform.forward;  // Forward
        else if (dirToGoForwardAction == 2) dirToGo = -1.0f * this.transform.forward;  // Back

        if (rotateDirAction == 1) rotateDir = 1.0f * this.transform.up;  // Rotate right
        else if (rotateDirAction == 2) rotateDir = -1.0f * this.transform.up;  // Rotate left

        this.transform.Rotate(rotateDir, Time.deltaTime * 200f);
        Agent_rb.AddForce(dirToGo * settings.AgentSpeed, ForceMode.VelocityChange);
    }

    // Floor's color changes when Episode ends
    IEnumerator GoalScoredSwapGroundMaterial(Material mat, float time)
    {
        FloorRenderer.material = mat;
        yield return new WaitForSeconds(time);
        FloorRenderer.material = FloorMaterial;
    }

    public override void Initialize()
    {
        Agent_rb = GetComponent<Rigidbody>();
        settings = FindObjectOfType<Settings>();

        FloorRenderer = Floor.GetComponent<Renderer>();
        FloorMaterial = FloorRenderer.material;
    }

    // Size = 15
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(Agent_rb.velocity);
        sensor.AddObservation(Exit1.transform.localPosition);
        sensor.AddObservation(Exit2.transform.localPosition);
        sensor.AddObservation(Exit3.transform.localPosition);
    }

    public override void OnEpisodeBegin()
    {
        onEpisode = false;
        inSpawnArea = false;
        SpawnAgent();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (onEpisode)
        {
            MoveAgent(actions.DiscreteActions);

            if (this.transform.localPosition.y < 0f)
            {
                AddReward(-0.7f);
                EndEpisode();
                StartCoroutine(
                    GoalScoredSwapGroundMaterial(settings.Failed_Floor, 0.5f));
            }

            AddReward(-1.0f / MaxStep);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;

        if (Input.GetKey(KeyCode.W)) discreteActionsOut[0] = 1;  // Forward
        if (Input.GetKey(KeyCode.S)) discreteActionsOut[0] = 2;  // Back
        if (Input.GetKey(KeyCode.D)) discreteActionsOut[1] = 1;  // Rotate right
        if (Input.GetKey(KeyCode.A)) discreteActionsOut[1] = 2;  // Rotate left
    }

    private void OnCollisionEnter(Collision collision)
    {
        // During Episode?
        if (onEpisode)
        {
            if (collision.gameObject.CompareTag("Exit"))
            {
                if (settings.TrainingMode == 1 || settings.TrainingMode == 2)
                {
                    AddReward(1.0f);
                }

                EndEpisode();
                StartCoroutine(
                    GoalScoredSwapGroundMaterial(settings.Success_Floor, 0.5f));
            }

            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                AddReward(-0.7f);
                EndEpisode();
                StartCoroutine(
                    GoalScoredSwapGroundMaterial(settings.Failed_Floor, 0.5f));
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                if (inSpawnArea) onEpisode = true;

                else SpawnAgent();
            }

            else if (
                collision.gameObject.CompareTag("UpStair") ||
                collision.gameObject.CompareTag("DownStair") ||
                collision.gameObject.CompareTag("Obstacle"))
            {
                SpawnAgent();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnArea"))
        {
            inSpawnArea = true;
        }
    }
}