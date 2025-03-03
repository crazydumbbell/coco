using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

    /// <summary>
    /// Save game save array action.
    /// </summary>
    [ActionCategory("SaveGamePro")]
    [Tooltip("Saves the specified array using the given identifier.")]
    public class SaveGameSaveStringArray : FsmStateAction
    {

        /// <summary>
        /// The identifiers.
        /// </summary>
        [RequiredField]
        public FsmString identifier;

        /// <summary>
        /// The array.
        /// </summary>
        [ArrayEditor(VariableType.String)]
        public FsmArray valueArray;

        public override void OnEnter()
        {
            if (!identifier.IsNone && !string.IsNullOrEmpty(identifier.Value))
            {
                SaveGame.Save(identifier.Value, valueArray.Values);
            }
            Finish();
        }

    }

}