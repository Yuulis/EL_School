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
    private int ClosestExit;  // Closest Exit when Agent spawns
    private int ReachedExit;  // Exit Agent reached

    // Env parameter
    float param_SpawnableAreaNum;
    float param_StepReward;

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

        // Only1F_Hard
        else if (settings.TrainingMode == 3)
        {
            if (param_SpawnableAreaNum == 0f)
            {
                // B stair side
                this.transform.localPosition = new Vector3(UnityEngine.Random.Range(44.0f, 53.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -3.0f));
            }

            else if (param_SpawnableAreaNum == 1f)
            {
                int rand = Random.Range(0, 1 + 1);
                // A stair side
                if (rand == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                else if (rand == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
            }

            else if (param_SpawnableAreaNum == 2f)
            {
                int rand = Random.Range(0, 2 + 1);

                // C stair side
                if (rand == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                else if (rand == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 96.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                else if (rand == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(87.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-28.0f, -10.0f));
            }

            else if (param_SpawnableAreaNum == 3f)
            {
                int rand = Random.Range(0, 5 + 1);
                // All
                if (rand == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(44.0f, 53.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -3.0f));
                else if (rand == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                else if (rand == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                else if (rand == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                else if (rand == 4) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 96.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                else if (rand == 5) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(87.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-28.0f, -10.0f));
            }
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

    void GetClosestExitNum()
    {
        float disToExit1 = Vector3.Distance(this.transform.localPosition, Exit1.transform.localPosition);
        float disToExit2 = Vector3.Distance(this.transform.localPosition, Exit2.transform.localPosition);
        float disToExit3 = Vector3.Distance(this.transform.localPosition, Exit3.transform.localPosition);

        float m = Mathf.Min(disToExit1, Mathf.Min(disToExit2, disToExit3));
        if (m == disToExit1) ClosestExit = 1;
        else if (m == disToExit2) ClosestExit = 2;
        else if (m == disToExit3) ClosestExit = 3;
    }

    void GetReachedExitNum(string ExitName)
    {
        if (ExitName == "Exit1") ReachedExit = 1;
        else if (ExitName == "Exit2") ReachedExit = 2;
        else if (ExitName == "Exit3") ReachedExit = 3;
    }

    void AddRewardByDistance()
    {
        float distanceToClosestExit = 0f;
        if (ClosestExit == 1) distanceToClosestExit = Vector3.Distance(this.transform.localPosition, Exit1.transform.localPosition);
        if (ClosestExit == 2) distanceToClosestExit = Vector3.Distance(this.transform.localPosition, Exit2.transform.localPosition);
        if (ClosestExit == 3) distanceToClosestExit = Vector3.Distance(this.transform.localPosition, Exit3.transform.localPosition);

        if (distanceToClosestExit < 30f)
        {
            AddReward(0.0001f);
        }
        else if (distanceToClosestExit < 20f)
        {
            AddReward(0.0002f);
        }
        else if (distanceToClosestExit < 10f)
        {
            AddReward(0.0005f);
        }
    }

    // Get environment parameters
    public void SetEnvParams()
    {
        EnvironmentParameters EnvParams = Academy.Instance.EnvironmentParameters;

        this.param_SpawnableAreaNum = EnvParams.GetWithDefault("SpawnableAreaNum", 3f);
        this.param_StepReward = EnvParams.GetWithDefault("StepReward", 0f);
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
        if (settings.TrainingMode == 3) SetEnvParams();
        onEpisode = false;
        inSpawnArea = false;
        ClosestExit = 0;
        ReachedExit = 0;
        SpawnAgent();
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (onEpisode)
        {
            if (settings.TrainingMode == 3) AddRewardByDistance();
            MoveAgent(actions.DiscreteActions);

            if (this.transform.localPosition.y < 0f)
            {
                AddReward(-0.7f);
                EndEpisode();
                StartCoroutine(
                    GoalScoredSwapGroundMaterial(settings.Failed_Floor, 0.5f));
            }

            AddReward(this.param_StepReward);
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
                if (settings.TrainingMode == 3) GetReachedExitNum(collision.gameObject.name);

                if (settings.TrainingMode == 1 || settings.TrainingMode == 2)
                {
                    AddReward(1.0f);
                }
                else if (settings.TrainingMode == 3)
                {
                    if (ClosestExit == ReachedExit) AddReward(1.0f);
                    else AddReward(0.5f);
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
            if (settings.TrainingMode == 3) GetClosestExitNum();
        }
    }
}