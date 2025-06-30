using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using ROS2;

namespace ROS2
{
    public class StateControl : MonoBehaviour
    {
        private ROS2UnityComponent ros2Unity;
        private ROS2Node ros2Node;
        private IClient<lifecycle_msgs.srv.ChangeState_Request, lifecycle_msgs.srv.ChangeState_Response> StateClient;
        private IClient<lifecycle_msgs.srv.GetAvailableStates_Request, lifecycle_msgs.srv.GetAvailableStates_Response> GetStateClient;
        private Task<lifecycle_msgs.srv.ChangeState_Response> asyncTask;
        private Task<lifecycle_msgs.srv.GetAvailableStates_Response> asyncTaskGet;
        // Start is called before the first frame update
        void Start()
        {
            ros2Unity = GetComponent<ROS2UnityComponent>();
            if (ros2Unity.Ok())
            {
                if (ros2Node == null)
                {
                    ros2Node = ros2Unity.CreateNode("status_controler");
                    StateClient = ros2Node.CreateClient<lifecycle_msgs.srv.ChangeState_Request, lifecycle_msgs.srv.ChangeState_Response>(
                        "joy_vel_converter/change_state"
                    );
                    GetStateClient = ros2Node.CreateClient<lifecycle_msgs.srv.GetAvailableStates_Request, lifecycle_msgs.srv.GetAvailableStates_Response>(
                        "joy_vel_converter/get_available_states"
                    );
                }
            }

        }

        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator PeriodicAsyncCall(int state)
        {
            if (ros2Unity.Ok())
            {
                while (!StateClient.IsServiceAvailable())
                {
                    yield return new WaitForSecondsRealtime(1);
                }

                lifecycle_msgs.srv.ChangeState_Request request = new lifecycle_msgs.srv.ChangeState_Request();
                request.Transition.Id = 1;

                asyncTask = StateClient.CallAsync(request);

                yield return new WaitForSecondsRealtime(1);
            }
        }

        private bool CheckState()
        {
            return true;
        }

        public void OnClickConfigure()
        {
            StartCoroutine(PeriodicAsyncCall(1));
        }

        public void OnClickActivate()
        {
            StartCoroutine(PeriodicAsyncCall(3));
        }

        public void OnClickDeactivate()
        {
            StartCoroutine(PeriodicAsyncCall(3));
        }

        public void OnClickCleanup()
        {
            StartCoroutine(PeriodicAsyncCall(3));
        }

        public void OnClickShutdown()
        {
            StartCoroutine(PeriodicAsyncCall(5));
        }
    }
}