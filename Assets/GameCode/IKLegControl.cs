using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKLegControl : MonoBehaviour
{
    [SerializeField] private bool _isIK = true;                                 // IK bool 값
    [SerializeField] protected Animator _animator;                               // 애니메이터
    [SerializeField] private Transform _leftFoot = null;                         // 왼쪽 발
    [SerializeField] private Transform _rightFoot = null;                        // 오른쪽 발
    [SerializeField] private Transform _plane = null;                            // 지면
    //[SerializeField] private LayerMask groundLayerMask = default;


    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        OnAnimatorIK();
    }

    // IK 컨트롤 함수
    void OnAnimatorIK()
    {
        if (_isIK)
        {
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

            // 왼쪽 발 위치 설정
            Vector3 leftFootPosition = GetFootPosition(_leftFoot);
            _animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPosition);
            _animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(transform.up, _plane.up) * transform.rotation);

            // 오른쪽 발 위치 설정
            Vector3 rightFootPosition = GetFootPosition(_rightFoot);
            _animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPosition);
            _animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(transform.up, _plane.up) * transform.rotation);
        }
        else
        {
            // false면 IK 끔
            _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
            _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
            _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
            _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
        }
    }
    private Vector3 GetFootPosition(Transform foot)
    {
        RaycastHit hit;
        // 발의 포지션 과 지면의 거리 계산 레이. Mathf.Infinity : 길이 제한 없이 비교하려고
        if (Physics.Raycast(foot.position + Vector3.up, -_plane.up, out hit, Mathf.Infinity))
        {
            return hit.point;
        }
        return foot.position;
    }


    #region 저장용 함수 이 함수는 해당 오브젝트와 충돌하면 IK가 움직임  
    void ForSave()
    {
        // animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        // animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        // animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
        // animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);

        // // 왼쪽 발 위치 설정
        // animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFoot.position);
        // RaycastHit leftHit;
        // if (Physics.Raycast(leftFoot.position + Vector3.up, Vector3.down, out leftHit, 1f, groundLayerMask))
        // {
        //     animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftHit.point);
        //     animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(transform.up, leftHit.normal) * transform.rotation);
        // }

        // // 오른쪽 발 위치 설정
        // animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFoot.position);
        // RaycastHit rightHit;
        // if (Physics.Raycast(rightFoot.position + Vector3.up, Vector3.down, out rightHit, 1f, groundLayerMask))
        // {
        //     animator.SetIKPosition(AvatarIKGoal.RightFoot, rightHit.point);
        //     animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.FromToRotation(transform.up, rightHit.normal) * transform.rotation);
        // }
    }
    #endregion
}
