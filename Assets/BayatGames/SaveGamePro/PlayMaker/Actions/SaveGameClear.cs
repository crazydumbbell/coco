using System;
using HutongGames.PlayMaker;

namespace BayatGames.SaveGamePro.PlayMaker.Actions
{

	/// <summary>
	/// Save game clear action.
	/// </summary>
	[ActionCategory ( "SaveGamePro" )]
	[Tooltip ( "Clears the all saved data. Use with Caution" )]
	public class SaveGameClear : FsmStateAction
	{

		public override void OnEnter ()
		{
			SaveGame.Clear ();
			Finish ();
		}

	}

}