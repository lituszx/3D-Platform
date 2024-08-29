using UnityEngine;

public interface IEntity
{
    Transform transform { get; }
    void MoveFromTo(Vector3 startPosition, Vector3 endPosition);
}
