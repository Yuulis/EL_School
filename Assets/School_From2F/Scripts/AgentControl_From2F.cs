using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Unity.Barracuda;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class AgentControl_From2F : Agent
{
    // General
    [HideInInspector]
    public int configuration;  // configuration num
    private bool onEpisode;
    private bool inSpawnArea;
    private int closestExit;  // Closest Exit when Agent spawns
    private int reachedExit;  // A Exit Agent reached
    private int closestStair;  // Closest Stair when Agent spawns
    private int reachedStair;  // A Stair Agent reached
    private int nowFloor;  // Which floor is Agent now?

    // Env parameter (for TrainingMode = 2)
    [HideInInspector]
    public float param_SpawnableAreaNum;
    [HideInInspector]
    public float param_StepReward;

    // NNModels
    public NNModel A1_StairSide_Brain;
    public NNModel B1_StairSide_Brain;
    public NNModel C1_StairSide_Brain;
    public NNModel A2_StairSide_Brain;
    public NNModel B2_StairSide_Brain;
    public NNModel C2_StairSide_Brain;

    // Behavior names
    string A1_StairSide_BehaviorName = "1F_A_StairSide";
    string B1_StairSide_BehaviorName = "1F_B_StairSide";
    string C1_StairSide_BehaviorName = "1F_C_StairSide";
    string A2_StairSide_BehaviorName = "2F_A_StairSide";
    string B2_StairSide_BehaviorName = "2F_B_StairSide";
    string C2_StairSide_BehaviorName = "2F_C_StairSide";

    // Agent
    Rigidbody Agent_rb;

    // Floor
    public GameObject Floor;
    Material FloorMaterial;
    Renderer FloorRenderer;

    // Exits
    public GameObject Exit1Pos;
    public GameObject Exit2Pos;
    public GameObject Exit3Pos;

    // Stairs
    public GameObject Stair1A;
    public GameObject Stair1B;
    public GameObject Stair1C;

    // Other Scripts
    Settings settings;
    SpawnAgentScript spawnAgentScript;


    // Move Agent
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
        float disToExit1 = Vector3.Distance(this.transform.localPosition, Exit1Pos.transform.localPosition);
        float disToExit2 = Vector3.Distance(this.transform.localPosition, Exit2Pos.transform.localPosition);
        float disToExit3 = Vector3.Distance(this.transform.localPosition, Exit3Pos.transform.localPosition);

        float m = Mathf.Min(disToExit1, Mathf.Min(disToExit2, disToExit3));
        if (m == disToExit1) closestExit = 1;
        else if (m == disToExit2) closestExit = 2;
        else if (m == disToExit3) closestExit = 3;
    }

    void GetReachedExitNum(string ExitName)
    {
        if (ExitName == "Exit1") reachedExit = 1;
        else if (ExitName == "Exit2") reachedExit = 2;
        else if (ExitName == "Exit3") reachedExit = 3;
    }

    void GetClosestStairNum()
    {
        float disToStair1A = Vector3.Distance(this.transform.localPosition, Stair1A.transform.localPosition);
        float disToStair1B = Vector3.Distance(this.transform.localPosition, Stair1B.transform.localPosition);
        float disToStair1C = Vector3.Distance(this.transform.localPosition, Stair1C.transform.localPosition);

        float m = Mathf.Min(disToStair1A, Mathf.Min(disToStair1B, disToStair1C));
        if (m == disToStair1A) closestStair = 1;
        if (m == disToStair1B) closestStair = 2;
        if (m == disToStair1C) closestStair = 3;
    }

    void GetReachedStairNum(string StairName)
    {
        if (StairName == "StairA_1_2") reachedStair = 1;
        else if (StairName == "StairB_1_3") reachedStair = 2;
        else if (StairName == "StairC_1_2") reachedStair = 3;
    }

    // Agent configuration
    public void ConfigureAgent(int config)
    {
        if (settings.doCurriculum)
        {
            EnvironmentParameters EnvParams = Academy.Instance.EnvironmentParameters;
            this.param_SpawnableAreaNum = EnvParams.GetWithDefault("SpawnableAreaNum", 10f);
            this.param_StepReward = EnvParams.GetWithDefault("StepReward", 0f);
        }

        if (config == 10)
        {
            SetModel(B1_StairSide_BehaviorName, B1_StairSide_Brain);
            nowFloor = 1;
        }
        else if (config == 11)
        {
            SetModel(A1_StairSide_BehaviorName, A1_StairSide_Brain);
            nowFloor = 1;
        }
        else if (config == 12)
        {
            SetModel(C1_StairSide_BehaviorName, C1_StairSide_Brain);
            nowFloor = 1;
        }
        else if (config == 20)
        {
            SetModel(B2_StairSide_BehaviorName, B2_StairSide_Brain);
            nowFloor = 2;
        }
        else if (config == 21)
        {
            SetModel(A2_StairSide_BehaviorName, A2_StairSide_Brain);
            nowFloor = 2;
        }
        else if (config == 22)
        {
            SetModel(C2_StairSide_BehaviorName, C2_StairSide_Brain);
            nowFloor = 2;
        }
    }

    public override void Initialize()
    {
        Agent_rb = GetComponent<Rigidbody>();
        settings = FindObjectOfType<Settings>();
        spawnAgentScript = GetComponent<SpawnAgentScript>();

        FloorRenderer = Floor.GetComponent<Renderer>();
        FloorMaterial = FloorRenderer.material;
    }

    // Size = 28
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(Agent_rb.velocity);
        sensor.AddObservation(Agent_rb.angularVelocity);

        sensor.AddObservation(nowFloor);

        sensor.AddObservation(Exit1Pos.transform.localPosition);
        sensor.AddObservation(Exit2Pos.transform.localPosition);
        sensor.AddObservation(Exit3Pos.transform.localPosition);

        sensor.AddObservation(Stair1A.transform.localPosition);
        sensor.AddObservation(Stair1B.transform.localPosition);
        sensor.AddObservation(Stair1C.transform.localPosition);
    }

    public override void OnEpisodeBegin()
    {
        onEpisode = false;
        inSpawnArea = false;
        closestExit = 0;
        reachedExit = 0;
        closestStair = 0;
        reachedStair = 0;

        if (settings.TrainingMode == 2)
        {
            if (settings.doCurriculum) ConfigureAgent(configuration);
            spawnAgentScript.SpawnAgent(param_SpawnableAreaNum, settings.doCurriculum);
        }

        // Reset Agent's status
        Agent_rb.angularVelocity = Vector3.zero;
        Agent_rb.velocity = Vector3.zero;
        this.transform.rotation = Quaternion.identity;
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        if (onEpisode)
        {
            MoveAgent(actions.DiscreteActions);

            // Penalty step by step
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
                GetReachedExitNum(collision.gameObject.name);
                if (closestExit == reachedExit) AddReward(1.5f);
                else AddReward(0.75f);

                EndEpisode();
                StartCoroutine(
                    GoalScoredSwapGroundMaterial(settings.Success_Floor, 0.5f));
            }

            else if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Wall"))
            {
                AddReward(-1.0f);
                EndEpisode();
                StartCoroutine(
                    GoalScoredSwapGroundMaterial(settings.Failed_Floor, 0.5f));
            }

            else if (collision.gameObject.CompareTag("UpStair"))
            {
                Agent_rb.angularVelocity = Vector3.zero;
                Agent_rb.velocity = Vector3.zero;
                this.transform.rotation = Quaternion.identity;

                if (collision.gameObject.name == "StairA_1_1")
                {
                    this.transform.Translate(-3.0f, 5.0f, -1.0f);
                    this.transform.Rotate(0f, 180f, 0f);
                }
                else if (collision.gameObject.name == "StairB_1_1")
                {
                    this.transform.Translate(0.0f, 5.0f, 12.5f);
                    this.transform.Rotate(0f, 0f, 0f);
                }
                else if (collision.gameObject.name == "StairC_1_1")
                {
                    this.transform.Translate(3.0f, 5.0f, -1.0f);
                    this.transform.Rotate(0f, 180f, 0f);
                }

                nowFloor++;
                AddReward(-0.625f);
            }

            else if (collision.gameObject.CompareTag("DownStair"))
            {
                Agent_rb.angularVelocity = Vector3.zero;
                Agent_rb.velocity = Vector3.zero;
                this.transform.rotation = Quaternion.identity;

                if (collision.gameObject.name == "StairA_1_2")
                {
                    this.transform.Translate(3.0f, -5.0f, -1.0f);
                    this.transform.Rotate(0f, 180f, 0f);
                }
                else if (collision.gameObject.name == "StairB_1_3")
                {
                    this.transform.Translate(0.0f, -5.0f, -12.5f);
                    this.transform.Rotate(0f, 180f, 0f);
                }
                else if (collision.gameObject.name == "StairC_1_2")
                {
                    this.transform.Translate(-3.0f, -5.0f, -1.0f);
                    this.transform.Rotate(0f, 180f, 0f);
                }

                nowFloor--;
                GetReachedStairNum(collision.gameObject.name);
                if (closestStair == reachedStair) AddReward(0.75f);
                else AddReward(0.375f);
            }
        }

        else
        {
            if (collision.gameObject.CompareTag("Floor"))
            {
                if (inSpawnArea) onEpisode = true;
                else spawnAgentScript.SpawnAgent(param_SpawnableAreaNum, settings.doCurriculum);
            }

            else if (collision.gameObject.CompareTag("UpStair") || collision.gameObject.CompareTag("DownStair") || collision.gameObject.CompareTag("Obstacle"))
            {
                spawnAgentScript.SpawnAgent(param_SpawnableAreaNum, settings.doCurriculum);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpawnArea"))
        {
            inSpawnArea = true;
            GetClosestExitNum();
            GetClosestStairNum();
        }
    }
}