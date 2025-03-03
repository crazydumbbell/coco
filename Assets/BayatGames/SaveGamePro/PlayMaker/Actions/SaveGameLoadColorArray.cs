using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

    /// <summary>
    /// Save game load array action.
    /// </summary>
    [ActionCategory("SaveGamePro")]
    [Tooltip("Loads the specified identifier and outputs the value to the variable.")]
    public class SaveGameLoadColorArray : FsmStateAction
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
        [ArrayEditor(VariableType.Color)]
        public FsmArray valueArray;

        public override void OnEnter()
        {
            if (!identifier.IsNone && !string.IsNullOrEmpty(identifier.Value))
            {
                var array = SaveGame.Load<UnityEngine.Color[]>(identifier.Value);
                valueArray.Values = Array.ConvertAll<UnityEngine.Color, object>(array, (item) =>
                {
                    return (object)item;
                });
            }
            Finish();
        }

    }

}