using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IKUIControl : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RawImage _touchUI;
    [SerializeField] private Camera mainCamera = null;
    [SerializeField] private IKTouchControl ikTouchControl;
    [SerializeField]private Dictionary<RawImage, Transform> uiToIKTargetMap = new Dictionary<RawImage, Transform>();
    private bool isDragging = false; // 드래그 중 여부를 나타내는 플래그


    private void Start()
    {
        // UI와 IK 타겟의 매핑 생성
        uiToIKTargetMap.Add(_touchUI, ikTouchControl.leftHand);
        // 다른 UI에 대해서도 매핑 추가
        // uiToIKTargetMap.Add(otherUI, otherIKTarget);
    }
    private void Update()
    {
        if (isDragging)
        {
            // 마우스 좌표를 월드 좌표로 변환
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = -ikTouchControl.leftHand.position.z; // IK 대상과 같은 Z 좌표로 설정

            // IK 타겟 위치 업데이트
            foreach (var entry in uiToIKTargetMap)
            {
                Transform target = entry.Value;
                ikTouchControl.UpdateIKTargetPosition(target, mouseWorldPos);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        _touchUI.transform.position = Input.mousePosition;
        _touchUI.GetComponent<RawImage>().color = Color.green;

        isDragging = true; // 드래그 중임을 표시
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _touchUI.GetComponent<RawImage>().color = Color.white;
        isDragging = false; // 드래그 종료
    }

}
