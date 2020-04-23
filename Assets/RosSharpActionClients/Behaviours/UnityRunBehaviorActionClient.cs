using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityRunBehaviorActionClient : MonoBehaviour
    {
        private RosConnector rosConnector;
        public RunBehaviorActionClient runBehaviorActionClient;
        // Start is called before the first frame update

        public string actionName = "run_behavior";
        protected static string choregraphe_dir = ".lastUploadedChoregrapheBehavior/";
        public string behavior = choregraphe_dir + "Hello_behavior";
        public string status = "";
        public string feedback = "";
        public string result = "";

        void Start()
        {
            rosConnector = GetComponent<RosConnector>();
            runBehaviorActionClient = new RunBehaviorActionClient(actionName, rosConnector.RosSocket);
            runBehaviorActionClient.Initialize();
        }

        // Update is called once per frame
        void Update()
        {
            status = runBehaviorActionClient.GetStatusString();
            feedback = runBehaviorActionClient.GetFeedbackString();
            result = runBehaviorActionClient.GetResultString();
        }

        public void RegisterGoal()
        {
            runBehaviorActionClient.behavior = behavior;
        }

        public void ExecuteBehavior(string new_behavior_name)
        {
            runBehaviorActionClient.behavior = choregraphe_dir + new_behavior_name;
            runBehaviorActionClient.SendGoal();
        }

    }
}