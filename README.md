# Nao Real Time Control

> Implementation of a basic VR User Interface to work with Nao Robot and ROS.

> The availability of frameworks and applications in the robotic domain fostered in the last years a spread inthe adoption of robots in daily life activities. Many of these activities include the robot teleoperation, i.e.controlling its movements remotely. Virtual Reality (VR) demonstrated its effectiveness in lowering the skill barrier for such a task. In this paper, we discuss the engineering and the implementation of a general-purpose, open-source framework for teleoperation a humanoid robot through a VR headset. It includes a VR interface for articulating different robot actions using the VR controllers, without the need for training. Besides, it exploits the flexibility of the Robot Operating System (ROS) for the control and synchronization of the robot hardware, the distribution of the computation and its scalability. The framework supports the extension for operating both other types of robots and using different VR configurations. We evaluated the proposed architecture for assessing the system’s quality through Software Architecture Analysis Method.

> **TAGS**: Humanoids Robot, ROS Framework, Virtual Reality, Human-Robot Interaction, NAO robot, Unity engine.

> [![youtube video here](https://img.youtube.com/vi/J8I892TnaIA/0.jpg)](https://www.youtube.com/watch?v=J8I892TnaIA)

## Table of Contents

- [Requirements](#generalrequirements)
- [ROS](#rosinstallation)
	- [Ubuntu Configuration](#ubuntuconf)
	- [Indigo Installation](#indigoinst)
	- [Rosdep and Environment Setup](#rosdep)
	- [ROS Workspace](#rosworkspace)
	- [Nao ROS](#naoros)
	- [Nao Real-Time Control](#naortc)
	- [Final Touches](#finalros)
- [Unity](#unity)
	- [Unity Setup](#unitysetup)
- [Launch](#launch)
	- [Virtual Robot](#virtual)
	- [Real Robot](#real)
- [Others](#others)

---

<a name="generalrequirements"></a>
## Requirements

Before beginning the installation process make sure all the general requirements are satisfied:
- Ubuntu 14.04 Trusty 64bit.
- Unity 2018.4.15f1 or newer.
- Oculus Rift CV1 - 2 Joystick - at least 2 Motion sensors.
- NAO Robot v4 or greater.
- NaoQi 2.1.4 SDK
- Ehternet connection is preferred but not required.

During testing phase, the Unity application was running in a Windows 10 pc, with 17-6700k, 32g ram, Nvidia gtx 1070 while ROS was on a laptop with Ubuntu 14.04 Trusty 64bit, i7-3rd gen, 4g ram.

---

<a name="rosinstallation"></a>
## ROS

ROS Indigo can be installed using Debian Packages (recommended) or from source. These packages are more efficient than source-based builds and is the preferred installation method for Ubuntu. Installing from source is not recommended but a tutorial can be found on the ROS website. ROS Indigo ONLY supports Saucy (13.10) and Trusty (14.04) for debian packages. Before continuing it would be wise to have Ubuntu Trusty 14.04 already installed and ready-to-go.

---

<a name="ubuntuconf"></a>
### Ubuntu Configuration 

Configure Ubuntu repositories to allow "restricted," "universe," and "multiverse." Ubuntu offers various guide that explain how to do [this](https://help.ubuntu.com/community/Repositories/Ubuntu). 

Setup Ubuntu to accept software from packages.ros.org:
```shell
$ sudo sh -c 'echo "deb http://packages.ros.org/ros/ubuntu $(lsb_release -sc) main" > /etc/apt/sources.list.d/ros-latest.list'
```
Setup Keys:
```shell
$ sudo apt-key adv --keyserver 'hkp://keyserver.ubuntu.com:80' --recv-key C1CF6E31E6BADE8868B172B4F42ED6FBAB17C654
```
If connecting to the keyserver causes issues, it is possible to substitute `hkp://pgp.mit.edu:80` or `hkp://keyserver.ubuntu.com:80` in the previous command.
Alternatively, the curl of the apt-key command is available, which can be helpful if behind a proxy server:
```shell
$ curl -sSL 'http://keyserver.ubuntu.com/pks/lookup?op=get&search=0xC1CF6E31E6BADE8868B172B4F42ED6FBAB17C654' | sudo apt-key add -
```
---

<a name="indigoinst"></a>
### Indigo Installation

Make sure Debian package index is up-to-date:
```shell
$ sudo apt-get update && sudo apt-get install dpkg
```
Indigo `Desktop-Full-Install` provides standard libraries and tools, like ROS, rqt, rviz, robot-generic libraries, 2D/3D simulators and 2D/3D perception. Indigo uses Gazebo 2 which is the default version of Gazebo on Trusty and is recommended.
```shell
$ sudo apt-get install ros-indigo-desktop-full
```
---

<a name="rosdep"></a>
### Rosdep and Environment Setup

Before using ROS, `rosdep` needs to be initialized. `rosdep` enables to easily install system dependencies for source and is required to run some core components in ROS.
```shell
$ sudo rosdep init
$ rosdep update
```
it is convenient if the ROS environment variables are automatically added to the bash session every time a new shell is launched:
```shell
$ echo "source /opt/ros/indigo/setup.bash" >> ~/.bashrc
$ source ~/.bashrc
```
`rosinstall` is a frequently used command-line tool in ROS that is distributed separately. It enables to easily download many source trees for ROS packages with one command:
```shell
$ sudo apt-get install python-rosinstall
```

---

<a name="rosworkspace"></a>
###  ROS Workspace

During the installation of ROS, you will see that you are prompted to source one of several setup.*sh files will need to be sourced, the best way to do this is to add this 'sourcing' to the shell startup script. This is required because ROS relies on the notion of combining spaces using the shell environment. This makes developing against different versions of ROS or against different sets of packages easier. If finding or using ROS packages causes issues it is important to make sure that the environment and environment paths are properly setup with:
```shell
$ printenv | grep ROS
```
`rosbuild` and `catkin` are the two available methods for organizing and building ROS code. `rosbuild` is not recommended or maintained anymore but kept for legacy. `catkin` is the recommended way to organise code, it uses more standard CMake conventions and provides more flexibility especially for people wanting to integrate external code bases or who want to release their software.
To create a ROS Workspace:
```shell
$ mkdir -p ~/catkin_ws/src
$ cd ~/catkin_ws/
$ catkin_make
```
The `catkin_make` command is a convenience tool for working with catkin workspaces. Running it the first time in the workspace, it will create a `CMakeLists.txt` link in the 'src' folder. Additionally, in the current directory there should now be a 'build' and 'devel' folder. Inside the 'devel' folder there are now several setup.*sh files. Sourcing any of these files will overlay this workspace on top of the environment. To make sure the workspace is properly overlayed by the setup script, make sure `ROS_PACKAGE_PATH` environment variable includes the current directory.
```shell
$ echo $ROS_PACKAGE_PATH/home/user/catkin_ws/src:/opt/ros/kinetic/share
```
The basic functionalities of ROS should now be installed correctly and it is possible to proceed to install NAO ROS Stack and the various NAO Packages.

---

<a name="naoros"></a>
### Nao ROS

To install the Aldebaran NAOqi SDK, it is possible to register and download from the Aldebaran/SoftBank website\footnote{https://community.aldebaran.com/}. 
This section covers installing NAOqi on a local PC, to remotely control the Nao, however to install NaoQi directly on the robot itself, investigating cross-compiling ROS for the Nao is needed. To setup NAOqi SDK and Python bindings: 

Create a new directory and copy the downloaded tars inside:
```shell
$ mkdir ~/naoqi
$ cp ~/Downloads/naoqi-sdk-2.1.4.13-linux64.tar ~/Downloads/pynaoqi-python2.7-2.1.4.13-linux64.tar ~/naoqi
$ cd ~/naoqi
```
Execute the following commands to extract the SDK:
```shell
$ tar xzf naoqi-sdk-2.1.4.13-linux64.tar
$ tar xzf pynaoqi-python2.7-2.1.4.13-linux64.tar
```
Check the installation by executing NAOqi:
```shell
$ ~/naoqi/naoqi-sdk-2.1.4.13-linux64/naoqi
```
The resulting output should be similar to:
```shell
Starting NAOqi version 2.1.4.13
.
NAOqi is listening on 0.0.0.0:9559
.
.
NAOqi is ready...
```
Press `CTRL-C` to exit.
Add the NAOqi library path to \texttt{PYTHONPATH}. e.g.
```shell
$ export PYTHONPATH=~/naoqi/pynaoqi-python2.7-2.1.4.13-linux64:$PYTHONPATH
```
To make this permanently available for every future terminal, edit \texttt{PYTHONPATH} in the \texttt{bashrc} file too. e.g.
```shell
$ echo 'export PYTHONPATH=~/naoqi/pynaoqi-python2.7-2.1.4.13-linux64:$PYTHONPATH' >> ~/.bashrc
```
To verify the correct installation of the Python bindings, open a python shell and try to include the ALProxy from naoqi.
```shell
$ python
-inside python shell-
>>> from naoqi import ALProxy
```
If these commands fails, please double check that the extracted python SDK folder is correctly in the `PYTHONPATH`.
Some extra ROS packages are required in order to display all components of the ROS bridge. So for ROS Indigo please execute the following command to install those packages:
```shell
$ sudo apt-get install ros-indigo-driver-base ros-indigo-move-base-msgs ros-indigo-octomap ros-indigo-octomap-msgs ros-indigo-humanoid-msgs ros-indigo-humanoid-nav-msgs ros-indigo-camera-info-manager ros-indigo-camera-info-manager-py
```
The next command will install the official packages for NAO needed to simply use RViz or other supported ROS tools:
```shell
$ sudo apt-get install ros-indigo-nao-robot
```
The above command will install the following packages: `nao_apps, nao_bringup, nao_description` and their dependencies.

Move inside the 'src' folder in the catkin workspace, if the 'src' folder does not exist, go back and install the ROS Workspace. In the `src` folder, create a `.rosinstall` file and open it.
```shell
$ cd ~/catkin_ws/src
$ touch .rosinstall
$ vim .rosinstall
```
Then copy paste the following text (don’t forget the ‘-’ at the beginning of the lines):
```shell
- git: {local-name: naoqi_bridge, uri: "https://github.com/ros-naoqi/naoqi_bridge.git", version: master}
- git: {local-name: nao_robot, uri: "https://github.com/ros-naoqi/nao_robot.git", version: master}
- git: {local-name: nao_extras, uri: "https://github.com/ros-naoqi/nao_extras.git", version: master}
- setup-file: {local-name: ../devel/setup.bash }
```
Save and close. Then, in order to fetch and update the packages from github run the following wstool command:
```shell
$ wstool update
```
Then, compile and setup the ROS workspace:
```shell
$ cd ..
$ catkin_make
$ source devel/setup.bash
```
It is a good practice to add the following line to `.bashrc` file. It will avoid weird behavior in the future by setting the catkin workspace as the default one. Note that this is a different command from the one showed in the Chapter regarding `rosdep` environment.
```shell
$ echo "source ~/catkin_ws/devel/setup.bash" >> ~/.bashrc
```
By default, the previous section only installs the Python version of the bridge. This requires basically no compiling, but rather a linking of python scripts. There exists also a C++ version, which might be faster to execute.
For this work, the C++ SDK 2.1.4.13 Linux 64 was used.
Execute the following commands for extracting, installing the SDK and checking the installation. A separate cmake file is included for finding the C++ Naoqi SDK, which requires the `AL_DIR` environment variable to point to the installation of the SDK. This can be done inside the `.bashrc` file:
```shell
$ echo "export AL_DIR=~/naoqi-sdk-2.1.4.13-linux64" >> ~/.bashrc
$ source ~/.bashrc
$ cd ~/catkin_ws
$ catkin_make
```
NAO meshes are important to properly display the robot in RVIZ:
```shell
$ sudo apt-get install ros-indigo-nao-meshes
```
Now install RosBridge suite, this package will provide the needed function and tools to connect with Unity:
```shell
$ sudo apt-get install ros-indigo-rosbridge-server
```

---

<a name="naortc"></a>
### Nao Real-Time Control

This packages requires several plugins that need to be compiled from source. The various packages need to be cloned inside the workspace and then compiled:
```shell
$ cd src
$ git clone https://github.com/triguz/nao_real_time_control
$ cd ..
$ catkin_make
```

---

<a name="finalros"></a>
### Final Touches

Before completing ROS Installation process, a couple of small tweaks need to be made to ensure that everything works correctly:
The launch file `nao_full_py.launch` provided by the package `nao_bringup` launches some of the nodes needed to communicate with NaoQi, unfortunately it doesn't launch `nao_behaviors` by default, so it needs to be added manually.
Go to `nao_robot/nao_bringup/launch/`, open `nao_full_py.launch` and add these lines:
```shell
<!-- enable behaviours -->
<include file="$(find nao_apps)/launch/behaviors.launch" >
 <arg name="nao_ip"        value="$(arg nao_ip)" />
</include>
```
Next make sure \texttt{.bashrc} is correctly set, it should look something like this in the end:
```shell
...
source /opt/ros/indigo/setup.bash
source ~/catkin_ws/devel/setup.bash
export PYTHONPATH=~/naoqi/pynaoqi-python2.7-2.1.4.13-linux64:$PYTHONPATH
export LD_LIBRARY_PATH=/home/triguz/catkin_ws/devel/lib:/opt/ros/indigo/lib:/opt/ros/indigo/lib/x86_64-linux-gnu
export AL_DIR=~/naoqi-sdk-2.1.4.13-linux64
```
Check network configuration to verify the connection with Unity. In Ubuntu type `ifconfig`, the IP address (`enp0s8`) of the Ubuntu system will be used by `rosbridge\_suite` and RosBridgeClient. These settings are needed so that the RosBridgeClient running in Windows, and the ROSBridge Server running on Ubuntu can communicate.

---

<a name="unity"></a>
## Unity

Before installing Unity, make sure to install and test all the necessary drivers and software to make Oculus Rift work correctly.

---

<a name="unitysetup"></a>
### Unity Setup

1. Start Unity and follow on screen instructions to sign in/create an account.
2. Create a new project.
3. Copy the Project folder from the latest commit from this repository into the Assets folder of the Unity project just created.

Make sure that Unity is using .NET Framework 4.x, since it is required by RosBridgeClient. To do this:
1. In the Unity menu, go to Edit, then Project Settings, finally Player.
2. In the Inspector pane, look under Other Settings, then Configuration.
3. Set Scripting Runtime Version* to .Net 4.x Equivalent.

Now that the various asset files have been downloaded and setup, open the Unity Scene, select the `RosConnector GameObject` and in the inspector (right panel), change the value of "Ros Bridge Server Url" field with the ROS IP as shown in the previous Chapter.

![Unity RosConnector Setup](https://github.com/triguz/Nao_RealTime_Control/blob/master/imgs/UnityRosConnectorSetup.png "Unity RosConnector Setup")

---

<a name="launch"></a>
## Launch

To ensure everything works fine, ROS nodes needs to be initialized first, then it is possible to launch the Unity Application. Open a new terminal for every command unless stated otherwise.

---

<a name="virtual"></a>
### Virtual Robot

Launch NaoQi on the current machine:
```shell
$ naoqi/naoqi-sdk-2.1.4.13-linux64/naoqi --broker-ip 127.0.0.1
```
Launch NAO drivers, robot state publisher, start action and service servers, and other functionalities:
```shell
$ roslaunch nao_bringup nao_full_py.launch 
```
(**Optional**) The Robot should now be ready and waiting in the \texttt{Stand} position, it can be viewed in Rviz:
```shell
$ rosrun rviz rviz
```
If the robot is missing check Chapter `Others` on how to correctly setup Rviz. If the robot isn't in the correct position, NaoQi has not be launched properly, check the relative terminal for error messages. If everything works correctly Rviz should show something like in the Figure.
![Rviz View](https://github.com/triguz/Nao_RealTime_Control/blob/master/imgs/RvizRobotModel.png "Rviz View")
Launch Nao Real-Time Control nodes:
```shell
$ roslaunch nao_hands_control nao_hands_control.launch 
```
```shell
$ roslaunch nao_head_control nao_head_control.launch 
```
```shell
$ roslaunch nao_arms_control nao_arms_control.launch
```
Launch RosBridge Server WebSocket:
```shell
$ roslaunch rosbridge_server rosbridge_websocket.launch 
```
Everything should be running without errors and it is now possible to launch the Unity Application by clicking "Play" in the Unity Interface. If Unity launches correctly the virtual robot in Unity should acquire the `Stand` pose and the RosBridge Server terminal should notify that a new client has connected to the bridge and should display to which topics it is subscribed to.

---

<a name="real"></a>
### Real Robot

Turn On the Nao Robot and make sure it is connected to the network.
Launch NAO drivers, robot state publisher, start action and service servers, and other functionalities:
```shell
$ roslaunch nao_bringup nao_full_py.launch nao_ip:=<robot_ip> roscore_ip:=<roscore_ip>
```
(**Optional**) The Robot should now be ready and waiting in the \texttt{Stand} position, it can be viewed in Rviz:
```shell
$ rosrun rviz rviz
```
If the robot is missing check Chapter `Others` on how to correctly setup Rviz. If the robot isn't in the correct position, NaoQi has not be launched properly, check the relative terminal for error messages. If everything works correctly Rviz should show something like in the Figure.

![Rviz View](https://github.com/triguz/Nao_RealTime_Control/blob/master/imgs/RvizRobotModel.png "Rviz View")

Before activating the various Nao Real-Time Control nodes go into each launch file and make sure the `nao_ip=<robot_ip>` corresponds to the correct ip of your robot otherwise the nodes will not be able to find NaoQi and will not start.
Launch Nao Real-Time Control nodes:
```shell
$ roslaunch nao_hands_control nao_hands_control.launch 
```
```shell
$ roslaunch nao_head_control nao_head_control.launch 
```
```shell
$ roslaunch nao_arms_control nao_arms_control.launch
```
Launch RosBridge Server WebSocket:
```shell
$ roslaunch rosbridge_server rosbridge_websocket.launch 
```
Being able to visualize the cameras feed from the real robot in Unity requires to convert to a compressed format and republish the raw image published by `naoqi_sensor_py`. One new publisher for each camera will be created:
```shell
$ rosrun image_transport republish raw in:=/nao_robot/camera/bottom/camera/image_raw compressed out:=/nao_robot/camera/bottom/camera/image_repub
```
```shell
$ rosrun image_transport republish raw in:=/nao_robot/camera/top/camera/image_raw compressed out:=/nao_robot/camera/top/camera/image_repub
```
Everything should be running without errors and it is now possible to launch the Unity Application by clicking "Play" in the Unity Interface. If Unity launches correctly the virtual robot in Unity should acquire the `Stand` pose and the RosBridge Server terminal should notify that a new client has connected to the bridge and should display to which topics it is subscribed to.

---

<a name="others"></a>
## Others

Individual ROS Packages can be installed with the command:
```shell
$ sudo apt-get install ros-indigo-PACKAGE
```
To find available packages, use:
```shell
$ apt-cache search ros-indigo
```
Rviz needs to be setup to properly show the robot. In the top-left "Displays" window, change "Fixed Frame" to "base_link" or "base_footprint". If only the "/map" option is available, then the URDF model wasn't loaded from the previous step. The "Target Frame" (right panel) should be "<Fixed Frame>". Global Status should change to "OK". If it is red and "error" then that probably means that the topic /joint_states is not being updated. Click on the "Add" button and add a grid. Click on the "Add" button and add RobotModel.

To view the automatic generated graph of all active ROS nodes and topics in a given moment:
```shell
$ rosrun rqt_graph rqt_graph
```
ROS Graphical User Interface can be helpful to view message type definition, active action client etc., to use ROS gui type:
```shell
$ rosrun rqt_gui rqt_gui
```
To add custom `BodyPoses` to the ones already available add these lines to `pose_manager.launch`:
```shell
<!-- You can define here a path to a Choregraphe posture library (XAP file). -->
    <param name="xap" value="$(find naoqi_pose)/config/filename.xap" />
```

---

