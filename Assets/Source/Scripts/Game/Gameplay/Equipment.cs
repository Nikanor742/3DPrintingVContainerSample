using UnityEngine;

namespace Game.Gameplay
{
    public abstract class Equipment : MonoBehaviour
    {
        public Renderer installBoxRenderer;
        public BoxCollider installBoxCollider;
        public BoxCollider interactBoxCollider;
        public GameObject arrows;
        
        public bool interactable;

        public void SetActive()
        {
            interactable = true;
            installBoxRenderer.material.color = Color.clear;
            arrows.SetActive(false);
        }
    }
}
