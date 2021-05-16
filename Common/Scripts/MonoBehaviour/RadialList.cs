using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform))]
public class RadialList : MonoBehaviour
{
    public enum Direction
    {
        LEFT_TO_RIGHT,
        RIGHT_TO_LEFT
    }

    [Space(4)]
    [Header("Properties")]
    [SerializeField]
    public float radius = 1f;

    public float sensitivity = 1f;

    public bool invert = false;

    public Direction direction = Direction.LEFT_TO_RIGHT;

    [Space(4)]
    [Header("References")]
    public Transform content;

    [SerializeField]
    private RadialListViewport _viewport;

    private float _rotation;
    public float Rotation
    {
        get
        {
            return _rotation;
        }
        set
        {
            _rotation = value;

            //if (direction == Direction.LEFT_TO_RIGHT)
            //    _rotation = Mathf.Clamp(_rotation, 0f, maxAngle);
            //else
            //    _rotation = Mathf.Clamp(_rotation, -maxAngle, 0f);

            UpdateListView();
        }
    }

    private RectTransform _rect;

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    private float angleDelta = 0f;
    private float maxAngle = 0f;

    public void UpdateListView()
    {
        // If destroyed
        if (content == null) return;

        // Calculate delta angle
        angleDelta = 360f / (content.childCount);

        // Update max angle
        maxAngle = Mathf.PI * Mathf.Rad2Deg + angleDelta;

        float angle = Rotation;

        // Offset by half circle
        if (direction == RadialList.Direction.LEFT_TO_RIGHT)
            angle += Mathf.PI * Mathf.Rad2Deg;

        foreach (RectTransform element in content)
        {
            element.localPosition = new Vector2(
            radius * Mathf.Cos(angle * Mathf.Deg2Rad),
            radius * Mathf.Sin(angle * Mathf.Deg2Rad));

            angle += angleDelta;
        }
    }

}
