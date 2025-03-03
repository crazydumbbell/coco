using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

    /// <summary>
    /// Save game save action.
    /// </summary>
    [ActionCategory("SaveGamePro")]
    [Tooltip("Saves the specified value using the given identifier.")]
    public class SaveGameSave : FsmStateAction
    {

        [CompoundArray("Count", "Identifier", "Value")]

        /// <summary>
        /// The identifiers.
        /// </summary>
        [RequiredField]
        public FsmString[] identifiers;

        /// <summary>
        /// The values.
        /// </summary>
        [ArrayEditor(VariableType.String)]
        public FsmVar[] values;

        public override void Reset()
        {
            identifiers = new FsmString[1];
            values = new FsmVar[1];
        }

        public override void OnEnter()
        {
            for (int i = 0; i < identifiers.Length; i++)
            {
                if (!identifiers[i].IsNone && !string.IsNullOrEmpty(identifiers[i].Value))
                {
                    values[i].UpdateValue();
                    SaveGame.Save(identifiers[i].Value, values[i].GetValue());
                }
            }
            Finish();
        }

    }

}