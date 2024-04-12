using Cinemachine;
using UnityEngine;

public class CinemachineTarget : MonoBehaviour {

    private CinemachineTargetGroup cinemachineTargetGroup;

    private void Awake() {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
    }
    // Start is called before the first frame update
    void Start() {
        CinemachineTargetGroup.Target cinemachineGroupTargetPlayer = new CinemachineTargetGroup.Target() {
            weight = 1f,
            radius = 1f,
            target = GameManager.Instance.GetPlayer().transform
        };
        CinemachineTargetGroup.Target[] cinemachineTargetGroupArray = new CinemachineTargetGroup.Target[]{
            cinemachineGroupTargetPlayer
        };


        cinemachineTargetGroup.m_Targets = cinemachineTargetGroupArray;
    }

}
