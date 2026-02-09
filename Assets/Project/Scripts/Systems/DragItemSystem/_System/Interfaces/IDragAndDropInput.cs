using R3;
using UnityEngine;

namespace Project.Scripts
{
    public interface IDragAndDropInput
    {
        ReactiveCommand<DragAndDropItemBase> OnBeginDrag { get; }
        ReactiveCommand<Vector2> OnDrag { get; }
        ReactiveCommand<DragAndDropSlot> OnDrop { get; }
        ReactiveCommand<Unit> OnEndDrag { get; }
    }
}