using UnityEngine;

namespace Utils
{
    public class FollowCamera : MonoBehaviour
    {
        public enum States
        {
            Stopped,
            Holding,
            Following
        }

        [field: SerializeField] public Transform Target { get; set; }
        [SerializeField] private float lerpSpeed;
        [SerializeField] private Vector3 offset;

        private Camera Cam { get; set; }
        private States State { get; set; } = States.Following;

        private void Start()
        {
            Cam = Camera.main;
        }

        private void LateUpdate()
        {
            switch (State)
            {
                case States.Stopped:
                    Target = null;
                    break;
                case States.Holding:
                    // Holdin은 Target은 존재하지만 카메라를 이동하지 않는 상태
                    break;
                case States.Following:
                    Vector3 lerpPos  = Vector3.Lerp(transform.position, Target.position + offset, lerpSpeed * Time.deltaTime);
                    lerpPos.z = transform.position.z;
                    transform.position = lerpPos;
                    break;
            }
        }

        public void ChangeState(States state)
        {
            if (state == States.Following && Target == null)
            {
                Debug.LogWarning("FollowCamera :: TargetMissing");
                return;
            }

            State = state;
        }
    }

}
