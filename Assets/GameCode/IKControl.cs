using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class IKControl : MonoBehaviour
{
    [SerializeField] protected Animator _animator;                               // 애니메이터

    // Component -> Character Position
    [Header("Character Transform")]
    [SerializeField] private Transform _head = null;                             // 몸통
    [SerializeField] private Transform _body = null;                             // 몸통
    [SerializeField] private Transform _leftFoot = null;                         // 왼쪽 발
    [SerializeField] private Transform _rightFoot = null;                        // 오른쪽 발
    [SerializeField] private Transform _LeftHand = null;                         // 왼손
    [SerializeField] private Transform _rightHand = null;                        // 오른손

    // Component -> UI
    [Header("UI Obejct")]
    [SerializeField] private Transform _uiHead = null;
    [SerializeField] private Transform _uiBody = null;
    [SerializeField] private Transform _uiLeftFoot = null;
    [SerializeField] private Transform _uiRightFoot = null;
    [SerializeField] private Transform _uiLeftHand = null;
    [SerializeField] private Transform _uiRightHand = null;


    // Component -> GameObject
    [Header("Control")]
    [SerializeField] private GameObject _Canvas = null;                        // Cavnas

    [SerializeField] private bool _isIK = false;                                 // IK bool 값
    [SerializeField] private bool _isCanvas = false;                                 // Canvas bool 값
    [SerializeField] private Vector3 _uiMoveDirection;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        CanvasControl();
        IKSetting();

                if (_isIK)
        {
            MoveCharacterWithIK();
        }
    }

    // IK 받는 메소드
    void IKSetting()
    {
        if (_isIK)
        {
            SetOnIK();
        }
        else
        {
            // false면 IK 끔
            SetOffIK();
        }
    }

    private void SetOnIK()
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

        // // 왼쪽 발 위치 설정
        // Vector3 leftFootPosition = GetUIPosition(_leftFoot);
        // _animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPosition);
        // _animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.FromToRotation(transform.up, _uiLeftFoot.up) * transform.rotation);
    }

    private void SetOffIK()
    {
        _animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0);
        _animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0);
        _animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0);
        _animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0);
    }
    public void MoveCharacter(Vector3 moveDirection)
    {
        _uiMoveDirection = moveDirection;
    }

    // // UI 포지션 받아오는 메소드
    // private Vector3 GetUIPosition(Transform transform)
    // {
    //     RaycastHit hit;
    //     if (Physics.Raycast(transform.position + Vector3.up, out hit, Mathf.Infinity))
    //     {
    //         return hit.point;
    //     }
    //     return transform.position;
    // }

    private void MoveCharacterWithIK()
    {
        transform.Translate(_uiMoveDirection * Time.deltaTime);
    }

    // 캔버스 입력 받는 메소드
    void CanvasControl()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_isCanvas)
            {
                _Canvas.SetActive(true);
                _isCanvas = false;
            }
            else
            {
                _Canvas.SetActive(false);
                _isCanvas = true;
            }
        }
    }
}
