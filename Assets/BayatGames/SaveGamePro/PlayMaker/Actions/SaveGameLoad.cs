using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

    /// <summary>
    /// Save game load action.
    /// </summary>
    [ActionCategory("SaveGamePro")]
    [Tooltip("Loads the specified identifier and outputs the value to the variable.")]
    public class SaveGameLoad : FsmStateAction
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
        [UIHint(UIHint.Variable)]
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
                    if (values[i].RealType.IsSubclassOf(typeof(UnityEngine.Object)) && !values[i].IsNone &&
                         values[i].objectReference != null)
                    {
                        SaveGame.LoadInto(identifiers[i].Value, values[i].GetValue());
                    }
                    else
                    {
                        values[i].SetValue(SaveGame.Load(identifiers[i].Value, values[i].RealType));
                    }
                }
            }
            Finish();
        }

    }

}