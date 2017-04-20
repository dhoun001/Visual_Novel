// Touchable.cs
using UnityEngine;

namespace UnityEngine.UI
{

    public class Touchable : Graphic
    {

        public override bool Raycast(Vector2 sp, Camera eventCamera)
        {
            return true;
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}