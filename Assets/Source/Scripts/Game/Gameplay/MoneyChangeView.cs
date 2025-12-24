using Game.Data;
using TMPro;
using UnityEngine;

namespace Game.Gameplay
{
    public class MoneyChangeView : CameraBillboard
    {
        [SerializeField] private TMP_Text moneyText;

        public void SetCount(int count)
        {
            moneyText.text = $"{count} {GlobalConstants.moneyTag}";
            Lean.Pool.LeanPool.Despawn(gameObject,1f);
        }
    }
}