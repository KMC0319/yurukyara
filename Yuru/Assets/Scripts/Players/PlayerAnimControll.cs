﻿using System;
using System.Collections.Generic;
using Animations;
using Battles.Health;
using doma;
using UniRx;
using UnityEngine;

namespace Players{
	public enum AnimResponce{
		Wait,AttackEnd,Damaged
	}
	[RequireComponent(typeof(BoxContainer))]
	public class PlayerAnimControll : MonoBehaviour{
		[SerializeField] public PlayerAnimDictionary MyDic;
		
		private PlayAbleController playAbleController;
		private BoxContainer boxContainer;
		
		
		private readonly Subject<AnimResponce> responseStream=new Subject<AnimResponce>();
		public Subject<AnimResponce> ResponseStream => responseStream;

		private AnimBox current;

		private void Start (){
			playAbleController = this.GetComponent<PlayAbleController>();
			boxContainer = this.GetComponent<BoxContainer>();
			
			playAbleController.PlayEndStream.Subscribe(FlowResponce);
		}

		private void Play(AnimBox anim_box){
			if(current==anim_box)return;
			playAbleController.TransAnimation(anim_box);
			current = anim_box;
		}

		public AnimBox ChangeAnim(string name){
			var res = boxContainer.FindAnim(name);
			Play(res);
			return res;
		}
		
		public void ChangeAnim(AnimBox anim_box){
			Play(anim_box);
		}

		public void ForceChangeAnim(AnimBox anim_box){
			playAbleController.TransAnimation(anim_box);
		}
		public AnimBox ForceChangeAnim(string name){
			var res = boxContainer.FindAnim(name);
			playAbleController.TransAnimation(res);
			return res;
		}

		private void FlowResponce(AnimBox anim_box){
			AnimResponce? a = null;
			
			if (anim_box is AttackBox){
				a = AnimResponce.AttackEnd;
			}

			if (anim_box.clip.name == MyDic.SmallDamage){
				a = AnimResponce.Damaged;
			}

			if (a != null){
				responseStream.OnNext((AnimResponce) a);
			}
		}

	}
}