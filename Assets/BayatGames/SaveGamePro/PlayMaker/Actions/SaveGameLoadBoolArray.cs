using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

    /// <summary>
    /// Save game load array action.
    /// </summary>
    [ActionCategory("SaveGamePro")]
    [Tooltip("Loads the specified identifier and outputs the value to the variable.")]
    public class SaveGameLoadBoolArray : FsmStateAction
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
        [ArrayEditor(VariableType.Bool)]
        public FsmArray valueArray;

        public override void OnEnter()
        {
            if (!identifier.IsNone && !string.IsNullOrEmpty(identifier.Value))
            {
                valueArray.boolValues = SaveGame.Load<bool[]>(identifier.Value);
            }
            Finish();
        }

    }

}