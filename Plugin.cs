using System;
using System.Reflection;
using HarmonyLib;
using Sandbox.Definitions;
using Sandbox.Game.Gui;
using Sandbox.Game.Screens.Helpers;
using Sandbox.Graphics.GUI;
using VRage.Game;
using VRage.Plugins;
using VRageMath;

namespace PureVanilla
{
	[HarmonyPatch]
	public class PureVanillaPlugin : IDisposable, IPlugin
	{
		public void Dispose()
		{
		}

		public void Init(object gameInstance)
		{
			new Harmony("PureVanilla").PatchAll(Assembly.GetExecutingAssembly());
		}

		public void Update()
		{
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiScreenToolbarConfigBase), "IsBlockResearched")]
		public static bool IsBlockResearchedPatch(ref bool __result, ref MyCubeBlockDefinition block)
		{
			if (block.DLCs != null)
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiScreenToolbarConfigBase), "AddDefinition")]
		public static bool AddDefinitionPatch(ref MyGuiControlGrid grid, ref MyObjectBuilder_ToolbarItem data, ref MyDefinitionBase definition, bool enabled = true)
		{
			if (definition.DLCs != null)
				return false;
			return true;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiScreenToolbarConfigBase), "AddDefinitionAtPosition")]
		public static bool AddDefinitionAtPositionPatch(ref MyCubeBlockDefinition block)
		{
			if (block.DLCs != null)
				return false;
			return true;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiScreenToolbarConfigBase), "AddCubeDefinition")]
		public static bool AddCubeDefinitionPatch(ref MyGuiControlGrid grid, ref MyCubeBlockDefinitionGroup group, ref Vector2I position)
		{
			if (group.Any.DLCs != null)
				return false;
			return true;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiScreenToolbarConfigBase), "AddCategoryToDisplayList")]
		public static bool AddCategoryToDisplayListPatch(ref string displayName, ref MyGuiBlockCategoryDefinition categoryID)
		{
			if (categoryID != null && categoryID.DLCs != null)
				return false;
			return true;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiScreenToolbarConfigBase), "CreateNodeItem")]
		public static bool CreateNodeItemPatch(ref MyGuiControlResearchGraph.GraphNode node, ref MyCubeBlockDefinition definition)
		{
			if (definition.DLCs != null)
				return false;
			return true;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiControlBlockGroupInfo), "IsAllowed")]
		public static bool IsAllowedPatch(ref bool __result, ref MyDefinitionBase blockDefinition)
		{
			if (blockDefinition?.DLCs != null)
			{
				__result = false;
				return false;
			}
			return true;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiControlBlockGroupInfo), "SetBlockGroup")]
		public static bool SetBlockGroupPatch(ref MyCubeBlockDefinitionGroup group)
		{
			if (group.Any.DLCs != null)
				return false;
			return true;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiControlBlockGroupInfo), "SetGeneralDefinition")]
		public static bool SetGeneralDefinition(ref MyDefinitionBase definition)
		{
			if (definition.DLCs != null)
				return false;
			return true;
		}

		[HarmonyPrefix]
		[HarmonyPatch(typeof(MyGuiControlBlockGroupInfo), "AddItemVariantDefinition")]
		public static bool AddItemVariantDefinition(ref MyCubeBlockDefinitionGroup group, ref int row)
		{
			if (group.Any.DLCs != null)
				return false;
			return true;
		}
	}
}