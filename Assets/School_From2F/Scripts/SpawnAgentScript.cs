using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAgentScript : MonoBehaviour
{
    int config;

    void Start()
    {
        config = GetComponent<AgentControl_From2F>().configuration;
    }

    // Agent Spawn
    public void SpawnAgent(float SpawnableAreaNum, bool doCurriculum)
    {
        // Curriculum training?
        if (doCurriculum)
        {
            // 1F
            // B stair side
            if (SpawnableAreaNum == 10f)
            {
                this.transform.localPosition = new Vector3(UnityEngine.Random.Range(44.0f, 53.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -3.0f));
                config = 10;
            }

            // A stair side
            else if (SpawnableAreaNum == 11f)
            {
                int rand = Random.Range(0, 2 + 1);
                if (rand == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                else if (rand == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                else if (rand == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(10.0f, 11.0f), 3.5f, UnityEngine.Random.Range(-27.0f, -25.0f));

                config = 11;
            }

            // C stair side
            else if (SpawnableAreaNum == 12f)
            {
                int rand = Random.Range(0, 3 + 1);
                if (rand == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                else if (rand == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 96.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                else if (rand == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(87.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-28.0f, -10.0f));
                else if (rand == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(80.0f, 84.0f), 3.5f, UnityEngine.Random.Range(-27.0f, -25.0f));

                config = 12;
            }

            // 2F
            // B stair side
            if (SpawnableAreaNum == 20f)
            {
                int rand = Random.Range(0, 1 + 1);
                if (rand == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(44.0f, 48.0f), 8.5f, UnityEngine.Random.Range(-29.0f, -2.0f));
                if (rand == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(48.0f, 51.5f), 8.5f, UnityEngine.Random.Range(-29.0f, -9.0f));

                config = 20;
            }

            // A stair side
            else if (SpawnableAreaNum == 21f)
            {
                int rand = Random.Range(0, 3 + 1);
                if (rand == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 43.5f), 8.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                else if (rand == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 43.5f), 8.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                else if (rand == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(10.0f, 14.0f), 8.5f, UnityEngine.Random.Range(-25.0f, -27.0f));
                else if (rand == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(6.0f, 7.0f), 8.5f, UnityEngine.Random.Range(-27.0f, -10.0f));

                config = 21;
            }

            // C stair side
            else if (SpawnableAreaNum == 22f)
            {
                int rand = Random.Range(0, 3 + 1);
                if (rand == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(51.5f, 96.0f), 8.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                else if (rand == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(51.5f, 96.0f), 8.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                else if (rand == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(87.0f, 92.0f), 8.5f, UnityEngine.Random.Range(-28.0f, -10.0f));
                else if (rand == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(80.0f, 84.0f), 8.5f, UnityEngine.Random.Range(-25.0f, -27.0f));

                config = 22;
            }

            // All (1F and 2F)
            else if (SpawnableAreaNum == 30f)
            {
                int rand_a = Random.Range(0, 1 + 1);

                // 1F
                if (rand_a == 0)
                {
                    int rand_c = Random.Range(0, 2 + 1);

                    // B stair side
                    if (rand_c == 0)
                    {
                        this.transform.localPosition = new Vector3(UnityEngine.Random.Range(44.0f, 53.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -3.0f));
                        config = 10;
                    }

                    // A stair side
                    else if (rand_c == 1)
                    {
                        int rand_b = Random.Range(0, 2 + 1);
                        if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                        else if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                        else if (rand_b == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(10.0f, 11.0f), 3.5f, UnityEngine.Random.Range(-27.0f, -25.0f));

                        config = 11;
                    }

                    // C stair side
                    else if (rand_c == 2)
                    {
                        int rand_b = Random.Range(0, 3 + 1);
                        if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 96.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                        else if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                        else if (rand_b == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(87.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-28.0f, -10.0f));
                        else if (rand_b == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(80.0f, 84.0f), 3.5f, UnityEngine.Random.Range(-27.0f, -25.0f));

                        config = 12;
                    }
                }

                // 2F
                else if (rand_a == 1)
                {
                    int rand_c = Random.Range(0, 2 + 1);

                    // B stair side
                    if (rand_c == 0)
                    {
                        int rand_b = Random.Range(0, 1 + 1);
                        if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(44.0f, 48.0f), 8.5f, UnityEngine.Random.Range(-29.0f, -2.0f));
                        if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(48.0f, 51.5f), 8.5f, UnityEngine.Random.Range(-29.0f, -9.0f));

                        config = 20;
                    }

                    // A stair side
                    else if (rand_c == 1)
                    {
                        int rand_b = Random.Range(0, 3 + 1);
                        if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 43.5f), 8.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                        else if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 43.5f), 8.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                        else if (rand_b == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(10.0f, 14.0f), 8.5f, UnityEngine.Random.Range(-25.0f, -27.0f));
                        else if (rand_b == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(6.0f, 7.0f), 8.5f, UnityEngine.Random.Range(-27.0f, -10.0f));

                        config = 21;
                    }

                    // C stair side
                    else if (rand_c == 22)
                    {
                        int rand_b = Random.Range(0, 3 + 1);
                        if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(51.5f, 96.0f), 8.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                        else if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(51.5f, 96.0f), 8.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                        else if (rand_b == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(87.0f, 92.0f), 8.5f, UnityEngine.Random.Range(-28.0f, -10.0f));
                        else if (rand_b == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(80.0f, 84.0f), 8.5f, UnityEngine.Random.Range(-25.0f, -27.0f));

                        config = 22;
                    }
                }
            }
        }

        else
        {
            // All (1F and 2F)
            int rand_a = Random.Range(0, 1 + 1);

            // 1F
            if (rand_a == 0)
            {
                int rand_c = Random.Range(0, 2 + 1);

                // B stair side
                if (rand_c == 0)
                {
                    this.transform.localPosition = new Vector3(UnityEngine.Random.Range(44.0f, 53.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -3.0f));
                    config = 10;
                }

                // A stair side
                else if (rand_c == 1)
                {
                    int rand_b = Random.Range(0, 2 + 1);
                    if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                    else if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 44.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                    else if (rand_b == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(10.0f, 11.0f), 3.5f, UnityEngine.Random.Range(-27.0f, -25.0f));

                    config = 11;
                }

                // C stair side
                else if (rand_c == 2)
                {
                    int rand_b = Random.Range(0, 3 + 1);
                    if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 96.0f), 3.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                    else if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(53.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                    else if (rand_b == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(87.0f, 92.0f), 3.5f, UnityEngine.Random.Range(-28.0f, -10.0f));
                    else if (rand_b == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(80.0f, 84.0f), 3.5f, UnityEngine.Random.Range(-27.0f, -25.0f));

                    config = 12;
                }
            }

            // 2F
            else if (rand_a == 1)
            {
                int rand_c = Random.Range(0, 2 + 1);

                // B stair side
                if (rand_c == 0)
                {
                    int rand_b = Random.Range(0, 1 + 1);
                    if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(44.0f, 48.0f), 8.5f, UnityEngine.Random.Range(-29.0f, -2.0f));
                    if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(48.0f, 51.5f), 8.5f, UnityEngine.Random.Range(-29.0f, -9.0f));

                    config = 20;
                }

                // A stair side
                else if (rand_c == 1)
                {
                    int rand_b = Random.Range(0, 3 + 1);
                    if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 43.5f), 8.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                    else if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(2.0f, 43.5f), 8.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                    else if (rand_b == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(10.0f, 14.0f), 8.5f, UnityEngine.Random.Range(-25.0f, -27.0f));
                    else if (rand_b == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(6.0f, 7.0f), 8.5f, UnityEngine.Random.Range(-27.0f, -10.0f));

                    config = 21;
                }

                // C stair side
                else if (rand_c == 22)
                {
                    int rand_b = Random.Range(0, 3 + 1);
                    if (rand_b == 0) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(51.5f, 96.0f), 8.5f, UnityEngine.Random.Range(-10.0f, -9.0f));
                    else if (rand_b == 1) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(51.5f, 96.0f), 8.5f, UnityEngine.Random.Range(-29.0f, -28.0f));
                    else if (rand_b == 2) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(87.0f, 92.0f), 8.5f, UnityEngine.Random.Range(-28.0f, -10.0f));
                    else if (rand_b == 3) this.transform.localPosition = new Vector3(UnityEngine.Random.Range(80.0f, 84.0f), 8.5f, UnityEngine.Random.Range(-25.0f, -27.0f));

                    config = 22;
                }
            }
        }
    }
}
