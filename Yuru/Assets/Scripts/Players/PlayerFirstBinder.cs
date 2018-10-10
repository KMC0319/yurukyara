﻿using doma.Inputs;
using UnityEngine;
using Zenject;

namespace Players{
	public class PlayerFirstBinder : MonoBehaviour,IPlayerBinder{

		[Inject]private PlayerSecondBinder target;

		public PlayerRootControll TargetPlayerRootControll => target.PlayerRootControll;
		public PlayerRootControll PlayerRootControll{ get; set; }
		
		
		[Inject] private InputRelayPoint inputRelayPoint;

		private void Awake(){
			PlayerRootControll = this.GetComponent<PlayerRootControll>();
		}

		private void Start(){
			Launch();
		}

		public void Launch(){
			inputRelayPoint.ChangeReciever(PlayerRootControll);
			inputRelayPoint.IsActive = true;
		}
	}
}