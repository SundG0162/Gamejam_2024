using UnityEngine;

namespace BSM.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;

        [field: SerializeField]
        public float FacingDirection { get; private set; } = 1;

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }

        public void Flip()
        {
            FacingDirection *= -1;
            _entity.transform.Rotate(new Vector3(0, 180f, 0));
        }

        public void Flip(float x)
        {
            if (Mathf.Abs(FacingDirection + x) < 0.5f)
                Flip();
        }
    }
}
