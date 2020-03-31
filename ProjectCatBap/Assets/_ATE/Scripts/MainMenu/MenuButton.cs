using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ATE
{
    [RequireComponent(typeof(Collider2D))]
	public abstract class MenuButton : MonoBehaviour
	{
        protected abstract void OnClicked();

        private void OnMouseDown()
        {
            OnClicked ();
        }

    }
}