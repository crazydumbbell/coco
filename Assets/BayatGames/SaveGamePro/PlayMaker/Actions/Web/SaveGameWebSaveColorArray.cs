using System;
using System.Collections;
using HutongGames.PlayMaker;

using BayatGames.SaveGamePro.Networking;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

    /// <summary>
    /// Save game web save array action.
    /// </summary>
    [ActionCategory("SaveGamePro Cloud")]
    [Tooltip("Saves the specified array using the given identifier to the Web.")]
    public class SaveGameWebSaveColorArray : FsmStateAction
    {

        /// <summary>
        /// The URL.
        /// </summary>
        [RequiredField]
        public FsmString url;

        /// <summary>
        /// The secret key.
        /// </summary>
        [RequiredField]
        public FsmString secretKey;

        /// <summary>
        /// The username.
        /// </summary>
        [RequiredField]
        public FsmString username;

        /// <summary>
        /// The password.
        /// </summary>
        public FsmString password;

        /// <summary>
        /// The identifiers.
        /// </summary>
        [RequiredField]
        public FsmString identifier;

        /// <summary>
        /// The values.
        /// </summary>
        [ArrayEditor(VariableType.Color)]
        public FsmArray valueArray;

        /// <summary>
        /// The request error.
        /// </summary>
        [Title("Store Request Error")]
        [Tooltip("The Error that is reported by Request")]
        [UIHint(UIHint.Variable)]
        public FsmString requestError;

        /// <summary>
        /// The cloud error.
        /// </summary>
        [Title("Store Cloud Error")]
        [Tooltip("The Error that is reported by Save Game Pro Cloud")]
        [UIHint(UIHint.Variable)]
        public FsmString cloudError;

        /// <summary>
        /// The is success event.
        /// </summary>
        public FsmEvent isSuccess;

        /// <summary>
        /// The is failed event.
        /// </summary>
        public FsmEvent isFailed;

        public override void OnEnter()
        {
            StartCoroutine(DoSave());
        }

        IEnumerator DoSave()
        {
            SaveGameWeb web = new SaveGameWeb(url.Value, secretKey.Value, username.Value, password.Value);
            if (!identifier.IsNone && !string.IsNullOrEmpty(identifier.Value))
            {
                yield return StartCoroutine(web.Save(identifier.Value, valueArray.Values));
                UnityEngine.Debug.Log(web.Request.downloadHandler.text);
                if (web.Request.isHttpError || web.Request.isNetworkError)
                {
                    requestError.Value = web.Request.error;
                    cloudError.Value = web.Request.downloadHandler.text;
                    Fsm.Event(isFailed);
                }
                else
                {
                    Fsm.Event(isSuccess);
                }
            }
            Finish();
        }

    }

}