using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTouchControl : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform _leftHandTarget;
    [SerializeField] private Transform _rightHandTarget;
    [SerializeField] private Transform _LeftFootTarget;
    [SerializeField] private Transform _rightFootTarget;
    [SerializeField] private Transform _bodyTarget;
    [SerializeField] private Transform _headTarget;
    [SerializeField] private GameObject _canvas;

    [Header("Image")]
    [SerializeField] private Transform img_leftHand;
    [SerializeField] private Transform img_rightHand;
    [SerializeField] private Transform img_leftFoot;
    [SerializeField] private Transform img_rightFoot;
    [SerializeField] private Transform img_body;
    [SerializeField] private Transform img_head;

    [SerializeField] private Animator animator;
    [SerializeField] private bool _isCanvas;

    #region  프로퍼티
    public Transform leftHand
    {
        get { return _leftHandTarget; }
        set { _leftHandTarget = value; }
    }
    public Transform rightHand
    {
        get { return _rightHandTarget; }
        set { _rightHandTarget = value; }
    }
    public Transform leftFoot
    {
        get { return _LeftFootTarget; }
        set { _LeftFootTarget = value; }
    }
    public Transform rightFoot
    {
        get { return _rightFootTarget; }
        set { _rightFootTarget = value; }
    }
    public Transform body
    {
        get { return _bodyTarget; }
        set { _bodyTarget = value; }
    }
    public Transform head
    {
        get { return _headTarget; }
        set { _headTarget = value; }
    }

    #endregion
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (_isCanvas)
            {
                _canvas.SetActive(true);
                _isCanvas = false;
            }
            else
            {
                _canvas.SetActive(false);
                _isCanvas = true;
            }
        }
    }

    #region Update 각각의 Position
    public void UpdateIKTargetPosition(Transform target, Vector3 position)
    {
        target.position = position;
    }

    public void UpdateLeftHandPosition(Vector3 position)
    {
        UpdateIKTargetPosition(_leftHandTarget, position);
    }

    public void UpdateRightHandPosition(Vector3 position)
    {
        UpdateIKTargetPosition(_rightHandTarget, position);
    }

    public void UpdateLeftFootPosition(Vector3 position)
    {
        UpdateIKTargetPosition(_LeftFootTarget, position);
    }

    public void UpdateRightFootPosition(Vector3 position)
    {
        UpdateIKTargetPosition(_rightFootTarget, position);
    }

    public void UpdateBodytPosition(Vector3 position)
    {
        UpdateIKTargetPosition(_bodyTarget, position);
    }

    public void UpdateHeadPosition(Vector3 position)
    {
        UpdateIKTargetPosition(_headTarget, position);
    }

    #endregion
    private void OnAnimatorIK()
    {
        if (animator != null)
        {
            // 왼손 IK 적용
            if (_leftHandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1f);

                animator.SetIKPosition(AvatarIKGoal.LeftHand, img_leftHand.localPosition);
                animator.SetIKRotation(AvatarIKGoal.LeftHand, img_leftHand.rotation);

            }

            // 오른손 IK 적용
            if (_leftHandTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f);

                animator.SetIKPosition(AvatarIKGoal.RightHand, img_rightHand.localPosition);
                animator.SetIKRotation(AvatarIKGoal.RightHand, img_rightHand.rotation);
            }

            // 왼발 IK
            if (_LeftFootTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);

                animator.SetIKPosition(AvatarIKGoal.LeftFoot, img_leftFoot.localPosition);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, img_leftFoot.rotation);
            }
            // 오른발 IK
            if (_rightFootTarget != null)
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
                animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

                animator.SetIKPosition(AvatarIKGoal.RightFoot, img_rightFoot.localPosition);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, img_rightFoot.rotation);
            }
            // 몸 IK
            if (_bodyTarget != null)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(img_body.localPosition);
            }
            // 머리 IK
            if (_headTarget != null)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(img_head.localPosition);
            }
        }
    }
}

