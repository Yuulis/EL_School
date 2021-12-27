# EL_School
**Goal**: To learn how to escape from school as fast as possible.  
Using [ML-Agents](https://github.com/Unity-Technologies/ml-agents) in [Release 17](https://github.com/Unity-Technologies/ml-agents/tree/release_17).  

## Environments
1. **School_Only1F** (Curriculum Learning)

---
## Env-1. School_Only1F

### Image
![Stage](images/Stage_Only1F.png)

### Environment
* **Exit1 ~ 3** : When Agent touched them, one episode will success and be ended. 
* **Obstacles** : When Agent touched them, one episode will fail and be ended.

### Agent
* It spawns random place in this stage.  
[!] There is spawnable area, made by collider, over the floor. When Agent land on the floor without touching it, Agent spawns again.
* It uses different brains depending on where it spawns. 
* It can move forward and back and turn around right and left direction (Discrete Action) .
* It can observe around with ray sensor. This ray is fired at 360 degrees.

### How to train
* Set three different brains to Agent where it spawns.

\<Curriculum Training>  
There two curriculum parameters : ``SpawnableAreaNum`` and ``StepReward``.  
cf. ``\config\AgentManagerCurriculum.yaml``  
Curriculum settings is below: 

| SpawnableAreaNum | StepReward | Using Behavior | threshold |  
|:----------------:|:----------:|:--------------:|:---------:|
| 0.0 (B_StairSide)| -0.0002    | EL_B_StairSide | 0.6       |
| 1.0 (A_StairSide)| -0.0002    | EL_A_StairSide | 0.5       |
| 2.0 (C_StairSide)| -0.0002    | EL_C_StairSide | 0.5       |
| 3.0 (All)        | -0.00025   | One of three   | -         |

Training starts from ``SpawnableAreaNum = 0``.

Max step of each Behavior is below :  

| Behavior Name | Max Step |
|:-------------:|:--------:|
|EL_B_StairSide | 1000000  |
|EL_A_StairSide | 10000000 |
|EL_C_StairSide | 10000000 |

### Rewards
* Agent gets ``StepReward`` set by Curriculum training at every step.
* When Agent touches Obstacles, it gets ``-1.0``.
* If Agent reaches Exit which is closest from where it spawned, it gets ``1.5``. Else, it gets ``0.75``.

### Result
Here is the result video. *The video is slow, this is due to the specs of my PC :(  
![result video](https://user-images.githubusercontent.com/79734873/147444470-8b665edb-289f-4361-b69f-fd716cac849f.mp4)