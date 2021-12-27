from matplotlib.colors import Colormap
import matplotlib.pyplot as plt
import numpy as np
from numpy.core.defchararray import title
import pandas as pd

fig = plt.figure(figsize=(10, 5), tight_layout=True)
plt.subplots_adjust(wspace=0.2, hspace=0.1)

FILE_NAME = "Only1F_result_2021-12-26-16-19-30.csv"

# Input data
ax_Success = fig.add_subplot(
    211, title="Success?", xlabel="X pos", ylabel="Z pos")
Success_Data = pd.read_csv(FILE_NAME, usecols=[0, 1, 2], dtype="float")

ax_ReachedExit = fig.add_subplot(
    212, title="Reached Exit", xlabel="X pos", ylabel="Z pos")
ReachedExit_Data = pd.read_csv(FILE_NAME, usecols=[0, 1, 3], dtype="float")

# DataFrame
df_success = pd.DataFrame(Success_Data, columns=["X", "Z", "Success?"])
df_ReachedExit = pd.DataFrame(ReachedExit_Data, columns=["X", "Z", "ReachedExit"])

# Success?
colorList_1 = {-1: "gray", 0: "red", 1: "green"}
labelName_1 = {-1: "Error", 0: "Failure", 1: "Success"}

for s in set(df_success["Success?"]):
    df2_success = df_success[df_success["Success?"] == s]
    c = colorList_1[s]
    l = labelName_1[s]
    ax_Success.scatter(df2_success.X, df2_success.Z, s=10, color=c, label=l)

ax_Success.legend()

# ReachedExit
colorList_2 = {0: "gray", 1: "red", 2: "blue", 3: "green"}
labelName_2 = {0: "Failure", 1: "1", 2: "2", 3: "3"}

for r in set(df_ReachedExit["ReachedExit"]):
    df2_ReachStair1 = df_ReachedExit[df_ReachedExit["ReachedExit"] == r]
    c = colorList_2[r]
    l = labelName_2[r]
    ax_ReachedExit.scatter(
        df2_ReachStair1.X, df2_ReachStair1.Z, s=10, color=c, label=l)

ax_ReachedExit.legend()


plt.subplots_adjust(left=0.030, right=0.995, top=0.965, bottom=0.040)
plt.show()

IMAGE_NAME = FILE_NAME[0:len(FILE_NAME) - 4] + ".png"
fig.savefig(IMAGE_NAME, facecolor="skyblue")
