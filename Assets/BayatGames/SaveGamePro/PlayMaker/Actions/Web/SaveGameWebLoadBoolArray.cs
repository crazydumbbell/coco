using System;
using System.Collections;

using BayatGames.SaveGamePro.Networking;

using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

    /// <summary>
    /// Save game web load array action.
    /// </summary>
    [ActionCategory("SaveGamePro Cloud")]
    [Tooltip("Loads the data using the specified identifier.")]
    public class SaveGameWebLoadBoolArray : FsmStateAction
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
        [UIHint(UIHint.Variable)]
        [ArrayEditor(VariableType.Bool)]
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
            StartCoroutine(DoLoad());
        }

        IEnumerator DoLoad()
        {
            SaveGameWeb web = new SaveGameWeb(this.url.Value, this.secretKey.Value, this.username.Value, this.password.Value);
            if (!this.identifier.IsNone && !string.IsNullOrEmpty(this.identifier.Value))
            {
                yield return StartCoroutine(web.Download(this.identifier.Value));
                if (web.Request.isHttpError || web.Request.isNetworkError)
                {
                    this.requestError.Value = web.Request.error;
                    this.cloudError.Value = web.Request.downloadHandler.text;
                    Fsm.Event(this.isFailed);
                }
                else
                {
                    var array = web.Load<bool[]>();
                    this.valueArray.Values = Array.ConvertAll<bool, object>(array, (item) =>
                    {
                        return (object)item;
                    });
                    Fsm.Event(this.isSuccess);
                }
            }
            Finish();
        }

    }

}