// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace GTAlpha
{
    public class @InputMaster : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputMaster()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""af4a45b4-d4bb-4dbf-b76f-635c5e037ff5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""8dd6399e-1f22-4602-8ecc-3ac11fc227c8"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""4bb03bf1-46d0-4a0e-9022-4c5ba16a9bdc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Evade"",
                    ""type"": ""Button"",
                    ""id"": ""e043946b-2414-406e-8be1-06799d5a3757"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump & Parkour"",
                    ""type"": ""Button"",
                    ""id"": ""a5c1eaf0-f5d4-471c-b369-7836ff5da53a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackSingleTarget"",
                    ""type"": ""Button"",
                    ""id"": ""a53304d7-0463-4f2d-bf28-62351ed7011b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AttackMultipleTarget"",
                    ""type"": ""Button"",
                    ""id"": ""f6cf4d47-6d41-44a7-8a00-cf5e8c777252"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill 1"",
                    ""type"": ""Button"",
                    ""id"": ""51b689bf-f1e4-475b-b4e6-aaee3da8fa8e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Skill 2"",
                    ""type"": ""Button"",
                    ""id"": ""ad683e23-d78a-4f51-b6c8-ac5b693faad2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Use Item"",
                    ""type"": ""Button"",
                    ""id"": ""363b5d0f-166e-4816-bc84-038e8b6d9a20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Change Item"",
                    ""type"": ""Button"",
                    ""id"": ""dbe428b9-612e-488a-b798-2ab9a5f2fa62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon 1"",
                    ""type"": ""Button"",
                    ""id"": ""9b34bcee-1b7c-447b-a5a5-47f3c13d2d40"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon 2"",
                    ""type"": ""Button"",
                    ""id"": ""577a7b5c-080d-4d9b-b957-df7bbdf0d6c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Weapon 3"",
                    ""type"": ""Button"",
                    ""id"": ""fd9a0dee-84d4-4e0a-b420-d262d9186829"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Mouse"",
                    ""id"": ""da2a8593-bb1a-4351-80c1-d98a9f60b54a"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""303d3fc9-e672-4198-95ec-8545da5235f7"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f7b9fe4a-6349-4961-9e48-0c839b7fbbc9"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4ed2ef81-e05c-4c2b-a440-d306a0999b26"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a11aa844-1720-4c64-97a2-233f6e395f01"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""125fc40a-2e89-428e-8a28-644deee1fd42"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""99f425a1-46e2-4c49-9eb8-3de150341f95"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""cca78f20-9e83-414e-bcdc-020acdd2d3d1"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2d94424d-e629-4314-8167-8e5b1dd093ef"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""2ceffea8-565f-4bc1-84c7-12f4b60986d4"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""92b65b60-7087-4a78-87e8-eb322ac3664c"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Evade"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3b99fea-87da-41c2-8135-cd84c736173f"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Evade"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""23b3bbdb-0d7d-43f4-a79a-0ad33617f0dc"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Jump & Parkour"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e3eb890-726a-4d66-891a-d5453da6ac9e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Jump & Parkour"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b65f4f1-2f40-4cc1-942d-b3863139fe85"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""AttackSingleTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ee12cb1-d270-4713-9451-3c7cf1ea17a0"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""AttackSingleTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""161e1d8d-836f-4e08-b401-11acaf4ac6bd"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""AttackMultipleTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d077ace9-a529-409d-956a-88bcbd43cf67"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""AttackMultipleTarget"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""16dcd43e-67ea-4016-a7c9-0c659dd92454"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Skill 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""775cbad0-8f12-42e6-8224-28a462fb74f2"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Skill 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edde737b-c223-4f4a-a5fa-38b83618e4d1"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Skill 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65d6fca7-aabc-42ce-af5d-3b332aca667b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Skill 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""07f791ef-3204-4323-8804-8c9b01d48f0c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Use Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9b8f548b-edd1-4508-93b6-dce737764782"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Use Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""019f1b66-a3c6-4ad6-8f10-d3b14b290444"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Change Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""473b07a0-477b-411e-b0a8-527874dc8870"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Change Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55be0424-8a4e-4698-81da-074216697d94"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Weapon 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36ccbd36-dfd2-45af-9be7-622845aeca19"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Weapon 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""033bef22-0372-4d1e-bba4-dee57fa2dd87"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Weapon 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cca61629-aed8-45d8-99cf-820c054c4513"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Weapon 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96c177f6-8f18-4887-aff5-068c7a830011"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Weapon 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a56c9d44-6203-449f-8b6c-0f08a3c12273"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Weapon 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""791cb831-7c08-4d70-815e-6196a5b36e0a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7027939c-125b-4935-9610-487c47a8fb12"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7f91efaa-e480-458e-9c07-9dbb8ea03b37"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""83f2320f-2863-41ef-a07b-2e5f43701d2e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""64535900-1ec5-4207-9f35-fa421b08c6fc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""3c00d0df-0023-43a1-8a8f-08dbc8fa8408"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2eb3521f-5a74-41c1-b828-083ea33e7e2e"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b95e7ddf-aacb-4623-95e1-41ab8e0b91ae"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bed70a78-2a40-4af6-baa4-e5c2c1d0e1c7"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""60ed7b93-dd05-4c63-af77-5dc2236debb9"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player"",
            ""bindingGroup"": ""Player"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // InGame
            m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
            m_InGame_Move = m_InGame.FindAction("Move", throwIfNotFound: true);
            m_InGame_Rotate = m_InGame.FindAction("Rotate", throwIfNotFound: true);
            m_InGame_Evade = m_InGame.FindAction("Evade", throwIfNotFound: true);
            m_InGame_JumpParkour = m_InGame.FindAction("Jump & Parkour", throwIfNotFound: true);
            m_InGame_AttackSingleTarget = m_InGame.FindAction("AttackSingleTarget", throwIfNotFound: true);
            m_InGame_AttackMultipleTarget = m_InGame.FindAction("AttackMultipleTarget", throwIfNotFound: true);
            m_InGame_Skill1 = m_InGame.FindAction("Skill 1", throwIfNotFound: true);
            m_InGame_Skill2 = m_InGame.FindAction("Skill 2", throwIfNotFound: true);
            m_InGame_UseItem = m_InGame.FindAction("Use Item", throwIfNotFound: true);
            m_InGame_ChangeItem = m_InGame.FindAction("Change Item", throwIfNotFound: true);
            m_InGame_Weapon1 = m_InGame.FindAction("Weapon 1", throwIfNotFound: true);
            m_InGame_Weapon2 = m_InGame.FindAction("Weapon 2", throwIfNotFound: true);
            m_InGame_Weapon3 = m_InGame.FindAction("Weapon 3", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // InGame
        private readonly InputActionMap m_InGame;
        private IInGameActions m_InGameActionsCallbackInterface;
        private readonly InputAction m_InGame_Move;
        private readonly InputAction m_InGame_Rotate;
        private readonly InputAction m_InGame_Evade;
        private readonly InputAction m_InGame_JumpParkour;
        private readonly InputAction m_InGame_AttackSingleTarget;
        private readonly InputAction m_InGame_AttackMultipleTarget;
        private readonly InputAction m_InGame_Skill1;
        private readonly InputAction m_InGame_Skill2;
        private readonly InputAction m_InGame_UseItem;
        private readonly InputAction m_InGame_ChangeItem;
        private readonly InputAction m_InGame_Weapon1;
        private readonly InputAction m_InGame_Weapon2;
        private readonly InputAction m_InGame_Weapon3;
        public struct InGameActions
        {
            private @InputMaster m_Wrapper;
            public InGameActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_InGame_Move;
            public InputAction @Rotate => m_Wrapper.m_InGame_Rotate;
            public InputAction @Evade => m_Wrapper.m_InGame_Evade;
            public InputAction @JumpParkour => m_Wrapper.m_InGame_JumpParkour;
            public InputAction @AttackSingleTarget => m_Wrapper.m_InGame_AttackSingleTarget;
            public InputAction @AttackMultipleTarget => m_Wrapper.m_InGame_AttackMultipleTarget;
            public InputAction @Skill1 => m_Wrapper.m_InGame_Skill1;
            public InputAction @Skill2 => m_Wrapper.m_InGame_Skill2;
            public InputAction @UseItem => m_Wrapper.m_InGame_UseItem;
            public InputAction @ChangeItem => m_Wrapper.m_InGame_ChangeItem;
            public InputAction @Weapon1 => m_Wrapper.m_InGame_Weapon1;
            public InputAction @Weapon2 => m_Wrapper.m_InGame_Weapon2;
            public InputAction @Weapon3 => m_Wrapper.m_InGame_Weapon3;
            public InputActionMap Get() { return m_Wrapper.m_InGame; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
            public void SetCallbacks(IInGameActions instance)
            {
                if (m_Wrapper.m_InGameActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                    @Rotate.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnRotate;
                    @Rotate.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnRotate;
                    @Rotate.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnRotate;
                    @Evade.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnEvade;
                    @Evade.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnEvade;
                    @Evade.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnEvade;
                    @JumpParkour.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnJumpParkour;
                    @JumpParkour.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnJumpParkour;
                    @JumpParkour.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnJumpParkour;
                    @AttackSingleTarget.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackSingleTarget;
                    @AttackSingleTarget.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackSingleTarget;
                    @AttackSingleTarget.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackSingleTarget;
                    @AttackMultipleTarget.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackMultipleTarget;
                    @AttackMultipleTarget.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackMultipleTarget;
                    @AttackMultipleTarget.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackMultipleTarget;
                    @Skill1.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnSkill1;
                    @Skill1.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnSkill1;
                    @Skill1.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnSkill1;
                    @Skill2.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnSkill2;
                    @Skill2.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnSkill2;
                    @Skill2.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnSkill2;
                    @UseItem.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnUseItem;
                    @UseItem.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnUseItem;
                    @UseItem.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnUseItem;
                    @ChangeItem.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnChangeItem;
                    @ChangeItem.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnChangeItem;
                    @ChangeItem.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnChangeItem;
                    @Weapon1.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnWeapon1;
                    @Weapon1.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnWeapon1;
                    @Weapon1.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnWeapon1;
                    @Weapon2.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnWeapon2;
                    @Weapon2.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnWeapon2;
                    @Weapon2.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnWeapon2;
                    @Weapon3.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnWeapon3;
                    @Weapon3.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnWeapon3;
                    @Weapon3.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnWeapon3;
                }
                m_Wrapper.m_InGameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Rotate.started += instance.OnRotate;
                    @Rotate.performed += instance.OnRotate;
                    @Rotate.canceled += instance.OnRotate;
                    @Evade.started += instance.OnEvade;
                    @Evade.performed += instance.OnEvade;
                    @Evade.canceled += instance.OnEvade;
                    @JumpParkour.started += instance.OnJumpParkour;
                    @JumpParkour.performed += instance.OnJumpParkour;
                    @JumpParkour.canceled += instance.OnJumpParkour;
                    @AttackSingleTarget.started += instance.OnAttackSingleTarget;
                    @AttackSingleTarget.performed += instance.OnAttackSingleTarget;
                    @AttackSingleTarget.canceled += instance.OnAttackSingleTarget;
                    @AttackMultipleTarget.started += instance.OnAttackMultipleTarget;
                    @AttackMultipleTarget.performed += instance.OnAttackMultipleTarget;
                    @AttackMultipleTarget.canceled += instance.OnAttackMultipleTarget;
                    @Skill1.started += instance.OnSkill1;
                    @Skill1.performed += instance.OnSkill1;
                    @Skill1.canceled += instance.OnSkill1;
                    @Skill2.started += instance.OnSkill2;
                    @Skill2.performed += instance.OnSkill2;
                    @Skill2.canceled += instance.OnSkill2;
                    @UseItem.started += instance.OnUseItem;
                    @UseItem.performed += instance.OnUseItem;
                    @UseItem.canceled += instance.OnUseItem;
                    @ChangeItem.started += instance.OnChangeItem;
                    @ChangeItem.performed += instance.OnChangeItem;
                    @ChangeItem.canceled += instance.OnChangeItem;
                    @Weapon1.started += instance.OnWeapon1;
                    @Weapon1.performed += instance.OnWeapon1;
                    @Weapon1.canceled += instance.OnWeapon1;
                    @Weapon2.started += instance.OnWeapon2;
                    @Weapon2.performed += instance.OnWeapon2;
                    @Weapon2.canceled += instance.OnWeapon2;
                    @Weapon3.started += instance.OnWeapon3;
                    @Weapon3.performed += instance.OnWeapon3;
                    @Weapon3.canceled += instance.OnWeapon3;
                }
            }
        }
        public InGameActions @InGame => new InGameActions(this);
        private int m_PlayerSchemeIndex = -1;
        public InputControlScheme PlayerScheme
        {
            get
            {
                if (m_PlayerSchemeIndex == -1) m_PlayerSchemeIndex = asset.FindControlSchemeIndex("Player");
                return asset.controlSchemes[m_PlayerSchemeIndex];
            }
        }
        public interface IInGameActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnRotate(InputAction.CallbackContext context);
            void OnEvade(InputAction.CallbackContext context);
            void OnJumpParkour(InputAction.CallbackContext context);
            void OnAttackSingleTarget(InputAction.CallbackContext context);
            void OnAttackMultipleTarget(InputAction.CallbackContext context);
            void OnSkill1(InputAction.CallbackContext context);
            void OnSkill2(InputAction.CallbackContext context);
            void OnUseItem(InputAction.CallbackContext context);
            void OnChangeItem(InputAction.CallbackContext context);
            void OnWeapon1(InputAction.CallbackContext context);
            void OnWeapon2(InputAction.CallbackContext context);
            void OnWeapon3(InputAction.CallbackContext context);
        }
    }
}
