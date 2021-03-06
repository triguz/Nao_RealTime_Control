/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */

using Newtonsoft.Json;

namespace RosSharp.RosBridgeClient.MessageTypes.NaoqiBridge
{
    public class SetSpeechVocabularyGoal : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "naoqi_bridge_msgs/SetSpeechVocabularyGoal";

        //  Goal: The new vocabulary to be set in the speech recognition module
        //  Result: True if the vocabulary was set
        //  Feedback: None
        public string[] words;

        public SetSpeechVocabularyGoal()
        {
            this.words = new string[0];
        }

        public SetSpeechVocabularyGoal(string[] words)
        {
            this.words = words;
        }
    }
}
