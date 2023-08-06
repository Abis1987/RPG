using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool drawWeapon;
		public bool attack;
		public bool spell1;
		public bool spell2;
		public bool spell3;
		public bool spell4;
		public bool spell5;




		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

		

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
            if (!Player.MyInstance.isDead)
            {
				if (Player.MyInstance.GetComponentInChildren<InteractWithInteractable>().MyCurrentVendor != null)
				{
					if (Player.MyInstance.GetComponentInChildren<InteractWithInteractable>().MyCurrentVendor.IsOpen == false)
					{
						MoveInput(value.Get<Vector2>());
					}
					else
					{
						return;
					}
				}
				else
				{
					MoveInput(value.Get<Vector2>());
				}
			}

		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{

			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			
				SprintInput(value.isPressed);
			
			
		}

  //      public void OnDrawWeapon(InputValue value)
  //      {
		//	if(Player.MyInstance.GetComponent<EquipmentSystem>().MyWeapon != null)
  //          {
		//		DrawWeaponInput(value.isPressed);
		//	}
			
		//}
        
		
		public void OnAttack(InputValue value)
		{
			if (!InputManager.MyInstance.inventoryOpen && !InputManager.MyInstance.lootOpen && !InputManager.MyInstance.vendorOpen && !InputManager.MyInstance.craftOpen )
            {
				AttackInput(value.isPressed);
			}
				
		}

		public void OnSpell1(InputValue value)
		{
			if(InventoryScript.MyInstance.Skill1.MyEquipedArmor != null)
            {
				if (InventoryScript.MyInstance.Skill1.MyEquipedArmor.MyUsableSpell != null)
				{
					Spell1Input(value.isPressed);
				}
			}
			
			
		}
		public void OnSpell2(InputValue value)
		{
			if (InventoryScript.MyInstance.Skill2.MyEquipedArmor != null)
            {
				if (InventoryScript.MyInstance.Skill2.MyEquipedArmor.MyUsableSpell != null)
				{
					Spell2Input(value.isPressed);
				}
			}
				
				
		}
		public void OnSpell3(InputValue value)
		{
			if (InventoryScript.MyInstance.Skill3.MyEquipedArmor != null)
            {
				if (InventoryScript.MyInstance.Skill3.MyEquipedArmor.MyUsableSpell != null)
				{
					Spell3Input(value.isPressed);
				}
			}
				
				
		}
		public void OnSpell4(InputValue value)
		{
			if (InventoryScript.MyInstance.Skill4.MyEquipedArmor != null)
            {
				if (InventoryScript.MyInstance.Skill4.MyEquipedArmor.MyUsableSpell != null)
				{
					Spell4Input(value.isPressed);
				}
			}
				
				
		}
		public void OnSpell5(InputValue value)
		{
			if(!InputManager.MyInstance.inventoryOpen && !InputManager.MyInstance.lootOpen && !InputManager.MyInstance.vendorOpen && !InputManager.MyInstance.craftOpen)
			{
				if (InventoryScript.MyInstance.Skill5.MyEquipedArmor != null)
				{
					if (InventoryScript.MyInstance.Skill5.MyEquipedArmor.MyUsableSpell != null)
					{
						Spell5Input(value.isPressed);
					}
				}
			}
			
				
				
		}


#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

        //public void DrawWeaponInput(bool newDrawWeaponState)
        //{
        //    drawWeapon = newDrawWeaponState;
        //}

		public void AttackInput(bool newAttackState)
		{
			attack = newAttackState;
		}

		public void Spell1Input(bool newSpellCastState)
		{
			spell1 = newSpellCastState;
		}
		public void Spell2Input(bool newSpellCastState)
		{
			spell2 = newSpellCastState;
		}
		public void Spell3Input(bool newSpellCastState)
		{
			spell3 = newSpellCastState;
		}
		public void Spell4Input(bool newSpellCastState)
		{
			spell4 = newSpellCastState;
		}
		public void Spell5Input(bool newSpellCastState)
		{
			spell5 = newSpellCastState;
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}


	}
	
}