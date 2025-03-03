using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

    /// <summary>
    /// Save game load array action.
    /// </summary>
    [ActionCategory("SaveGamePro")]
    [Tooltip("Loads the specified identifier and outputs the value to the variable.")]
    public class SaveGameLoadVector2Array : FsmStateAction
    {

        /// <summary>
        /// The identifiers.
        /// </summary>
        [RequiredField]
        public FsmString identifier;

        /// <summary>
        /// The array.
        /// </summary>
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Vector2)]
        public FsmArray valueArray;

        public override void OnEnter()
        {
            if (!identifier.IsNone && !string.IsNullOrEmpty(identifier.Value))
            {
                var array = SaveGame.Load<UnityEngine.Vector2[]>(identifier.Value);
                valueArray.Values = Array.ConvertAll<UnityEngine.Vector2, object>(array, (item) =>
                {
                    return (object)item;
                });
            }
            Finish();
        }

    }

}